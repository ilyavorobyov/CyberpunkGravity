using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    private const string ALLCOINSVALUE = "Coins";

    [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private TMP_Text _allCoinsText;
    [SerializeField] private TMP_Text _coinsPerGameSessionText;
    [SerializeField] private TMP_Text _batteryValueText;

    private int _coinsPerGameSessionValue = 0;
    private int _batteryValue;

    public int Coins { get; private set; }

    private void Awake()
    {
        _batteryValue = 10;
        _batteryValueText.text = _batteryValue.ToString();
        Coins = PlayerPrefs.GetInt(ALLCOINSVALUE);
        _allCoinsText.text = Coins.ToString();
        _playerMover.SetEnergy(_batteryValue);
    }

    private void AddBatteryEnergy(int energy)
    {
        _batteryValue += energy;
        _batteryValueText.text = _batteryValue.ToString();
        _playerMover.SetEnergy(_batteryValue);
    }

    private void RemoveBatteryEnergy(int energy)
    {
        _batteryValue -= energy;
        _playerMover.SetEnergy(_batteryValue);
        _batteryValueText.text = _batteryValue.ToString();
    }

    private void AddCoins(int denomination)
    {
        _coinsPerGameSessionValue += denomination;
        _coinsPerGameSessionText.text = _coinsPerGameSessionValue.ToString();
    }

    private void SaveCoins()
    {
        Coins += _coinsPerGameSessionValue;
        PlayerPrefs.SetInt(ALLCOINSVALUE, Coins);
        _allCoinsText.text = Coins.ToString();
        _coinsPerGameSessionValue = 0;
        _coinsPerGameSessionText.text = _coinsPerGameSessionValue.ToString();
    }

    private void OnEnable()
    {
        _player.PlayerDied += SaveCoins;
        _playerMover.Shot += RemoveBatteryEnergy;
        _playerCollisionHandler.CoinCollected += AddCoins;
        _playerCollisionHandler.BatteryTaken += AddBatteryEnergy;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= SaveCoins;
        _playerMover.Shot -= RemoveBatteryEnergy;
        _playerCollisionHandler.CoinCollected -= AddCoins;
        _playerCollisionHandler.BatteryTaken -= AddBatteryEnergy;
    }
}