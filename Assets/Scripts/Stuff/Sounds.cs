using System.Collections.Generic;
using UnityEngine;

public class Sounds : SingletonMonoBehaviour<Sounds>
{
    private List<AudioSource> _sources = new List<AudioSource>();

    private AudioClip _jumpSound;
    private AudioClip _brokeningSound;
    private AudioClip _trapSound;
    private AudioClip _movingSound;

    private AudioClip _trampolineBoostSound;
    private AudioClip _magnetBoostSound;
    private AudioClip _jetpackBoostSound;
    private AudioClip _armorBoostSound;
    private AudioClip _weaponBoostSound;

    private AudioClip _simpleAttackSound;
    private AudioClip _laserAttackSound;
    private AudioClip _rocketAttackSound;
    private AudioClip _explosionSound;

    private AudioClip _barrierEnemySound;
    private AudioClip _pusherEnemySound;
    private AudioClip _enemyHitSound;

    private AudioClip _coinSound;

    private void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            AudioSource audioSource = Resources.Load<AudioSource>(AssetPath.AudioSourcePrefab);
            AudioSource newSource = Instantiate<AudioSource>(audioSource, transform);
            newSource.transform.position = transform.position;
            _sources.Add(newSource);
        }

        _jumpSound = Resources.Load<AudioClip>(AssetPath.Sounds.SimpleJump);
        _brokeningSound = Resources.Load<AudioClip>(AssetPath.Sounds.BrokeningJump);
        _trapSound = Resources.Load<AudioClip>(AssetPath.Sounds.TrapJump);
        _movingSound = Resources.Load<AudioClip>(AssetPath.Sounds.MovingJump);

        _trampolineBoostSound = Resources.Load<AudioClip>(AssetPath.Sounds.TrampolineBoost);
        _magnetBoostSound = Resources.Load<AudioClip>(AssetPath.Sounds.MagnetBoost);
        _jetpackBoostSound = Resources.Load<AudioClip>(AssetPath.Sounds.JetpackBoost);
        _armorBoostSound = Resources.Load<AudioClip>(AssetPath.Sounds.ArmorBoost);
        _weaponBoostSound = Resources.Load<AudioClip>(AssetPath.Sounds.WeaponBoost);

        _simpleAttackSound = Resources.Load<AudioClip>(AssetPath.Sounds.SimpleAttack);
        _laserAttackSound = Resources.Load<AudioClip>(AssetPath.Sounds.LaserAttack);
        _rocketAttackSound = Resources.Load<AudioClip>(AssetPath.Sounds.RocketAttack);
        _explosionSound = Resources.Load<AudioClip>(AssetPath.Sounds.Explosion);

        _barrierEnemySound = Resources.Load<AudioClip>(AssetPath.Sounds.BarrierEnemy);
        _pusherEnemySound = Resources.Load<AudioClip>(AssetPath.Sounds.PusherEnemy);
        _enemyHitSound = Resources.Load<AudioClip>(AssetPath.Sounds.EnemyHit);

        _coinSound = Resources.Load<AudioClip>(AssetPath.Sounds.Coin);
    }

    public void StartJumpSound(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Simple:
                Play(_jumpSound);
                break;
            case PlatformType.Brokening:
                Play(_brokeningSound);
                break;
            case PlatformType.Moving:
                Play(_movingSound);
                break;
            case PlatformType.Trap:
                Play(_trapSound);
                break;
        }
    }
    public void StartBoostSound(BoostType type)
    {
        switch (type)
        {
            case BoostType.Trampoline:
                Play(_trampolineBoostSound);
                break;
            case BoostType.Jetpack:
                Play(_jetpackBoostSound, true);
                break;
            case BoostType.Magnet:
                Play(_magnetBoostSound, true);
                break;
            case BoostType.WeaponLaser:
                Play(_weaponBoostSound);
                break;
            case BoostType.WeaponRocket:
                Play(_weaponBoostSound);
                break;
            case BoostType.Armor:
                Play(_armorBoostSound, true);
                break;
        }
    }

    public void StopBoostSound(BoostType type)
    {
        switch (type)
        {
            case BoostType.Jetpack:
                Stop(_jetpackBoostSound);
                break;
            case BoostType.Magnet:
                Stop(_magnetBoostSound);
                break;
            case BoostType.Armor:
                Stop(_armorBoostSound);
                break;
            default:
                Debug.Log("Wrong Sound Stopped");
                break;
        }
    }

    
    public void StartAttackSound(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Gun:
                Play(_simpleAttackSound);
                break;
            case WeaponType.Laser:
                Play(_laserAttackSound);
                break;
            case WeaponType.Rocket:
                Play(_rocketAttackSound);
                break;
            case WeaponType.Explosion:
                Play(_explosionSound);
                break;
            default:
                break;
        }
    }

    public void StartEnemySound(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Barrier:
                Play(_barrierEnemySound, true);
                break;
            case EnemyType.Pusher:
                Play(_pusherEnemySound, true);
                break;
            case EnemyType.Hit:
                Play(_enemyHitSound);
                break;
            default:
                break;
        }
    }

    public void StopEnemySound(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Barrier:
                Stop(_barrierEnemySound);
                break;
            case EnemyType.Pusher:
                Stop(_pusherEnemySound);
                break;
            default:
                break;
        }
    }

    public void StartCoinSound()
    {
        Play(_coinSound);
    }

    private void Stop(AudioClip clip)
    {
        foreach (AudioSource source in _sources)
        {
            if (source.clip == clip)
            {
                source.loop = false;
                source.Stop();
            }
        }
    }
    private void Play(AudioClip clip, bool isLooping = false)
    {
        if (RestartPlayingClip(clip)) { return; } 
        foreach (AudioSource source in _sources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                if (isLooping) { source.loop = true; }
                return;
            }
        }
        Debug.Log("Too much sounds");
    }
    

    private bool RestartPlayingClip(AudioClip clip)
    {
        foreach(AudioSource source in _sources)
        {
            if (source.clip == clip &&
                source.isPlaying)
            {
                source.Stop();
                source.Play();
                return true;
            }
        }
        return false;
    }

}

