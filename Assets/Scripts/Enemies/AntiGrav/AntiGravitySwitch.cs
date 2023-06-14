using UnityEngine;

public class AntiGravitySwitch : Enemy
{
    private PlayerMover _playerMover;
    private Vector3 _playerPosition;
    private float _addToXPosition = 20;

    private void Start()
    {
        _playerMover = PlayerObject.GetComponent<PlayerMover>();
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
        float _maxY = 7.6f;
        float _minY = 0.1f;
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
        float _yPosition = Random.Range(_minY, _maxY);
    }

    public override void Die()
    {
        base.Die();
        _playerMover.TurnOnGravityChanger();
    }
}