using UnityEngine;
class Rocket : MonoBehaviour
{
    public void StartExplosion()
    { 
        ObjectsFactory generator = new ObjectsFactory();
        GameObject explosion = generator.GetProjectile(WeaponType.Explosion);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
