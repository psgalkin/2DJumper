using System;
using UnityEngine;
using Object = UnityEngine.Object;

class ObjectsFactory
{
    private GameObject _simplePlatform;
    private GameObject _brokeningPlatform;
    private GameObject _movingPlatform;

    private GameObject _coin;

    private GameObject _barrierEnemy;
    private GameObject _pusherEnemy;

    private GameObject _jetpackBoost;
    private GameObject _trampolineBoost;
    private GameObject _magnetBoost;
    private GameObject _weaponBoost;
    private GameObject _armorBoost;

    public GameObject GetPlatform(PlatformType type)
    {
        GameObject platform;
        switch (type)
        {
            case PlatformType.Simple:
                platform = GetSimplePlatform();
                break;
            case PlatformType.Brokening:
                platform = GetBrokeningPlatform();
                break;
            case PlatformType.Moving:
                platform = GetMovingPlatform();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(platform);
    }

    public Vector3 PlatformSize()
    {
        _simplePlatform = Resources.Load<GameObject>(AssetPath.Platforms[PlatformType.Simple]);
        BoxCollider2D collider = _simplePlatform.GetComponent<BoxCollider2D>();
        return collider.size * collider.transform.localScale;
    }

    public float CoinRadius()
    {
        _coin = Resources.Load<GameObject>(AssetPath.Coin);
        CircleCollider2D collider = _coin.GetComponent<CircleCollider2D>();
        return collider.radius * collider.transform.localScale.x;
    }

    private GameObject GetSimplePlatform()
    {
        if (!_simplePlatform)
        {
            _simplePlatform = Resources.Load<GameObject>(AssetPath.Platforms[PlatformType.Simple]);
        }
        return _simplePlatform;
    }

    private GameObject GetBrokeningPlatform()
    {
        if (!_brokeningPlatform)
        {
            _brokeningPlatform = Resources.Load<GameObject>(AssetPath.Platforms[PlatformType.Brokening]);
        }
        return _brokeningPlatform;
    }

    private GameObject GetMovingPlatform()
    {
        if (!_movingPlatform)
        {
            _movingPlatform = Resources.Load<GameObject>(AssetPath.Platforms[PlatformType.Moving]);
        }
        return _movingPlatform;
    }

    public GameObject GetCoin()
    {
        if (!_coin)
        {
            _coin = Resources.Load<GameObject>(AssetPath.Coin);
        }

        return UnityEngine.Object.Instantiate(_coin);
    }

    public GameObject GetEnemy(EnemyType type)
    {
        GameObject enemy;
        switch (type)
        {
            case EnemyType.Barrier:
                enemy = GetBarrierEnemy();
                break;
            case EnemyType.Pusher:
                enemy = GetPusherEnemy();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(enemy);
    }

    private GameObject GetBarrierEnemy()
    {
        if (!_barrierEnemy)
        {
            _barrierEnemy = Resources.Load<GameObject>(AssetPath.Enemys[EnemyType.Barrier]);
        }
        return _barrierEnemy;
    }

    private GameObject GetPusherEnemy()
    {
        if (!_pusherEnemy)
        {
            _pusherEnemy = Resources.Load<GameObject>(AssetPath.Enemys[EnemyType.Pusher]);
        }
        return _pusherEnemy;
    }

    public GameObject GetBoost(BoostType type)
    {
        GameObject boost;
        switch (type)
        {
            case BoostType.Jetpack:
                boost = GetJetpackBoost();
                break;
            case BoostType.Trampoline:
                boost = GetTrampolineBoost();
                break;
            case BoostType.Magnet:
                boost = GetMagnetBoost();
                break;
            case BoostType.Weapon:
                boost = GetWeaponBoost();
                break;
            case BoostType.Armor:
                boost = GetArmorBoost();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(boost);
    }

    private GameObject GetJetpackBoost()
    {
        if (!_jetpackBoost)
        {
            _jetpackBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Jetpack]);
        }
        return _jetpackBoost;
    }

    private GameObject GetTrampolineBoost()
    {
        if (!_trampolineBoost)
        {
            _trampolineBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Trampoline]);
        }
        return _trampolineBoost;
    }

    private GameObject GetMagnetBoost()
    {
        if (!_magnetBoost)
        {
            _magnetBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Magnet]);
        }
        return _magnetBoost;
    }

    private GameObject GetWeaponBoost()
    {
        if (!_weaponBoost)
        {
            _weaponBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Weapon]);
        }
        return _weaponBoost;
    }

    private GameObject GetArmorBoost()
    {
        if (!_armorBoost)
        {
            _armorBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Armor]);
        }
        return _armorBoost;
    }
}
