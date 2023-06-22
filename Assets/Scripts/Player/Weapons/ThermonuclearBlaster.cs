using UnityEngine;

public class ThermonuclearBlaster : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}