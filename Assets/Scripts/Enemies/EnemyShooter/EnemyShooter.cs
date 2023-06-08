using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private Rocket _enemyLaserMissile;
    [SerializeField] private float _timeBetweenShots;

    private Coroutine _laserShooting;
    private float _distanceToPlayer = 16;
    private Vector3 _shootingPosition;

    private void Start()
    {
        if (_laserShooting != null)
        {
            StopCoroutine(_laserShooting);
        }

        _laserShooting = StartCoroutine(LaserShooting());
    }

    private void Update()
    {
        _shootingPosition = Player.transform.position + new Vector3(_distanceToPlayer, 0, 0);
        var step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, step);
    }

    private IEnumerator LaserShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        while(true)
        {
            yield return waitForSeconds;
            Instantiate(_enemyLaserMissile, transform.position,Quaternion.identity);
        }
    }
}