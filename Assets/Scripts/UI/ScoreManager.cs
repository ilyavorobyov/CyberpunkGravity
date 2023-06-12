using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _maxScoreText;
    [SerializeField] private int _minNextLevelScore;
    [SerializeField] private int _maxNextLevelScore;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _additionToSpeed;
    [SerializeField] private GameUIController _gameUIController;

    private const string ScoreValue = "Score";

    private int _maxResult;
    private float _tempCurrentResult;
    private float _timeFromStart;
    private float _speedObjects;
    private int _nextLevelScore;
    private int _scoreMultiplier = 5;
    private int _currentResult;

    public event UnityAction<float> SpeedChange;

    private void Awake()
    {
        _timeFromStart = 0;
        _maxScoreText.text = PlayerPrefs.GetInt(ScoreValue).ToString();
    }

    private void Start()
    {
        _speedObjects = _startSpeed;
        SpeedChange?.Invoke(_speedObjects);
        _nextLevelScore = Random.Range(_minNextLevelScore, _maxNextLevelScore);
    }

    private void Update()
    {
        _timeFromStart += Time.deltaTime;
        _tempCurrentResult = _timeFromStart * _scoreMultiplier;
        _currentResult = (int)_tempCurrentResult;
        _scoreText.text = _currentResult.ToString();

        if (_currentResult >= _nextLevelScore)
        {
            _speedObjects += _additionToSpeed;
            _nextLevelScore += Random.Range(_minNextLevelScore, _maxNextLevelScore);
            SpeedChange?.Invoke(_speedObjects);
        }
    }

    private void OnEnable()
    {
        _player.PlayerDied += SaveResult;
        _gameUIController.StartGame += StartGame;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= SaveResult;
        _gameUIController.StartGame -= StartGame;
    }

    private void StartGame()
    {
        _timeFromStart = 0;
    }

    private void SaveResult()
    {
        _maxResult = PlayerPrefs.GetInt(ScoreValue);

        if (_maxResult < _currentResult)
        {
            PlayerPrefs.SetInt(ScoreValue, _currentResult);
        }

        Debug.Log("save result");
        _maxScoreText.text = PlayerPrefs.GetInt(ScoreValue).ToString();
    }
}