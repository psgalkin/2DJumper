using UnityEngine;

class StrikerEnemy : Enemy
{
    private float _attackPeriod;
    private Quaternion _attackRotation;
    private float _lastHitTime;


    public void SetAttackPeriod(float period)
    {
        _attackPeriod = period;
        _attackRotation = Quaternion.FromToRotation(Vector3.zero, Vector3.up);
    }

    private void Update()
    {
        if (Time.time - _lastHitTime > _attackPeriod)
        {
            Hit();
        }
    }

    private void Hit()
    {

    }
}
