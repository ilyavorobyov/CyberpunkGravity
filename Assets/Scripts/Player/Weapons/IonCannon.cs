using UnityEngine;

public class IonCannon : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}