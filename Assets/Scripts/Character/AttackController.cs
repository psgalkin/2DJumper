using UnityEngine;

class AttackController : MonoBehaviour
{
    private WeaponType _weaponType = WeaponType.Gun;
    private ObjectsFactory _factory = new ObjectsFactory();

    [SerializeField] private float _gunSpeed;
    [SerializeField] private float _gunPeriod;
    [SerializeField] private float _laserDuration;
    [SerializeField] private float _laserPeriod;
    [SerializeField] private float _rocketSpeed;
    [SerializeField] private float _rocketPeriod;

    private float _lastShootTime;

    public void SetWeapon(WeaponType type)
    {
        _weaponType = type;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = Input.mousePosition;
            GameObject projectile = _factory.GetProjectile(_weaponType);
            projectile.transform.position = transform.position;
            projectile.GetComponent<Rigidbody2D>().velocity =
                (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _gunSpeed;

            _lastShootTime = Time.time;
        }
    }

}
