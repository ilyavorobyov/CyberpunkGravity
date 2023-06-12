using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesResourcesSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemySamples;
    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Player _player;
    [SerializeField] private float _minTimeOfAppearance;
    [SerializeField] private float _maxTimeOfAppearance;
    [SerializeField] private GameUIController _gameUIController;
    [SerializeField] private ScoreManager _scoreManager;

    private Coroutine _performAppearance;
    private float _timeOfAppearance;
    private float _speedObjects;
    private int _numberDangerousEnemies = 4;

    private void Start()
    {
        _timeOfAppearance = Random.Range(_minTimeOfAppearance, _maxTimeOfAppearance);

        foreach (var enemyObject in _enemySamples)
        {
            var enemy = Instantiate(enemyObject, transform.position, Quaternion.identity);
            enemy.Init(_player, _speedObjects);
            enemy.transform.SetParent(transform, false);
            enemy.gameObject.SetActive(false);
            _enemys.Add(enemy);
        }
    }

    private void OnEnable()
    {
        _gameUIController.StartGame += StartGame;
        _gameUIController.ChangeState += ControlSpawn;
        _scoreManager.SpeedChange += SetObjectsSpeed;
    }

    private void OnDisable()
    {
        _gameUIController.StartGame -= StartGame;
        _gameUIController.ChangeState -= ControlSpawn;
        _scoreManager.SpeedChange -= SetObjectsSpeed;
    }

    private void StartGame()
    {
        foreach (var enemy in _enemys)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void SetObjectsSpeed(float speed)
    {
        _speedObjects = speed;
    }

    private void SetEnemy()
    {
        if (!CheckEnemyActive())
        {
            var enemy = _enemys[Random.Range(0, _enemySamples.Count)];
            enemy.AttackPlayer(_speedObjects);

            if (enemy as AntiGravitySwitch)
            {
                var secondEnemy = _enemys[Random.Range(0, _numberDangerousEnemies)];
                secondEnemy.AttackPlayer(_speedObjects);
            }
        }
    }

    private bool CheckEnemyActive()
    {
        foreach (var enemy in _enemys)
        {
            if (enemy.gameObject.activeSelf == true)
            {
                return true;
            }
        }

        return false;
    }
    private void ControlSpawn(bool state)
    {
        if (!state)
        {
            if (_performAppearance != null)
            {
                StopCoroutine(_performAppearance);
            }

            _performAppearance = StartCoroutine(PerformAppearance());
        }
        else
        {
            if (_performAppearance != null)
            {
                StopCoroutine(_performAppearance);
            }
        }
    }

    private IEnumerator PerformAppearance()
    {
        var waitForSeconds = new WaitForSeconds(_timeOfAppearance);

        while (true)
        {
            yield return waitForSeconds;
            SetEnemy();
            _timeOfAppearance = Random.Range(_minTimeOfAppearance, _maxTimeOfAppearance);
        }
    }
}