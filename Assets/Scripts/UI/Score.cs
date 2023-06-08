using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _maxScoreText;

    private const string ScoreValue = "Score";

    private int _maxResult;
    private int _currentResult;
    private float _timeFromStart;

    private void Awake()
    {
        _maxScoreText.text = PlayerPrefs.GetInt(ScoreValue).ToString();
    }

    private void Update()
    {
        _timeFromStart += Time.deltaTime;
        _currentResult = (int)_player.transform.position.x;
        _scoreText.text = ((int)_timeFromStart).ToString();
    }

    private void OnEnable()
    {
        _player.PlayerDied += SaveResult;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= SaveResult;
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