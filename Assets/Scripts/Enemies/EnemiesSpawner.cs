using System.Collections;
using System.Collections.Generic;
using PlayerCharacter;
using Scorer;
using UI;
using UnityEngine;

namespace Enemies
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _easyEnemiesSamples;
        [SerializeField] private List<Enemy> _hardEnemiesSamples;
        [SerializeField] private Player _player;
        [SerializeField] private float _minTimeOfAppearanceEasyEnemies;
        [SerializeField] private float _minTimeOfAppearanceHardEnemies;
        [SerializeField] private float _maxTimeOfAppearanceEasyEnemies;
        [SerializeField] private float _maxTimeOfAppearanceHardEnemies;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private ScoreManager _scoreManager;

        private List<Enemy> _easyEnemies = new List<Enemy>();
        private List<Enemy> _hardEnemies = new List<Enemy>();
        private Coroutine _activateEasyEnemy;
        private Coroutine _activateHardEnemy;
        private float _timeOfAppearanceEasyEnemies;
        private float _timeOfAppearanceHardEnemies;
        private float _speedObjects;

        private void Start()
        {
            _timeOfAppearanceEasyEnemies = Random.Range(_minTimeOfAppearanceEasyEnemies,
                _maxTimeOfAppearanceEasyEnemies);
            _timeOfAppearanceHardEnemies = Random.Range(_minTimeOfAppearanceHardEnemies,
                _maxTimeOfAppearanceHardEnemies);

            foreach (var enemyObject in _easyEnemiesSamples)
            {
                var enemy = Instantiate(enemyObject, transform.position, Quaternion.identity);
                enemy.Init(_player, _speedObjects);
                enemy.transform.SetParent(transform, false);
                enemy.gameObject.SetActive(false);
                _easyEnemies.Add(enemy);
            }

            foreach (var enemyObject in _hardEnemiesSamples)
            {
                var enemy = Instantiate(enemyObject, transform.position, Quaternion.identity);
                enemy.Init(_player, _speedObjects);
                enemy.transform.SetParent(transform, false);
                enemy.gameObject.SetActive(false);
                _hardEnemies.Add(enemy);
            }
        }

        private void OnEnable()
        {
            _gameUI.StartGame += OnStartGame;
            _gameUI.ChangeState += OnChangeState;
            _gameUI.MenuButtonClick += OnStartGame;
            _scoreManager.SpeedChange += OnSpeedChange;
        }

        private void OnDisable()
        {
            _gameUI.StartGame -= OnStartGame;
            _gameUI.ChangeState -= OnChangeState;
            _gameUI.MenuButtonClick -= OnStartGame;
            _scoreManager.SpeedChange -= OnSpeedChange;
        }

        private void OnStartGame()
        {
            foreach (var enemy in _easyEnemies)
            {
                enemy.gameObject.SetActive(false);
            }

            foreach (var enemy in _hardEnemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void OnChangeState(bool state)
        {
            if (!state)
            {
                if (_activateEasyEnemy != null)
                    StopCoroutine(_activateEasyEnemy);

                _activateEasyEnemy = StartCoroutine(ActivateEasyEnemy());
            }
            else
            {
                if (_activateEasyEnemy != null)
                    StopCoroutine(_activateEasyEnemy);
            }

            if (!state)
            {
                if (_activateHardEnemy != null)
                    StopCoroutine(_activateHardEnemy);

                _activateHardEnemy = StartCoroutine(ActivateHardEnemy());
            }
            else
            {
                if (_activateHardEnemy != null)
                    StopCoroutine(_activateHardEnemy);
            }
        }

        private void OnSpeedChange(float speed)
        {
            _speedObjects = speed;
        }

        private bool CheckEnemyActive(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.activeSelf == true)
                    return true;
            }

            return false;
        }

        private void SetEnemy(List<Enemy> enemies)
        {
            if (!CheckEnemyActive(enemies))
            {
                var enemy = enemies[Random.Range(0, enemies.Count)];
                enemy.AttackPlayer(_speedObjects);
            }
        }

        private IEnumerator ActivateEasyEnemy()
        {
            var waitForSecondsEasyEnemies = new WaitForSeconds(_timeOfAppearanceEasyEnemies);
            List<Enemy> easyEnemiesTemp = new List<Enemy>();

            while (true)
            {
                yield return waitForSecondsEasyEnemies;

                foreach (Enemy additionalEnemy in _easyEnemies)
                {
                    if (additionalEnemy.gameObject.activeSelf == false)
                        easyEnemiesTemp.Add(additionalEnemy);
                }

                if (easyEnemiesTemp.Count > 0)
                {
                    var enemy = easyEnemiesTemp[Random.Range(0, easyEnemiesTemp.Count)];
                    enemy.AttackPlayer(_speedObjects);
                }

                easyEnemiesTemp.Clear();
                _timeOfAppearanceEasyEnemies = Random.Range(_minTimeOfAppearanceEasyEnemies,
                    _maxTimeOfAppearanceEasyEnemies);
            }
        }

        private IEnumerator ActivateHardEnemy()
        {
            var waitForSecondsHardEnemies = new WaitForSeconds(_timeOfAppearanceHardEnemies);

            while (true)
            {
                yield return waitForSecondsHardEnemies;
                SetEnemy(_hardEnemies);
                _timeOfAppearanceHardEnemies = Random.Range(_minTimeOfAppearanceHardEnemies,
                    _maxTimeOfAppearanceHardEnemies);
            }
        }
    }
}