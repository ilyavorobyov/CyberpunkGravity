using System;
using DG.Tweening;
using PlayerCharacter;
using ShopBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _gameOverPanel;
        [SerializeField] private RectTransform _pausePanel;
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _allCoinsText;
        [SerializeField] private TMP_Text _allCoinsValue;
        [SerializeField] private TMP_Text _coinsPerGameSession;
        [SerializeField] private TMP_Text _scoreValue;
        [SerializeField] private TMP_Text _batteryValueText;
        [SerializeField] private TMP_Text _controlText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _menuButtonPausePanel;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Image _selectedWeaponIcon;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _closeShopButton;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private LearningPanel _learningPanel;
        [SerializeField] private AudioSource _startButtonClick;
        [SerializeField] private AudioSource _simpleButtonClick;
        [SerializeField] private AudioSource _gameOverSound;
        [SerializeField] private Button _soundSwitchMenuButton;

        private WeaponController _weaponController;

        public static Action RocketsRemoval;
        public event UnityAction StartGame;
        public event UnityAction GameOver;
        public event UnityAction MenuButtonClick;
        public event UnityAction<bool> ChangeState;

        public bool IsGameOn { get; private set; }

        private void Awake()
        {
            _weaponController = _player.GetComponent<WeaponController>();
            Time.timeScale = 0;
            IsGameOn = false;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _startButton.onClick.AddListener(OnStartButtonClick);
            _menuButton.onClick.AddListener(OnMenuUiState);
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _menuButtonPausePanel.onClick.AddListener(OnMenuUiState);
            _player.PlayerDied += OnPlayerDied;
            _weaponController.WeaponChange += OnWeaponChanged;
            _shopButton.onClick.AddListener(OnShopButtonClick);
            _closeShopButton.onClick.AddListener(OnCloseShopButtonClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuUiState);
            _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
            _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
            _menuButtonPausePanel.onClick.RemoveListener(OnMenuUiState);
            _player.PlayerDied -= OnPlayerDied;
            _weaponController.WeaponChange -= OnWeaponChanged;
            _shopButton.onClick.RemoveListener(OnShopButtonClick);
            _closeShopButton.onClick.RemoveListener(OnCloseShopButtonClick);
        }

        public void PauseGame()
        {
            OnPauseButtonClick();
        }

        private void ShrinkAnimation(GameObject uiElement)
        {
            uiElement.transform.localScale = Vector3.one;
            uiElement.SetActive(true);
            uiElement.transform.DOScale(Vector3.zero, _animationDuration).
                SetLoops(1, LoopType.Yoyo).OnComplete(() => uiElement.SetActive(false)).
                SetUpdate(UpdateType.Normal, true);
        }

        private void ZoomAnimation(GameObject uiElement)
        {
            uiElement.transform.localScale = Vector3.zero;
            uiElement.SetActive(true);
            uiElement.transform.DOScale(Vector3.one, _animationDuration).
                SetLoops(1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, true);
        }

        private void OnMenuUiState()
        {
            if (_pauseButton.gameObject.activeSelf == true)
            {
                ShrinkAnimation(_pauseButton.gameObject);
            }

            if (_pausePanel.gameObject.activeSelf == true)
            {
                ShrinkAnimation(_pausePanel.gameObject);
            }

            if (_gameOverPanel.gameObject.activeSelf == true)
            {
                ShrinkAnimation(_gameOverPanel.gameObject);
            }

            ZoomAnimation(_allCoinsText.gameObject);
            ZoomAnimation(_allCoinsValue.gameObject);
            ZoomAnimation(_startButton.gameObject);
            ZoomAnimation(_shopButton.gameObject);
            ZoomAnimation(_soundSwitchMenuButton.gameObject);
            ShrinkAnimation(_coinsPerGameSession.gameObject);
            ShrinkAnimation(_scoreValue.gameObject);
            ShrinkAnimation(_batteryValueText.gameObject);
            ShrinkAnimation(_selectedWeaponIcon.gameObject);

            if (_controlText.gameObject.activeSelf == true)
            {
                ShrinkAnimation(_controlText.gameObject);
            }

            ChangeState?.Invoke(true);
            MenuButtonClick.Invoke();
            RocketsRemoval?.Invoke();
            _simpleButtonClick.PlayDelayed(0);
            IsGameOn = false;
        }

        private void OnStartButtonClick()
        {
            Time.timeScale = 1f;
            _pausePanel.gameObject.SetActive(false);
            _shopPanel.gameObject.SetActive(false);
            ZoomAnimation(_pauseButton.gameObject);
            ShrinkAnimation(_startButton.gameObject);
            ShrinkAnimation(_startButton.gameObject);
            ShrinkAnimation(_startButton.gameObject);
            ShrinkAnimation(_shopButton.gameObject);
            ShrinkAnimation(_soundSwitchMenuButton.gameObject);
            ZoomAnimation(_coinsPerGameSession.gameObject);
            ZoomAnimation(_scoreValue.gameObject);
            ZoomAnimation(_batteryValueText.gameObject);
            ZoomAnimation(_selectedWeaponIcon.gameObject);
            ShrinkAnimation(_allCoinsText.gameObject);
            ShrinkAnimation(_allCoinsValue.gameObject);
            ZoomAnimation(_controlText.gameObject);
            ChangeState?.Invoke(false);
            StartGame.Invoke();
            RocketsRemoval?.Invoke();
            _startButtonClick.PlayDelayed(0);
            IsGameOn = true;
        }

        private void OnRestartButtonClick()
        {
            Time.timeScale = 1;
            ShrinkAnimation(_gameOverPanel.gameObject);
            ZoomAnimation(_pauseButton.gameObject);
            ZoomAnimation(_controlText.gameObject);
            ChangeState?.Invoke(false);
            StartGame.Invoke();
            RocketsRemoval?.Invoke();
            _simpleButtonClick.PlayDelayed(0);
            IsGameOn = true;
        }

        private void OnPlayerDied()
        {
            Time.timeScale = 0;
            ShrinkAnimation(_pauseButton.gameObject);
            ZoomAnimation(_gameOverPanel.gameObject);
            ShrinkAnimation(_controlText.gameObject);
            ChangeState?.Invoke(true);
            GameOver.Invoke();
            _gameOverSound.PlayDelayed(0);
            IsGameOn = false;
        }

        private void OnPauseButtonClick()
        {
            Time.timeScale = 0;
            ChangeState?.Invoke(true);
            ZoomAnimation(_pausePanel.gameObject);
            ShrinkAnimation(_pauseButton.gameObject);
            ShrinkAnimation(_controlText.gameObject);
            _simpleButtonClick.PlayDelayed(0);
            IsGameOn = false;
        }

        private void OnResumeButtonClick()
        {
            Time.timeScale = 1;
            ChangeState?.Invoke(false);
            ShrinkAnimation(_pausePanel.gameObject);
            ZoomAnimation(_pauseButton.gameObject);
            ZoomAnimation(_controlText.gameObject);
            _simpleButtonClick.PlayDelayed(0);
            IsGameOn = true;
        }

        private void OnWeaponChanged(Sprite icon)
        {
            _selectedWeaponIcon.sprite = icon;
        }

        private void OnShopButtonClick()
        {
            _shopPanel.SetActive(true);
            _shopPanel.GetComponent<Shop>().RenderGoods();
            _simpleButtonClick.PlayDelayed(0);
        }

        private void OnCloseShopButtonClick()
        {
            _shopPanel.SetActive(false);
            _simpleButtonClick.PlayDelayed(0);
        }
    }
}