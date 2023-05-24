using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected WeaponViewObject WeaponView;

    [SerializeField] private string _label;
    [SerializeField] private int _energyConsuming;
    [SerializeField] private Sprite _icon;
    [SerializeField] protected PlayerBullet Bullet;

    public string Label => _label;
    public int EnergyConsuming => _energyConsuming;
    public Sprite Icon => _icon;

    public void Init(WeaponViewObject weaponView)
    {
        WeaponView = weaponView;
    }

    public abstract void Shoot();
}
