using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

[RequireComponent(typeof(PlayerCollisionHandler))]
public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;
    [SerializeField] private List<Weapon> _availableWeapons;
    [SerializeField] private TMP_Text _batteryValueText;
    [SerializeField] private TMP_Text _addingBatteryText;
    [SerializeField] private float _changeEnergyTextEffectTime;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private AudioSource _weaponChangedSound;
    [SerializeField] private AudioSource _noAmmoSound;

    private const string BatteryValueName = "BatteryValue";

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
    private int _defaultBatteryEnergy = 20;
    private WeaponViewObject _weaponViewObject;
    private float _soundVolume = 1f;

    public event UnityAction<Sprite> WeaponChange;

    private void Awake()
    {
        CheckAvailableWeapons();
        _onMenu = true;
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _weaponViewObject = GetComponentInChildren<WeaponViewObject>();
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
        WeaponChange?.Invoke(_currentWeapon.Icon);
    }

    private void OnEnable()
    {
        _gameUI.StartGame += OnStartGame;
        _playerCollisionHandler.BatteryTaken += OnBatteryTaken;
    }

    private void OnDisable()
    {
        _gameUI.StartGame -= OnStartGame;
        _playerCollisionHandler.BatteryTaken -= OnBatteryTaken;
    }

    public void ChangeOnMenuValue(bool value)
    {
        _onMenu = value;
    }

    private void OnStartGame()
    {
        CheckAvailableWeapons();

        if (PlayerPrefs.HasKey(BatteryValueName))
        {
            _batteryValue = PlayerPrefs.GetInt(BatteryValueName);
        }
        else
        {
            _batteryValue = _defaultBatteryEnergy;
            PlayerPrefs.SetInt(BatteryValueName, _batteryValue);
        }

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

    private void OnBatteryTaken(int energyPoints)
    {
        _batteryValue += energyPoints;
        CalculateAvailableNumberOfShots();
        StartTextEffectCoroutine(_changeEnergyColorAdd, energyPoints);
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
                AudioSource.PlayClipAtPoint(_currentWeapon.ShootSound, transform.position, _soundVolume);
            }
            else
            {
                _batteryValueText.color = _changeEnergyColorLacks;
                _noAmmoSound.PlayDelayed(0);
            }
        }
    }

    private void StartTextEffectCoroutine(Color color, int energyPoints)
    {
        if (_changeEnergyTextColor != null)
        {
            StopCoroutine(_changeEnergyTextColor);
        }

        _changeEnergyTextColor = StartCoroutine(ChangeEnergyTextColor(color, energyPoints));
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
            _weaponChangedSound.PlayDelayed(0);
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
            _weaponChangedSound.PlayDelayed(0);
        }
    }

    private void ChangeWeapon(Weapon weapon)
    {
        WeaponChange?.Invoke(weapon.Icon);
        _currentWeapon = weapon;
        _currentWeapon.Init(_weaponViewObject);
        _weaponViewObject.SetSprite(_currentWeapon.Sprite);
    }

    private IEnumerator ChangeEnergyTextColor(Color temp—olor, int addedEnergy)
    {
        var waitForSeconds = new WaitForSeconds(_changeEnergyTextEffectTime);
        _addingBatteryText.text = $"+ {addedEnergy / _currentWeapon.EnergyConsuming}".ToString();
        _batteryValueText.color = temp—olor;
        _batteryValueText.fontSize = _batteryTextMaxFontSize;
        yield return waitForSeconds;
        _addingBatteryText.text = "";
        _batteryValueText.color = _regularEnergyText—olor;
        _batteryValueText.fontSize = _regularTextSize;
    }
}