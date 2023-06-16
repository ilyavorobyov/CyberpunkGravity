using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;

[RequireComponent(typeof(PlayerCollisionHandler))]
public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;
    [SerializeField] private List<Weapon> _availableWeapons;
    [SerializeField] private TMP_Text _batteryValueText;
    [SerializeField] private float _changeEnergyTextEffectTime;
    [SerializeField] private GameUIController _gameUIController;

    private bool _onMenu;
    private float _batteryTextMaxFontSize = 60;
    private float _regularTextSize = 40;
    private Color _regularEnergyText—olor;
    private Color _changeEnergyColorLacks;
    private Color _changeEnergyColorAdd;
    private Coroutine _changeEnergyTextColor;
    private PlayerCollisionHandler _playerCollisionHandler;
    private PlayerMover _playerMover;
    private int _currentWeaponNumber = 0;
    private Weapon _currentWeapon;
    private int _batteryValue;
    private int _startBatteryValue = 100;
    private WeaponViewObject _weaponViewObject;

    public event UnityAction<string, Sprite> WeaponChange;

    private void Awake()
    {
        CheckAvailableWeapons();
        _onMenu = true;
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _batteryValue = _startBatteryValue;
        _weaponViewObject = GetComponentInChildren<WeaponViewObject>();
        _playerMover = GetComponent<PlayerMover>();
        _currentWeapon = _availableWeapons[0];
        _currentWeapon.Init(_weaponViewObject);
        CalculateAvailableNumberOfShots();
    }

    private void Start()
    {
        _weaponViewObject.SetSprite(_currentWeapon.Sprite);
        _regularEnergyText—olor = _batteryValueText.color;
        _changeEnergyColorLacks = Color.red;
        _changeEnergyColorAdd = Color.green;
        WeaponChange?.Invoke(_currentWeapon.Label, _currentWeapon.Icon);
    }

    private void OnEnable()
    {
        _gameUIController.StartGame += StartGame;
        _playerCollisionHandler.BatteryTaken += AddEnergy;
    }

    private void OnDisable()
    {
        _gameUIController.StartGame -= StartGame;
        _playerCollisionHandler.BatteryTaken -= AddEnergy;
    }

    public void ChangeOnMenuValue(bool value)
    {
        _onMenu = value;
    }

    private void StartGame()
    {
        CheckAvailableWeapons();
        _batteryValue = _startBatteryValue;
        CalculateAvailableNumberOfShots();
    }

    private void CheckAvailableWeapons()
    {
        foreach (var weapon in _allWeapons)
        {
            if (weapon.IsBuyed == true)
            {
                _availableWeapons.Add(weapon);
            }
        }
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
        if (!_onMenu)
        {
            if (_currentWeaponNumber == _availableWeapons.Count - 1)
            {
                _currentWeaponNumber = 0;
            }
            else
            {
                _currentWeaponNumber++;
            }

            ChangeWeapon(_availableWeapons[_currentWeaponNumber]);
            CalculateAvailableNumberOfShots();
        }
    }

    private void OnPreviousWeapon()
    {
        if (!_onMenu)
        {
            if (_currentWeaponNumber == 0)
            {
                _currentWeaponNumber = _availableWeapons.Count - 1;
            }
            else
            {
                _currentWeaponNumber--;
            }

            ChangeWeapon(_availableWeapons[_currentWeaponNumber]);
            CalculateAvailableNumberOfShots();
        }
    }

    private void ChangeWeapon(Weapon weapon)
    {
        WeaponChange?.Invoke(weapon.Label, weapon.Icon);
        _currentWeapon = weapon;
        _currentWeapon.Init(_weaponViewObject);
        _weaponViewObject.SetSprite(_currentWeapon.Sprite);
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
}