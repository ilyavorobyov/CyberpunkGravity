using UnityEngine;

public class AntiGravitySwitch : Enemy
{
    private PlayerMover _playerMover;

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
        Vector3 position = new Vector3(StartPositionFromPlayer.x, _yPosition, StartPositionFromPlayer.z);
        transform.position = position;
    }

    public override void Die()
    {
        base.Die();
        _playerMover.TurnOnGravityChanger();
    }
}