using UnityEngine;
using System;
using System.Collections;

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
    private float _rayDistance;

    private void Start()
    {
        _rayDistance = (float)Math.Sqrt(1.0d + Camera.main.aspect) * Camera.main.orthographicSize * 2.0f;
    }

    public void SetWeapon(WeaponType type)
    {
        _weaponType = type;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            switch (_weaponType)
            {
                case WeaponType.Gun:
                    //if (Time.time - _lastShootTime > _gunPeriod) {
                        StartGun(ref target);
                    //}
                    _lastShootTime = Time.time;
                    break;
                case WeaponType.Laser:
                    if (Time.time - _lastShootTime > _laserPeriod) {
                        StartLaser(ref target);
                    }
                    _lastShootTime = Time.time;
                    break;
                case WeaponType.Rocket:
                    if (Time.time - _lastShootTime > _rocketPeriod) {
                        _lastShootTime = Time.time;
                    }
                    break;
            }
        }
    }

    private void StartGun(ref Vector3 target)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, target.z);
        GameObject projectile = _factory.GetProjectile(_weaponType);

        projectile.transform.position = transform.position;
        
        float angle = (target.x - pos.x > 0.0f) ?
            (float)(Math.Atan((target.y - pos.y)/ (target.x - pos.x) ) * 180f / Math.PI) :
            180f + (float)(Math.Atan((target.y - pos.y) / (target.x - pos.x)) * 180f / Math.PI);
        Quaternion quat = Quaternion.Euler(0.0f, 0.0f, angle);
   
        projectile.transform.rotation *= quat;
        
        projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _gunSpeed;
    }

    private void StartLaser(ref Vector3 target)
    {
        Vector2 startLaserPoint = new Vector2(transform.position.x, transform.position.y);
        Vector2 endLaserPoint = Vector2.zero;
        RaycastHit2D[] hits = Physics2D.RaycastAll(startLaserPoint, new Vector2(target.x, target.y), _rayDistance);
        
        foreach (var hit in hits) 
        {
            if (hit.collider.CompareTag(Tag.Border)) 
            {
                endLaserPoint = new Vector2(
                    hit.collider.transform.position.x,
                    hit.collider.transform.position.y);
                break;
            }
        }

        Vector2 middleLaserPoint = new Vector2(
            (endLaserPoint.x / 2 + startLaserPoint.x / 2),
            (endLaserPoint.y / 2 + startLaserPoint.y / 2));

        GameObject projectile = _factory.GetProjectile(_weaponType);

        projectile.transform.position = middleLaserPoint;
        projectile.transform.rotation = Quaternion.FromToRotation(startLaserPoint, endLaserPoint);

        Rect rect = projectile.GetComponent<Sprite>().rect;
        rect.height = (endLaserPoint - startLaserPoint).magnitude;
        rect.width = 0.2f;      

        Vector2[] rayPoints = new Vector2[] { 
            new Vector2(endLaserPoint.x - middleLaserPoint.x, endLaserPoint.y - middleLaserPoint.y), 
            new Vector2(middleLaserPoint.x - startLaserPoint.x, middleLaserPoint.y - startLaserPoint.y) };

        projectile.GetComponent<EdgeCollider2D>().points = rayPoints;

        StartCoroutine(DestroyLaserRay(projectile));
    } 

    private IEnumerator DestroyLaserRay(GameObject ray)
    {
        yield return new WaitForSeconds(_laserDuration);
        Destroy(ray);
    }

    private void StartRocket(ref Vector3 target)
    {
//        GameObject projectile = _factory.GetProjectile(_weaponType);

//        projectile.transform.position = transform.position;
//        projectile.transform.rotation = Quaternion.FromToRotation(transform.position, target);

//        projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _gunSpeed;
//       
    }
}
