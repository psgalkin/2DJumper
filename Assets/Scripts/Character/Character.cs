using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Camera _camera;
    private float _maxHeightChracterPosition = 0;
    private float _deltaCameraCharacter;

    private GameLogic _gameLogic;
    private MovingController _movingController;
    private AttackController _attackController;
    private InGameUi _ui;
    private Visual _visual;
    private Rigidbody2D _rigidbody;

    private bool _isMagnetWorking = false;
    private bool _isHasArmor = false;
    private int _coinCount;

    private float _lvlWidth;

    private IEnumerator _armorCoroutine;
    private IEnumerator _magnetCoroutine;
    private IEnumerator _jetpackCoroutine;


    [SerializeField] private CharacterData _characterData;

    void Awake()
    {
        _camera = FindObjectOfType<Camera>();

        _deltaCameraCharacter =  Camera.main.orthographicSize / 5;
        _lvlWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;

        _gameLogic = FindObjectOfType<GameLogic>();

        _movingController = GetComponent<MovingController>();
        _movingController.SetForceCoef(_characterData.ForceJumpCoef);
        _movingController.SetFlyingSpeed(_characterData.FlyingSpeed);

        _attackController = GetComponent<AttackController>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<InGameUi>();
        _visual = GetComponent<Visual>();


        _armorCoroutine = ArmorLogic();
        _magnetCoroutine = MagnetLogic();
        _jetpackCoroutine = JetpackLogic();
    }

    public float GetCoinSpeed()
    {
        return _characterData.CoinSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Boost))
        {
            switch (collision.GetComponent<Boost>().GetType())
            {
                case BoostType.Trampoline:
                    StartTrampoline();
                    break;
                case BoostType.Jetpack:
                    StartJetpack();
                    Destroy(collision.gameObject);
                    break;
                case BoostType.Magnet:
                    StartMagnet();
                    Destroy(collision.gameObject);
                    break;
                case BoostType.WeaponRocket:
                    StartWeapon(WeaponType.Rocket);
                    Destroy(collision.gameObject);
                    break;
                case BoostType.WeaponLaser:
                    StartWeapon(WeaponType.Laser);
                    Destroy(collision.gameObject);
                    break;
                case BoostType.Armor:
                    StartArmor();
                    Destroy(collision.gameObject);
                    break;
            }
        }
        else if (collision.CompareTag(Tag.Coin))
        {
            StartCoin();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(Tag.Enemy))
        {
            if (_isHasArmor) { StopArmor(); }
            else { _gameLogic.Loss(); }
        }
        else if (collision.CompareTag(Tag.Border))
        {
            switch (collision.GetComponent<Border>().GetType())
            {
                case BorderType.Bottom:
                    _gameLogic.Loss();
                    break;
                case BorderType.EndLevel:
                    _gameLogic.Win(_coinCount);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rigidbody.velocity.y < 0.01f
            && collision.gameObject.CompareTag(Tag.Platform))
        {
            switch (collision.gameObject.GetComponent<Platform>().GetType())
            {
                case PlatformType.Brokening:
                    bool triggerJump = false;
                    if (collision.gameObject.GetComponentInChildren<Boost>() != null &&
                        collision.gameObject.GetComponentInChildren<Boost>().GetType() == BoostType.Trampoline)
                    {
                        triggerJump = true;
                    }
                    Destroy(collision.gameObject);
                    if (triggerJump)
                    {
                        _movingController.ForceJump();
                    }
                    break;
                case PlatformType.Trap:
                    _movingController.RandomJump();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Boost) &&
            collision.GetComponent<Boost>().GetType() == BoostType.Trampoline)
        {
            _movingController.StopJump();
        }
    }


    private void StartTrampoline()
    {
        _movingController.ForceJump();
    }

    //**********************************************************************************//

    private void StartJetpack()
    {
        
        StopCoroutine(_jetpackCoroutine);

        _ui.EndBoostStatus(BoostType.Jetpack);
        _visual.StopJetpack();

        _jetpackCoroutine = JetpackLogic();
        StartCoroutine(_jetpackCoroutine);

    }

    private IEnumerator JetpackLogic()
    {
        
        _movingController.StartFlying();
        _visual.StartJetpack();

        _ui.StartBoostStatus(BoostType.Jetpack, _characterData.JetpackDuration);
        yield return new WaitForSeconds(1.0f);

        for (int i = _characterData.JetpackDuration - 1; i > 0; --i)
        {
            _ui.SetBoostStatus(BoostType.Jetpack, i);
            yield return new WaitForSeconds(1.0f);
        }
        _ui.EndBoostStatus(BoostType.Jetpack);

        _movingController.StopFlying();
        _visual.StopJetpack();
    }

    //**********************************************************************************//

    private void StartMagnet()
    {
        StopCoroutine(_magnetCoroutine);

        _ui.EndBoostStatus(BoostType.Magnet);
        _isMagnetWorking = false;
        _visual.StopMagnet();

        _magnetCoroutine = MagnetLogic();
        StartCoroutine(_magnetCoroutine);
    }

    private IEnumerator MagnetLogic()
    {
        _isMagnetWorking = true;
        _visual.StartMagnet();

        _ui.StartBoostStatus(BoostType.Magnet, _characterData.MagnetDuration);
        yield return new WaitForSeconds(1.0f);

        for (int i = _characterData.MagnetDuration - 1; i > 0; --i)
        {
            _ui.StartBoostStatus(BoostType.Magnet, i);
            yield return new WaitForSeconds(1.0f);
        }
        _ui.EndBoostStatus(BoostType.Magnet);

        _isMagnetWorking = false;
        _visual.StopMagnet();
    }

    public bool IsMagnetWorking() { return _isMagnetWorking; }

    //**********************************************************************************//

    private void StartWeapon(WeaponType type)
    {
        _attackController.SetWeapon(type);
        StartCoroutine(WeaponLogic(type));
    }

    private IEnumerator WeaponLogic(WeaponType type)
    {
        BoostType boostType = (type == WeaponType.Laser) ? BoostType.WeaponLaser : BoostType.WeaponRocket;
        _ui.StartBoostStatus(boostType, 0);
        yield return new WaitForSeconds(1.0f);
        _ui.EndBoostStatus(boostType);
    }

    //**********************************************************************************//

    private void StartArmor()
    {
        StopCoroutine(_armorCoroutine);

        _ui.EndBoostStatus(BoostType.Armor);
        _isHasArmor = false;
        _visual.StopArmor();

        _armorCoroutine = ArmorLogic();
        StartCoroutine(_armorCoroutine);
    }

    private void StopArmor()
    {
        StopCoroutine(_armorCoroutine);

        _ui.EndBoostStatus(BoostType.Armor);
        _isHasArmor = false;
        _visual.StopArmor();
    }

    private IEnumerator ArmorLogic()
    {
        _isHasArmor = true;
        _visual.StartArmor();

        _ui.StartBoostStatus(BoostType.Armor, _characterData.ArmorDuration);
        yield return new WaitForSeconds(1.0f);

        for (int i = _characterData.ArmorDuration - 1; i > 0; --i)
        {
            if (!_isHasArmor) { break; }
            _ui.SetBoostStatus(BoostType.Armor, i);
            yield return new WaitForSeconds(1.0f);
        }
        _ui.EndBoostStatus(BoostType.Armor);
        _isHasArmor = false;
        _visual.StopArmor();
    }

    //**********************************************************************************//

    private void StartCoin()
    {
        ++_coinCount;
        _ui.SetCoinCount(_coinCount); 
    }
    


    void Update()
    {
        SetCameraHeight();
        SwitchBorder();
    }

    private void SetCameraHeight()
    {
        if (transform.position.y > _maxHeightChracterPosition) {
            _maxHeightChracterPosition = transform.position.y;
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                _maxHeightChracterPosition + _deltaCameraCharacter, _camera.transform.position.z );
        }
    }

    private void SwitchBorder()
    {
        if (transform.position.x <= 0)
        {
            transform.position = new Vector3(
                transform.position.x + _lvlWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= _lvlWidth)
        {
            transform.position = new Vector3(
                transform.position.x - _lvlWidth, transform.position.y, transform.position.z);
        }
    }
}
