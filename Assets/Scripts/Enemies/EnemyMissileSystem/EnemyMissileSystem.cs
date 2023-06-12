using System.Collections;
using UnityEngine;

public class EnemyMissileSystem : Enemy
{
    [SerializeField] private Rocket _enemyShooterMissile;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _minYDirectionMissle;
    [SerializeField] private float _maxYDirectionMissle;

    private Coroutine _missileShooting;
    private bool _isShooting = true;
    private float _distanceToPlayer = 13;
    private Vector3 _shootingPosition; 
    private float _step = 0.01f;

    private void Start()
    {
        transform.position = StartPositionFromPlayer;
        _shootingPosition = new Vector3(_distanceToPlayer, 1, 0);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, _step);
    }

    private void OnEnable()
    {
        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }

        _missileShooting = StartCoroutine(MissileShooting());
        transform.position = StartPositionFromPlayer;
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
    }

    private IEnumerator MissileShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        if (_isShooting)
        {
            while (true)
            {
                yield return waitForSeconds;
                float yDirectionMissle = Random.Range(-0.2f, 0.7f);
                var missile = Instantiate(_enemyShooterMissile, transform.position + new Vector3(0.15f, 0.33f, 0f), Quaternion.identity);
                missile.SetYDirection(yDirectionMissle);
            }
        }
    }
}