using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private Rocket _enemyLaserShot;
    [SerializeField] private float _minShotTime;
    [SerializeField] private float _maxShotTime;
    [SerializeField] private AudioSource _shootSound;

    private float _shotTime;
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
        _shotTime = Random.Range(_minShotTime, _maxShotTime);
        var waitForSeconds = new WaitForSeconds(_shotTime);

        while (true)
        {
            yield return waitForSeconds;
            var laserShot = Instantiate(_enemyLaserShot, _shotPoint.transform.position, Quaternion.identity);
            laserShot.SetSpeed(Speed);
            _shootSound.PlayDelayed(0);
            _shotTime = Random.Range(_minShotTime, _maxShotTime);
        }
    }
}