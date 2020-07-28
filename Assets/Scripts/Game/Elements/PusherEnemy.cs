using UnityEngine;
using System;

class PusherEnemy : Enemy
{
    private float _attackPeriod = 0.5f;
    private float _attackSpeed = 6.5f;
    private Vector3 _attackRotation = Vector3.right;
    private float _lastHitTime;
    private ObjectsFactory _factory;
    private bool _isActive = false;

    private void Start()
    {
        _factory = new ObjectsFactory();
        _lastHitTime = Time.time;
    }

    public void SetAttackPeriod(float period)
    {
        _attackPeriod = period;
        //_attackRotation = Quaternion.FromToRotation(Vector3.zero, Vector3.up);
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
    }

    public void SetActive()
    {
        _isActive = true;
    }

    private void Update()
    {
        if (Time.time - _lastHitTime > _attackPeriod)
        {
            if (!_isActive) { return; }

            double x = _attackRotation.x;
            double y = _attackRotation.y;
            _attackRotation.x = //(x > 0d) ?
                (float)(x * Math.Cos(15d) - y * Math.Sin(15d)); //: (float)(x * Math.Cos(15d) + y * Math.Sin(15d));
            _attackRotation.y = //(x > 0d) ?
                (float)(x * Math.Sin(15d) + y * Math.Cos(15d));// : (float)(-x * Math.Sin(15d) + y * Math.Cos(15d));
           
            
            Hit(_attackRotation + gameObject.transform.position);
            _lastHitTime = Time.time;
        }
    }

    private void Hit(Vector3 target)
    {
        GameObject enemyProjectile = _factory.GetEnemy(EnemyType.Hit);
        Sounds.Instance.StartEnemySound(EnemyType.Hit);
        enemyProjectile.transform.position = transform.position;

        float angle = (target.x - transform.position.x > 0.0f) ?
            (float)(Math.Atan((target.y - transform.position.y) / (target.x - transform.position.x)) * 180f / Math.PI) :
            180f + (float)(Math.Atan((target.y - transform.position.y) / (target.x - transform.position.x)) * 180f / Math.PI);
        Quaternion quat = Quaternion.Euler(0.0f, 0.0f, angle);

        enemyProjectile.transform.rotation *= quat;


        enemyProjectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _attackSpeed;
    }
}
