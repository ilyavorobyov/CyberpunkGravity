using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerCollisionHandler))]
public class Wallet : MonoBehaviour
{
    private const string ALLCOINSVALUE = "Coins";

    [SerializeField] private TMP_Text _allCoinsText;
    [SerializeField] private TMP_Text _coinsPerGameSessionText;
    private PlayerCollisionHandler _playerCollisionHandler;
    private Player _player;

    private int _coinsPerGameSessionValue = 0;

    public int Coins { get; private set; }

    private void Awake()
    {
        Coins = PlayerPrefs.GetInt(ALLCOINSVALUE);
        _allCoinsText.text = Coins.ToString();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _player = GetComponent<Player>();
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
        _playerCollisionHandler.CoinCollected += AddCoins;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= SaveCoins;
        _playerCollisionHandler.CoinCollected -= AddCoins;
    }

}