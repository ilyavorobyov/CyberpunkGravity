using UnityEngine;

public class IonCannon : Weapon
{
    public override void Shoot(float speed)
    {
        var bullet = Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
        bullet.Init(speed);
    }
}