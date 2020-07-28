using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

class AttackController : MonoBehaviour
{
    private WeaponType _weaponType = WeaponType.Rocket;
    private ObjectsFactory _factory = new ObjectsFactory();

    [SerializeField] private float _gunSpeed;
    [SerializeField] private float _gunPeriod;
    [SerializeField] private float _laserDuration;
    [SerializeField] private float _laserPeriod;
    [SerializeField] private float _rocketSpeed;
    [SerializeField] private float _rocketPeriod;

    private Camera _camera;

    private float _lastShootTime;
    private float _rayDistance;

    private void Start()
    {
        _rayDistance = (float)Math.Sqrt(1.0d + Camera.main.aspect) * Camera.main.orthographicSize * 2.0f;
        _camera = Camera.main;
    }

    public void SetWeapon(WeaponType type)
    {
        _weaponType = type;
    }


    private bool IsPointerOverUiObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }

        return false;
    }


    private void Update()
    {
        Debug.Log(Time.time - _lastShootTime);
        if (Input.GetMouseButtonDown(0))            
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            if (IsPointerOverUiObject()) { return; } 
                
                
            switch (_weaponType)
            {
                case WeaponType.Gun:
                    if (Time.time - _lastShootTime > _gunPeriod) {
                        Sounds.Instance.StartAttackSound(WeaponType.Gun);
                        StartGun(ref target);
                        _lastShootTime = Time.time;
                    }
                    
                    break;
                case WeaponType.Laser:
                    if (Time.time - _lastShootTime > _laserPeriod) {
                        Sounds.Instance.StartAttackSound(WeaponType.Laser);
                        StartLaser(ref target);
                        _lastShootTime = Time.time;
                    }
                    
                    break;
                case WeaponType.Rocket:
                    Sounds.Instance.StartAttackSound(WeaponType.Rocket);
                    if (Time.time - _lastShootTime > _rocketPeriod) {
                        StartRocket(ref target);
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
            (float)(Math.Atan((target.y - pos.y) / (target.x - pos.x) ) * 180f / Math.PI) :
            180f + (float)(Math.Atan((target.y - pos.y) / (target.x - pos.x)) * 180f / Math.PI);
        Quaternion quat = Quaternion.Euler(0.0f, 0.0f, angle);
   
        projectile.transform.rotation *= quat;
        
        projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _gunSpeed;
    }

    private void StartLaser(ref Vector3 target)
    {
        Vector2 startLaserPoint = new Vector2(transform.position.x, transform.position.y);
        Vector2 endLaserPoint = new Vector2(target.x, target.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(startLaserPoint, new Vector2(target.x - transform.position.x, target.y - transform.position.y));
        
        foreach (var hit in hits) 
        {
            if (hit.collider.CompareTag(Tag.Border))  
            {
                endLaserPoint = new Vector2(
                    hit.point.x,
                    hit.point.y);
                break;
            }
        }

        // берем центральную точку
        Vector2 middleLaserPoint = new Vector2(
            (endLaserPoint.x / 2 + startLaserPoint.x / 2),
            (endLaserPoint.y / 2 + startLaserPoint.y / 2));

        GameObject projectile = _factory.GetProjectile(_weaponType);

        // генерим в нее объект
        projectile.transform.position = middleLaserPoint;

        // раворачиваем объект чтобы развернулся спрайт
        float angle = (endLaserPoint.x - middleLaserPoint.x > 0.0f) ?
            (float)(Math.Atan((endLaserPoint.y - middleLaserPoint.y) / (endLaserPoint.x - middleLaserPoint.x)) * 180f / Math.PI) :
            180f + (float)(Math.Atan((endLaserPoint.y - middleLaserPoint.y) / (endLaserPoint.x - middleLaserPoint.x)) * 180f / Math.PI);
        
        Quaternion quat = Quaternion.Euler(0.0f, 0.0f, angle);

        projectile.GetComponentInChildren<SpriteRenderer>().transform.rotation *= quat;

        // крутим масштабы спрайта
        float spriteX = projectile.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        float spriteY = projectile.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        float coefX = (endLaserPoint - startLaserPoint).magnitude / spriteY;
        ///float coefY = 0.2f / spriteX;

        projectile.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale =
            new Vector3(coefX, 0.5f, 1.0f);

        // добавляем коллайдер
        Vector2[] rayPoints = new Vector2[] { 
            new Vector2(endLaserPoint.x - middleLaserPoint.x, endLaserPoint.y - middleLaserPoint.y), 
            new Vector2(startLaserPoint.x - middleLaserPoint.x, startLaserPoint.y - middleLaserPoint.y) };

        projectile.GetComponent<EdgeCollider2D>().points = rayPoints;
             


        StartCoroutine(DestroyLaserRay(projectile));
    } 

    private IEnumerator DestroyLaserRay(GameObject ray)
    {
        yield return new WaitForSeconds(_laserDuration);
        Destroy(ray);
    }

    private void StartRocket(ref Vector3 pointTarget)
    {
        Vector3 target = (TargetManager.GetEnemy(transform)) ?
            TargetManager.GetEnemy(transform).transform.position : pointTarget;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, target.z);
        GameObject projectile = _factory.GetProjectile(_weaponType);

        projectile.transform.position = transform.position;

        float angle = (target.x - pos.x > 0.0f) ?
            (float)(Math.Atan((target.y - pos.y) / (target.x - pos.x)) * 180f / Math.PI) :
            180f + (float)(Math.Atan((target.y - pos.y) / (target.x - pos.x)) * 180f / Math.PI);
        Quaternion quat = Quaternion.Euler(0.0f, 0.0f, angle);

        projectile.transform.rotation *= quat;

        projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(target.x - transform.position.x, target.y - transform.position.y)).normalized * _gunSpeed;

    }
}
