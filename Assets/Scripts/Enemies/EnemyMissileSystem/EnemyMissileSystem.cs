using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSystem : Enemy
{
    [SerializeField] private Rocket _enemyShooterMissile;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _minYDirectionMissle;
    [SerializeField] private float _maxYDirectionMissle;
    [SerializeField] private float _shootXPosition;
    [SerializeField] private float _startYPosition;
    [SerializeField] private float _shootYPosition;

    private List<Rocket> _missiles = new List<Rocket>();
    private Coroutine _missileShooting;
    private bool _isShooting = true;
    private Vector3 _shootingPosition; 
    private float _step = 0.015f;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, _step);
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);

        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }

        _missileShooting = StartCoroutine(MissileShooting());
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = PlayerPosition + new Vector3(PlayerPosition.x + AddToXPosition, _startYPosition, 0);
        transform.position = StartPosition;
        _shootingPosition = new Vector3(_shootXPosition, _shootYPosition, 0);
    }

    private void OnDisable()
    {
        foreach (Rocket missile in _missiles)
        {
            Destroy(missile.gameObject);
        }

        _missiles.Clear();

        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }
    }

    private IEnumerator MissileShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        if (_isShooting)
        {
            while (true)
            {
                yield return waitForSeconds;
                float yDirectionMissle = Random.Range(_minYDirectionMissle, _maxYDirectionMissle);
                var missile = Instantiate(_enemyShooterMissile, transform.position + new Vector3(0.15f, 0.33f, 0f), Quaternion.identity);
                missile.SetYDirection(yDirectionMissle);
                _missiles.Add(missile);
            }
        }
    }
}