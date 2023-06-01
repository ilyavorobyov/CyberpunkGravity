using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSystem : Enemy
{
    [SerializeField] private Rocket _enemyShooterMissile;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _minYDirectionMissle;
    [SerializeField] private float _maxYDirectionMissle;

    private Coroutine _missileShooting;
    private float _distanceToPlayerForShooting = 16;
    private bool _isShooting = false;
    private bool _isDistanceCalculating;
    private float _distanceToPlayer;

    void Start()
    {
        _isDistanceCalculating = true;

        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }

        _missileShooting = StartCoroutine(MissileShooting());

    }

    void Update()
    {
        _distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

        if (_isDistanceCalculating)
        {
            if (_distanceToPlayer <= _distanceToPlayerForShooting)
            {
                _isDistanceCalculating = false;
                _isShooting = true;

                if (_missileShooting != null)
                {
                    StopCoroutine(_missileShooting);
                }

                _missileShooting = StartCoroutine(MissileShooting());
            }
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
                float yDirectionMissle = Random.Range(0f, 0.7f);
                var missile = Instantiate(_enemyShooterMissile, transform.position + new Vector3(0.15f, 0.33f, 0f), Quaternion.identity);
                missile.SetYDirection(yDirectionMissle);
            }
        }
    }
}