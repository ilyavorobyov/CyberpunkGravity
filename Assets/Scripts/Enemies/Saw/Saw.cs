using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Enemy
{
    [SerializeField] private bool _isSmallSaw;

    private float _maxYSmallSaw = 7;
    private float _minYSmallSaw = 0.6f;
    private float _maxYBigSaw = 6.2f;
    private float _minYBigSaw = 1.2f;
    private Vector3 _position;
    private Vector3 _playerPosition;
    private float _addToXPosition = 20;

    private void Start()
    {
        _playerPosition = PlayerObject.transform.position;
        StartPosition = PlayerObject.transform.position + new Vector3(_playerPosition.x + _addToXPosition, 0, 0);
        transform.position = StartPosition;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        if (_isSmallSaw)
        {
            float yPosition = Random.Range(_minYSmallSaw, _maxYSmallSaw);
            _position = StartPosition + new Vector3(0, yPosition, 0);
        }
        else
        {
            float yPosition = Random.Range(_minYBigSaw, _maxYBigSaw);
            _position = StartPosition + new Vector3(0, yPosition, 0);
        }

        transform.position = _position;
    }
}
