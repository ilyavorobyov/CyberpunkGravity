using UnityEngine;
using UnityEngine.UIElements;

public class AntiGravitySwitch : Enemy
{
    [SerializeField] private float _maxY = 7.6f;
    [SerializeField] private float _minY = 0.1f;

    private PlayerMover _playerMover;
    private float _yPosition;

    private void Start()
    {
        _playerMover = PlayerObject.GetComponent<PlayerMover>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    public override void SetStartInfo()
    {
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
        _yPosition = Random.Range(_minY, _maxY);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = new Vector3(AddToXPosition, _yPosition, 0);
        transform.position = StartPosition;
        gameObject.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        _playerMover.TurnOnGravityChanger();
    }
}