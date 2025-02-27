using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyShooter : Enemy
    {
        [SerializeField] private Rocket _enemyLaserShot;
        [SerializeField] private float _minShotTime;
        [SerializeField] private float _maxShotTime;
        [SerializeField] private AudioSource _shootSound;

        private ShotPoint _shotPoint;
        private float _shotTime;
        private float _distanceToPlayer = 10;
        private float _startYPosition = 2;
        private float _step = 0.06f;
        private Vector3 _shootingPosition;
        private Coroutine _laserShooting;

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
}