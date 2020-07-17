using System;
using UnityEngine;
using Object = UnityEngine.Object;

class ObjectsFactory
{
    private GameObject _simplePlatform;
    private GameObject _brokeningPlatform;
    private GameObject _movingPlatform;
    private GameObject _trapPlatform;

    private GameObject _coin;

    private GameObject _barrierEnemy;
    private GameObject _pusherEnemy;

    private GameObject _jetpackBoost;
    private GameObject _trampolineBoost;
    private GameObject _magnetBoost;
    private GameObject _weaponLaserBoost;
    private GameObject _weaponRocketBoost;
    private GameObject _armorBoost;

    private GameObject _bullet;
    private GameObject _laserRay;
    private GameObject _rocket;
    private GameObject _explosion;

    private GameObject _character;

    private GameObject _bottomBorder;
    private GameObject _upperBorder;
    private GameObject _endLevelBorder;
    private GameObject _sideBorder;

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
            case PlatformType.Trap:
                platform = GetTrapPlatform();
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

    private GameObject GetTrapPlatform()
    {
        if (!_trapPlatform)
        {
            _trapPlatform = Resources.Load<GameObject>(AssetPath.Platforms[PlatformType.Trap]);
        }
        return _trapPlatform;
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

    public GameObject GetBoost(BoostType type, Transform transform)
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
            case BoostType.WeaponLaser:
                boost = GetWeaponLaserBoost();
                break;
            case BoostType.WeaponRocket:
                boost = GetWeaponRocketBoost();
                break;
            case BoostType.Armor:
                boost = GetArmorBoost();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(boost, transform);
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

    private GameObject GetWeaponLaserBoost()
    {
        if (!_weaponLaserBoost)
        {
            _weaponLaserBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.WeaponLaser]);
        }
        return _weaponLaserBoost;
    }

    private GameObject GetWeaponRocketBoost()
    {
        if (!_weaponRocketBoost)
        {
            _weaponRocketBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.WeaponRocket]);
        }
        return _weaponRocketBoost;
    }

    private GameObject GetArmorBoost()
    {
        if (!_armorBoost)
        {
            _armorBoost = Resources.Load<GameObject>(AssetPath.Boosts[BoostType.Armor]);
        }
        return _armorBoost;
    }

    public GameObject GetCharacter()
    {
        if (!_character)
        {
            _character = Resources.Load<GameObject>(AssetPath.Character);
        }
        return UnityEngine.Object.Instantiate(_character);
    }

    public GameObject GetProjectile(WeaponType type)
    {
        GameObject projectile;
        switch(type)
        {
            case WeaponType.Gun:
                projectile = GetGunProjectile();
                break;
            case WeaponType.Laser:
                projectile = GetLaserProjectile();
                break;
            case WeaponType.Rocket:
                projectile = GetRocketProjectile();
                break;
            case WeaponType.Explosion:
                projectile = GetExplosion();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(projectile);
    }

    private GameObject GetGunProjectile()
    {
        if (!_bullet)
        {
            _bullet = Resources.Load<GameObject>(AssetPath.Projectiles[WeaponType.Gun]);
        }
        return _bullet;
    }

    private GameObject GetLaserProjectile()
    {
        if (!_laserRay)
        {
            _laserRay = Resources.Load<GameObject>(AssetPath.Projectiles[WeaponType.Laser]);
        }
        return _laserRay;
    }

    private GameObject GetRocketProjectile()
    {
        if (!_rocket)
        {
            _rocket = Resources.Load<GameObject>(AssetPath.Projectiles[WeaponType.Rocket]);
        }
        return _rocket;
    }

    private GameObject GetExplosion()
    {
        if (!_explosion)
        {
            _explosion = Resources.Load<GameObject>(AssetPath.Projectiles[WeaponType.Explosion]);
        }
        return _explosion;
    }

    public GameObject GetBorder(BorderType type)
    {
        GameObject border;
        switch (type)
        {
            case BorderType.Upper:
                border = GetUpperBorder();
                break;
            case BorderType.Bottom:
                border = GetBottomBorder();
                break;
            case BorderType.EndLevel:
                border = GetEndLevelBorder();
                return UnityEngine.Object.Instantiate(border);
                break;
            case BorderType.Side:
                border = GetSideBorder();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return UnityEngine.Object.Instantiate(border, Camera.main.transform);
    }

    private GameObject GetUpperBorder()
    {
        if (!_upperBorder)
        {
            _upperBorder = Resources.Load<GameObject>(AssetPath.Borders[BorderType.Upper]);
        }
        return _upperBorder;
    }

    private GameObject GetBottomBorder()
    {
        if (!_bottomBorder)
        {
            _bottomBorder = Resources.Load<GameObject>(AssetPath.Borders[BorderType.Bottom]);
        }
        return _bottomBorder;
    }

    private GameObject GetEndLevelBorder()
    {
        if (!_endLevelBorder)
        {
            _endLevelBorder = Resources.Load<GameObject>(AssetPath.Borders[BorderType.EndLevel]);
        }
        return _endLevelBorder;
    }

    private GameObject GetSideBorder()
    {
        if (!_sideBorder)
        {
            _sideBorder = Resources.Load<GameObject>(AssetPath.Borders[BorderType.Side]);
        }
        return _sideBorder;
    }
}
