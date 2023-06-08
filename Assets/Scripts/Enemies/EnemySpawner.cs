using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _minTimeOfAppearance;
    [SerializeField] private float _maxTimeOfAppearance;
    [SerializeField] private GameUIController _gameUIController;

    private Coroutine _performAppearance;
    private float _timeOfAppearance;


    private void Start()
    {
        _timeOfAppearance = Random.Range(_minTimeOfAppearance, _maxTimeOfAppearance);
    }

    private void OnEnable()
    {
        _gameUIController.ChangeState += ControlSpawn;
    }

    private void OnDisable()
    {
        _gameUIController.ChangeState -= ControlSpawn;
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
            Debug.Log(_timeOfAppearance);
            _timeOfAppearance = Random.Range(_minTimeOfAppearance, _maxTimeOfAppearance);
        }
    }
}