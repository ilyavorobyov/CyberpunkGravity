using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _xOffset;
    [SerializeField] private ScoreManager _scoreManager;

    private float _speed;
    private Vector3 _startPosition = new Vector3(18f, -1.26f, 0f);
    private float _minX = 2;
    private float _speedMultiplier = 0.3f;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < _minX)
        {
           transform.position = _startPosition;
        }
    }

    private void OnEnable()
    {
        _scoreManager.SpeedChange += SetMovingSpeed;
    }

    private void OnDisable()
    {
        _scoreManager.SpeedChange -= SetMovingSpeed;
    }

    private void SetMovingSpeed(float speed)
    {
        _speed = speed * _speedMultiplier;
    }
}