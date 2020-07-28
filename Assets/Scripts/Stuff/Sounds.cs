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


    private void Start()
    {
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
    }
    public void StartBoostSound(BoostType type)
    {
        switch (type)
        {
            case BoostType.Trampoline:
                Play(_trampolineBoostSound);
                break;
            case BoostType.Jetpack:
                Play(_jetpackBoostSound);
                break;
            case BoostType.Magnet:
                Play(_magnetBoostSound);
                break;
            case BoostType.WeaponLaser:
                Play(_weaponBoostSound);
                break;
            case BoostType.WeaponRocket:
                Play(_weaponBoostSound);
                break;
            case BoostType.Armor:
                Play(_armorBoostSound);
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

    
    public void AttackSound(WeaponType type)
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
                Play(_barrierEnemySound);
                break;
            case EnemyType.Pusher:
                Play(_pusherEnemySound);
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



    public void Stop(AudioClip clip)
    {
        foreach (AudioSource source in _sources)
        {
            if (source.clip == clip)
            {
                source.Stop();
            }
        }
    }
    private void Play(AudioClip clip)
    {
        CheckEmpty();
        if(RestartPlayingClip(clip)) { return; }

        AudioSource audioSource = Resources.Load<AudioSource>(AssetPath.AudioSourcePrefab);

        audioSource.transform.position = transform.position;
        audioSource.clip = clip;
        audioSource.Play();

        _sources.Add(audioSource);
    }
    
    private void CheckEmpty()
    {
        for (int i = _sources.Count - 1; i >= 0; --i)
        {
            if (!_sources[i].isPlaying)
            {
                _sources.RemoveAt(i);
                continue;
            }
        }
    }
    private bool RestartPlayingClip(AudioClip clip)
    {
        foreach(AudioSource source in _sources)
        {
            if (source.clip == clip)
            {
                source.Stop();
                source.Play();
                return true;
            }
        }
        return false;
    }

}

