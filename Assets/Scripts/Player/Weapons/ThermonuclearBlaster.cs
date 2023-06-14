using UnityEngine;

public class ThermonuclearBlaster : Weapon
{
    public override void Shoot()
    {
        var bullet = Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}