using UnityEngine;

public class PlasmaBlaster : Weapon
{
    public override void Shoot()
    {
        var bullet = Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}