using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private const string ScoreValue = "Score";

    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _maxScoreText;

    private int _maxResult;
    private int _currentResult;

    private void Awake()
    {
        _maxScoreText.text = PlayerPrefs.GetInt(ScoreValue).ToString();
    }

    private void Update()
    {
        _currentResult = (int)_player.transform.position.x;
        _scoreText.text = _currentResult.ToString();
    }

    private void SaveResult()
    {
        _maxResult = PlayerPrefs.GetInt(ScoreValue);

        if(_maxResult < _currentResult)
        {
            PlayerPrefs.SetInt(ScoreValue, _currentResult);
        }

        Debug.Log("save result");
        _maxScoreText.text = PlayerPrefs.GetInt(ScoreValue).ToString();
    }

    private void OnEnable()
    {
        _player.PlayerDied += SaveResult;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= SaveResult;
    }
}