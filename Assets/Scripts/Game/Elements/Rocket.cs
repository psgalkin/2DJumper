using UnityEngine;
class Rocket : MonoBehaviour
{
    public void StartExplosion()
    { 
        ObjectsFactory generator = new ObjectsFactory();
        GameObject explosion = generator.GetProjectile(WeaponType.Explosion);
        explosion.transform.position = //transform.position;
            new Vector3(transform.position.x, transform.position.y, explosion.transform.position.z);
        Destroy(gameObject);
    }
}
