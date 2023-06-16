using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlaster : Weapon
{
    public override void Shoot()
    {
        var bullet = Instantiate(Bullet, WeaponView.transform.position, Quaternion.identity);
    }
}