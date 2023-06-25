using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private EnemyLaserShot _enemyLaserShot;
    [SerializeField] private float _timeBetweenShots;

    private ShotPoint _shotPoint;
    private Coroutine _laserShooting;
    private float _distanceToPlayer = 11;
    private float _startYPosition = 2;
    private Vector3 _shootingPosition;
    private float _step = 0.015f;

    private void Start()
    {
        _shotPoint = GetComponentInChildren<ShotPoint>();
    }

    private void Update()
    {
        _shootingPosition = PlayerObject.transform.position + new Vector3(_distanceToPlayer, 0, 0);
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, _step);
    }

    private void OnDisable()
    {
        if (_laserShooting != null)
        {
            StopCoroutine(_laserShooting);
        }
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);

        if (_laserShooting != null)
        {
            StopCoroutine(_laserShooting);
        }

        _laserShooting = StartCoroutine(LaserShooting());
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = PlayerPosition + new Vector3(PlayerPosition.x + AddToXPosition, _startYPosition, 0);
        transform.position = StartPosition;
    }

    private IEnumerator LaserShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        while (true)
        {
            yield return waitForSeconds;
            Instantiate(_enemyLaserShot, _shotPoint.transform.position, Quaternion.identity);
        }
    }
}