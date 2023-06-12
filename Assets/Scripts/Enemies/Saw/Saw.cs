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

    private void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        if (_isSmallSaw)
        {
            float yPosition = Random.Range(_minYSmallSaw, _maxYSmallSaw);
            _position = StartPositionFromPlayer + new Vector3(0, yPosition, 0);
        }
        else
        {
            float yPosition = Random.Range(_minYBigSaw, _maxYBigSaw);
            _position = StartPositionFromPlayer + new Vector3(0, yPosition, 0);
        }

        transform.position = _position;
    }
}
