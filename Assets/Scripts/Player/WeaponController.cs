using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;
using Color = UnityEngine.Color;

[RequireComponent(typeof(PlayerCollisionHandler))]
public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private TMP_Text _batteryValueText;
    [SerializeField] private float _changeEnergyTextEffectTime;

    private bool _onMenu;
    private float _batteryTextMaxFontSize = 60;
    private float _regularTextSize = 40;
    private Color _regularEnergyText—olor;
    private Color _changeEnergyColorLacks;
    private Color _changeEnergyColorAdd;
    private Coroutine _changeEnergyTextColor;
    private PlayerCollisionHandler _playerCollisionHandler;
    private int _currentWeaponNumber = 0;
    private Weapon _currentWeapon;
    private int _batteryValue;
    private WeaponViewObject _weaponViewObject;

    public event UnityAction<string, Sprite> WeaponChange;

    private void Awake()
    {
        _onMenu = true;
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _batteryValue = 10;
        _weaponViewObject = GetComponentInChildren<WeaponViewObject>();
        _currentWeapon = _weapons[0];
        _currentWeapon.Init(_weaponViewObject);
        CalculateAvailableNumberOfShots();
    }

    private void Start()
    {
        _weaponViewObject.SetSprite(_currentWeapon.Icon);
        _regularEnergyText—olor = _batteryValueText.color;
        _changeEnergyColorLacks = Color.red;
        _changeEnergyColorAdd = Color.green;
        WeaponChange?.Invoke(_currentWeapon.Label, _currentWeapon.Icon);
    }

    public void ChangeOnMenuValue(bool value)
    {
        _onMenu = value;
    }
    private void AddEnergy(int energyPoints)
    {
        _batteryValue += energyPoints;
        CalculateAvailableNumberOfShots();
        StartTextEffectCoroutine(_changeEnergyColorAdd);
    }

    private void CalculateAvailableNumberOfShots()
    {
        int numberOfShots = _batteryValue / _currentWeapon.EnergyConsuming;
        _batteryValueText.text = numberOfShots.ToString();
    }

    private void OnShoot()
    {
        if (!_onMenu)
        {
            int energyConsuming = _currentWeapon.EnergyConsuming;

            if (energyConsuming <= _batteryValue)
            {
                _currentWeapon.Shoot();
                _batteryValue -= energyConsuming;
                CalculateAvailableNumberOfShots();
            }
            else
            {
                StartTextEffectCoroutine(_changeEnergyColorLacks);
            }
        }
    }

    private void StartTextEffectCoroutine(Color color)
    {
        if (_changeEnergyTextColor != null)
        {
            StopCoroutine(_changeEnergyTextColor);
        }

        _changeEnergyTextColor = StartCoroutine(ChangeEnergyTextColor(color));
    }

    private void OnNextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
        {
            _currentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        CalculateAvailableNumberOfShots();
    }

    private void OnPreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        CalculateAvailableNumberOfShots();
    }

    private void ChangeWeapon(Weapon weapon)
    {
        WeaponChange?.Invoke(weapon.Label, weapon.Icon);
        _currentWeapon = weapon;
        _currentWeapon.Init(_weaponViewObject);
        _weaponViewObject.SetSprite(_currentWeapon.Icon);
    }

    private IEnumerator ChangeEnergyTextColor(Color temp—olor)
    {
        var waitForSeconds = new WaitForSeconds(_changeEnergyTextEffectTime);
        _batteryValueText.color = temp—olor;
        _batteryValueText.fontSize = _batteryTextMaxFontSize;
        yield return waitForSeconds;
        _batteryValueText.color = _regularEnergyText—olor;
        _batteryValueText.fontSize = _regularTextSize;
    }

    private void OnEnable()
    {
        _playerCollisionHandler.BatteryTaken += AddEnergy;
    }

    private void OnDisable()
    {
        _playerCollisionHandler.BatteryTaken -= AddEnergy;
    }
}