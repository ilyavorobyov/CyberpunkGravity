using UnityEngine;

public class BlackBlaster : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}