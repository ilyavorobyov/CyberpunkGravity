using UnityEngine;

public class ElectricBall : Enemy
{
    private Vector3 _playerPosition;
    private Vector3 _startPosition;
    private float addToXPosition = 20;

    private void Start()
    {
        _playerPosition = Player.transform.position;
        _startPosition = Player.transform.position + new Vector3(_playerPosition.x + addToXPosition, 0, 0);
        transform.position = _startPosition;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}