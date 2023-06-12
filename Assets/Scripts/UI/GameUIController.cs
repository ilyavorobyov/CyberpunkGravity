using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _gameOverPanel;
    [SerializeField] private RectTransform _pausePanel;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _coinsPerGameSession;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private TMP_Text _batteryValueText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButtonPausePanel;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Image _selectedWeaponIcon;
    [SerializeField] private TMP_Text _selectedWeaponText;

    private WeaponController _weaponController;

    public event UnityAction StartGame;
    public event UnityAction GameOver;
    public event UnityAction MenuButtonClick;
    public event UnityAction<bool> ChangeState;

    private void Awake()
    {
        _weaponController = _player.GetComponent<WeaponController>();
        Time.timeScale = 0;
        OnMenuUiState();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _startButton.onClick.AddListener(OnStartButtonClick);
        _menuButton.onClick.AddListener(OnMenuUiState);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _menuButtonPausePanel.onClick.AddListener(OnMenuUiState);
        _player.PlayerDied += GameOverUiState;
        _weaponController.WeaponChange += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuUiState);
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _menuButtonPausePanel.onClick.RemoveListener(OnMenuUiState);
        _player.PlayerDied -= GameOverUiState;
        _weaponController.WeaponChange -= OnWeaponChanged;
    }

    private void ShrinkAnimation(GameObject uiElement)
    {
        uiElement.transform.localScale = Vector3.one;
        uiElement.SetActive(true);
        uiElement.transform.DOScale(Vector3.zero, _animationDuration).SetLoops(1, LoopType.Yoyo).OnComplete(() => uiElement.SetActive(false)).SetUpdate(UpdateType.Normal, true);
    }

    private void ZoomAnimation(GameObject uiElement)
    {
        uiElement.transform.localScale = Vector3.zero;
        uiElement.SetActive(true);
        uiElement.transform.DOScale(Vector3.one, _animationDuration).SetLoops(1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, true);
    }

    private void OnMenuUiState()
    {
        if(_pauseButton.gameObject.activeSelf == true)
        {
            ShrinkAnimation(_pauseButton.gameObject);
        }

        if (_pausePanel.gameObject.activeSelf == true)
        {
            ShrinkAnimation(_pausePanel.gameObject);
        }

        if(_gameOverPanel.gameObject.activeSelf == true)
        {
            ShrinkAnimation(_gameOverPanel.gameObject);
        }

        ZoomAnimation(_startButton.gameObject);
        ShrinkAnimation(_coinText.gameObject);
        ShrinkAnimation(_coinsPerGameSession.gameObject);
        ShrinkAnimation(_scoreText.gameObject);
        ShrinkAnimation(_scoreValue.gameObject);
        ShrinkAnimation(_batteryValueText.gameObject);
        ChangeState?.Invoke(true);
        MenuButtonClick.Invoke();
    }

    private void OnStartButtonClick()
    {
        Time.timeScale = 1f;
        _pausePanel.gameObject.SetActive(false);
        ZoomAnimation(_pauseButton.gameObject);
        ShrinkAnimation(_startButton.gameObject);
        ZoomAnimation(_coinText.gameObject);
        ZoomAnimation(_coinsPerGameSession.gameObject);
        ZoomAnimation(_scoreText.gameObject);
        ZoomAnimation(_scoreValue.gameObject);
        ZoomAnimation(_batteryValueText.gameObject);
        ChangeState?.Invoke(false);
        StartGame.Invoke();
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        ShrinkAnimation(_gameOverPanel.gameObject);
        ZoomAnimation(_pauseButton.gameObject);
        ChangeState?.Invoke(false);
        StartGame.Invoke();
    }

    private void GameOverUiState()
    {
        Time.timeScale = 0;
        ShrinkAnimation(_pauseButton.gameObject);
        ZoomAnimation(_gameOverPanel.gameObject);
        ChangeState?.Invoke(true);
        GameOver.Invoke();
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        ChangeState?.Invoke(true);
        ZoomAnimation(_pausePanel.gameObject);
        ShrinkAnimation(_pauseButton.gameObject);
    }

    private void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        ChangeState?.Invoke(false);
        ShrinkAnimation(_pausePanel.gameObject);
        ZoomAnimation(_pauseButton.gameObject);
    }

    private void OnWeaponChanged(string label, Sprite icon)
    {
        _selectedWeaponIcon.sprite = icon;
        _selectedWeaponText.text = label;
    }
}