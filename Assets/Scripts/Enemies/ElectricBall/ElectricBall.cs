using UnityEngine;

public class ElectricBall : Enemy
{
    [SerializeField] private int _additionalSpeed;

    private Vector3 _playerPosition;
    private Vector3 _startPosition;
    private float _addToXPosition = 20;

    private void OnEnable()
    {
        _playerPosition = PlayerObject.transform.position;
        _startPosition = PlayerObject.transform.position + new Vector3(_playerPosition.x + _addToXPosition, 0, 0);
        transform.position = _startPosition;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * (Speed + _additionalSpeed) * Time.deltaTime);
    }
}