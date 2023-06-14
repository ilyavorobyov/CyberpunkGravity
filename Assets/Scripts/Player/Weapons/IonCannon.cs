using UnityEngine;

public class IonCannon : Weapon
{
    public override void Shoot()
    {
        var bullet = Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}