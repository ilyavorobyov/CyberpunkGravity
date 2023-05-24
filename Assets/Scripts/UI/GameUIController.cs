using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _gameOverPanel;
    [SerializeField] private RectTransform _pausePanel;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _coinsPerGameSession;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButtonPausePanel;

    public event UnityAction StartGame;
    public event UnityAction GameOver;
    public event UnityAction MenuButtonClick;
    public event UnityAction<bool> ChangeState;

    private void Awake()
    {
        Time.timeScale = 1;
        OnMenuUiState();
    }

    private void OnMenuUiState()
    {
        _pausePanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
        _gameOverPanel.gameObject.SetActive(false);
        _coinText.gameObject.SetActive(false);
        _coinsPerGameSession.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _scoreValue.gameObject.SetActive(false);
        ChangeState?.Invoke(true);
    }

    private void OnStartButtonClick()
    {
        Time.timeScale = 1;
        _pausePanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _gameOverPanel.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _coinText.gameObject.SetActive(true);
        _coinsPerGameSession.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(true);
        _scoreValue.gameObject.SetActive(true);
        ChangeState?.Invoke(false);
        StartGame.Invoke();
    }

    private void GameOverUiState()
    {
        _pausePanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
        ChangeState?.Invoke(true);
        GameOver.Invoke();
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        ChangeState?.Invoke(true);
        _pausePanel.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    private void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        ChangeState?.Invoke(false);
        _pausePanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    private void OnMenuButtonClick()
    {
        ChangeState?.Invoke(true);
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnStartButtonClick);
        _startButton.onClick.AddListener(OnStartButtonClick);
        _menuButton.onClick.AddListener(OnMenuUiState);
        _menuButton.onClick.AddListener(OnMenuButtonClick);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _menuButtonPausePanel.onClick.AddListener(OnMenuUiState);
        _player.PlayerDied += GameOverUiState;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnStartButtonClick);
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuUiState);
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _menuButtonPausePanel.onClick.RemoveListener(OnMenuUiState);
        _player.PlayerDied -= GameOverUiState;
    }
}