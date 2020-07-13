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

    private const string _platformTag = "Platform";
    private const string _coinTag = "Coin";
    private const string _boostTag = "Boost";
    private const string _enemyTag = "Enemy";

    private bool _isMagnetWorking = false;
    private bool _isHasArmor = false;
    private int _coinCount;

    [SerializeField] private float _forceJumpCoef;
    [SerializeField] private int _jetpackDuration;
    [SerializeField] private int _magnetDuration;
    [SerializeField] private int _armorDuration;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _deltaCameraCharacter = Camera.main.orthographicSize / 3;

        _gameLogic = FindObjectOfType<GameLogic>();
        _movingController = GetComponent<MovingController>();
        _movingController.SetForceCoef(_forceJumpCoef);
        _ui = FindObjectOfType<InGameUi>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_boostTag))
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
        else if (collision.CompareTag(_coinTag))
        {
            StartCoin();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(_enemyTag))
        {
            _gameLogic.Loss();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_boostTag) &&
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
        StartCoroutine(JetpackLogic());
    }

    private IEnumerator JetpackLogic()
    {
        _movingController.StartFlying();
        for (int i = _jetpackDuration; i > 0; --i)
        {
            _ui.BoostStatus($"Jetpack: {i}\n");
            yield return new WaitForSeconds(1.0f);
        }
        _ui.BoostStatus("");
        _movingController.StopFlying();
    }

    private void StartMagnet()
    {
        StartCoroutine(MagnetLogic());
    }

    private IEnumerator MagnetLogic()
    {
        _isMagnetWorking = true;
        for (int i = _magnetDuration; i > 0; --i)
        {
            _ui.BoostStatus($"Magnet: {i}\n");
            yield return new WaitForSeconds(1.0f);
        }
        _ui.BoostStatus("");
        _isMagnetWorking = false;
    }

    public bool IsMagnetWorking() { return _isMagnetWorking; }

    private void StartWeapon()
    {
        _attackController.SetWeapon(WeaponType.Laser);
        throw new NotImplementedException();
    }

    private IEnumerator WeaponLogic()
    {
        _ui.BoostStatus("Weapon Taken\n");
        yield return new WaitForSeconds(1.0f);
        _ui.BoostStatus("");
    }

    private void StartArmor()
    {
        StartCoroutine(ArmorLogic());
    }

    private IEnumerator ArmorLogic()
    {
        _isHasArmor = true;
        for (int i = _armorDuration; i > 0; --i)
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
