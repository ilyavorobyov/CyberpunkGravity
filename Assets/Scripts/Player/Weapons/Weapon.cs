using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected PlayerBullet Bullet;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private string _label;
    [SerializeField] private int _energyConsuming;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isBuyed;

    protected WeaponViewObject WeaponView;

    public string Label => _label;
    public int EnergyConsuming => _energyConsuming;
    public Sprite Icon => _icon;
    public Sprite Sprite => _sprite;
    public bool IsBuyed => _isBuyed;
    public AudioClip ShootSound => _shootSound;

    public void Init(WeaponViewObject weaponView)
    {
        WeaponView = weaponView;
    }

    public void Buying()
    {
        _isBuyed = true;
    }

    public abstract void Shoot();
}