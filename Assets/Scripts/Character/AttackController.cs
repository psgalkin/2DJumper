using UnityEngine;

class AttackController : MonoBehaviour
{
    private WeaponType _weaponType = WeaponType.Gun;

    public void SetWeapon(WeaponType type)
    {
        _weaponType = type;
    }
}
