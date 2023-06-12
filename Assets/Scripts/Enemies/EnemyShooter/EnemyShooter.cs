using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private EnemyLaserShot _enemyLaserShot;
    [SerializeField] private float _timeBetweenShots;

    private Coroutine _laserShooting;
    private float _distanceToPlayer = 15;
    private Vector3 _shootingPosition;
    private float _step = 0.01f;

    private void Start()
    {
        transform.position = StartPositionFromPlayer;
    }

    private void Update()
    {
        _shootingPosition = PlayerObject.transform.position + new Vector3(_distanceToPlayer, 0, 0);
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, _step);
    }

    private void OnEnable()
    {
        transform.position = StartPositionFromPlayer;

        if (_laserShooting != null)
        {
            StopCoroutine(_laserShooting);
        }

        _laserShooting = StartCoroutine(LaserShooting());
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
    }

    private IEnumerator LaserShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        while(true)
        {
            yield return waitForSeconds;
            Instantiate(_enemyLaserShot, transform.position, Quaternion.identity);
        }
    }
}