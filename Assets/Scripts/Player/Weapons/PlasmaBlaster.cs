using UnityEngine;

public class PlasmaBlaster : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}