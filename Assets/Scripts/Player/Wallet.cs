using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerCollisionHandler))]
public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _allCoinsText;
    [SerializeField] private TMP_Text _coinsPerGameSessionText;
    [SerializeField] private TMP_Text _gameOverPanelText;

    private const string AllCoinsValue = "Coins";

    private PlayerCollisionHandler _playerCollisionHandler;
    private Player _player;
    private int _coinsPerGameSessionValue = 0;

    public int Coins { get; private set; }

    private void Awake()
    {
        Coins = PlayerPrefs.GetInt(AllCoinsValue);
        _allCoinsText.text = Coins.ToString();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.PlayerDied += OnPlayerDied;
        _playerCollisionHandler.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= OnPlayerDied;
        _playerCollisionHandler.CoinCollected -= OnCoinCollected;
    }

    public void RemoveCoins(int coins)
    {
        Coins -= coins;
        _allCoinsText.text = Coins.ToString();
    }

    public void OnPlayerDied()
    {
        Coins += _coinsPerGameSessionValue;

        if(_coinsPerGameSessionValue == 0)
        {
            _gameOverPanelText.text = "Игра окончена! Попробуй снова!";
        }
        else
        {
            _gameOverPanelText.text = "Игра окончена! Собрано " 
                + _coinsPerGameSessionValue + " монет";
        }

        PlayerPrefs.SetInt(AllCoinsValue, Coins);
        _allCoinsText.text = Coins.ToString();
        _coinsPerGameSessionValue = 0;
        _coinsPerGameSessionText.text = _coinsPerGameSessionValue.ToString();
    }

    private void OnCoinCollected(int denomination)
    {
        _coinsPerGameSessionValue += denomination;
        _coinsPerGameSessionText.text = _coinsPerGameSessionValue.ToString();
    }
}