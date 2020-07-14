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

    private bool _isMagnetWorking = false;
    private bool _isHasArmor = false;
    private int _coinCount;

    [SerializeField] private CharacterData _characterData;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _deltaCameraCharacter = Camera.main.orthographicSize / 3;

        _gameLogic = FindObjectOfType<GameLogic>();

        _movingController = GetComponent<MovingController>();
        _movingController.SetForceCoef(_characterData.ForceJumpCoef);
        _movingController.SetFlyingSpeed(_characterData.FlyingSpeed);

        _attackController = GetComponent<AttackController>();

        _ui = FindObjectOfType<InGameUi>();
        _visual = GetComponent<Visual>();
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
                case BoostType.Weapon:
                    StartWeapon();
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

    private void StartJetpack()
    {
        StopCoroutine(JetpackLogic());
        StartCoroutine(JetpackLogic());
    }

    private IEnumerator JetpackLogic()
    {
        
        _movingController.StartFlying();
        _visual.StartJetpack();
        
        for (int i = _characterData.JetpackDuration; i > 0; --i)
        {
            _ui.BoostStatus($"Jetpack: {i}\n");
            yield return new WaitForSeconds(1.0f);
        }
        _ui.BoostStatus("");

        _movingController.StopFlying();
        _visual.StopJetpack();
    }

    private void StartMagnet()
    {
        StopCoroutine(MagnetLogic());
        StartCoroutine(MagnetLogic());
    }

    private IEnumerator MagnetLogic()
    {
        _isMagnetWorking = true;
        _visual.StartMagnet();

        for (int i = _characterData.MagnetDuration; i > 0; --i)
        {
            _ui.BoostStatus($"Magnet: {i}\n");
            yield return new WaitForSeconds(1.0f);
        }
        _ui.BoostStatus("");

        _isMagnetWorking = false;
        _visual.StopMagnet();
    }

    public bool IsMagnetWorking() { return _isMagnetWorking; }

    private void StartWeapon()
    {
        _attackController.SetWeapon(WeaponType.Laser);
    }

    private IEnumerator WeaponLogic()
    {
        _ui.BoostStatus("Weapon Taken\n");
        yield return new WaitForSeconds(1.0f);
        _ui.BoostStatus("");
    }

    private void StartArmor()
    {
        StopCoroutine(ArmorLogic());
        StartCoroutine(ArmorLogic());
    }

    private void StopArmor()
    {
        StopCoroutine(ArmorLogic());

        _ui.BoostStatus("");
        _isHasArmor = false;
    }

    private IEnumerator ArmorLogic()
    {
        _isHasArmor = true;
        for (int i = _characterData.ArmorDuration; i > 0; --i)
        {
            if (!_isHasArmor) { break; }
            _ui.BoostStatus($"Armor: {i}\n");
            yield return new WaitForSeconds(1.0f);
        }
        _ui.BoostStatus("");
        _isHasArmor = false;
    }

    private void StartCoin()
    {
        ++_coinCount;
        _ui.SetCoinCount(_coinCount); 
    }
    
    void Update()
    {
        SetCameraHeight();
    }

    private void SetCameraHeight()
    {
        if (transform.position.y > _maxHeightChracterPosition) {
            _maxHeightChracterPosition = transform.position.y;
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                _maxHeightChracterPosition + _deltaCameraCharacter, _camera.transform.position.z );
        }
    }
}
