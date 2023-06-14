using UnityEngine;

public class ElectricBall : Enemy
{
    [SerializeField] private int _additionalSpeed;

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
        transform.Translate(Vector3.left * (Speed + _additionalSpeed) * Time.deltaTime);
    }
}