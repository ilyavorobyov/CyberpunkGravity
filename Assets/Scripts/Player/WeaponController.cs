using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(PlayerCollisionHandler))]
public class WeaponController: MonoBehaviour
{
    [SerializeField] private TMP_Text _batteryValueText;
    private PlayerCollisionHandler _playerCollisionHandler;
    private Weapon _weapon;
    private int _batteryValue;

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
        _batteryValue = 10;
        _batteryValueText.text = _batteryValue.ToString();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    private void AddEnergy(int energyPoints)
    {
        _batteryValue += energyPoints;
        _batteryValueText.text = _batteryValue.ToString();
    }

    private void OnShoot()
    {
        Debug.Log("SD");
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