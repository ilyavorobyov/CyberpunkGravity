using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesAndBuffsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Battery _battery;
    [SerializeField] private List<Buff> _buffSamples;
    [SerializeField] private int _minTimeCreateResources;
    [SerializeField] private int _maxTimeCreateResources;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Transform[] _shapesToSpawn;
    [SerializeField] private int _chanceCreateBuff;

    private Vector3 _topSpawnPosition;
    private Vector3 _middleSpawnPosition;
    private Vector3 _bottomSpawnPosition;
    private float _topYPosition = 6.8f;
    private float _middleYPosition = 3.25f;
    private float _bottomYPosition = 0.3f;
    private float _speedObjects;
    private int _topSpawnPositionNumber = 0;
    private int _middleSpawnPositionNumber = 1;
    private int _coinRespawnNumber = 0;
    private int _maxRespawnNumber = 4;
    private int _minAdditionXPosition = 6;
    private int _maxAdditionXPosition = 13;
    private int _minNumberToCalculateChance = 0;
    private int _maxNumberToCalculateChance = 101;
    private Coroutine _createResources;

    private void Start()
    {
        _topSpawnPosition = new Vector3(23f, _topYPosition, 0f);
        _middleSpawnPosition = new Vector3(23f, _middleYPosition, 0f);
        _bottomSpawnPosition = new Vector3(23f, _bottomYPosition, 0f);
    }

    private void OnEnable()
    {
        _gameUI.ChangeState += OnChangeState;
        _scoreManager.SpeedChange += OnSpeedChange;
    }

    private void OnDisable()
    {
        _gameUI.ChangeState -= OnChangeState;
        _scoreManager.SpeedChange += OnSpeedChange;
    }

    private void OnSpeedChange(float speed)
    {
        _speedObjects = speed;
    }

    private void OnChangeState(bool state)
    {
        if (!state)
        {
            if (_createResources != null)
            {
                StopCoroutine(_createResources);
            }

            _createResources = StartCoroutine(CreateResources());
        }
        else
        {
            if (_createResources != null)
            {
                StopCoroutine(_createResources);
            }
        }
    }

    private Vector3 SelectSpawnPosition()
    {
        int numberOfPosition = Random.Range(_topSpawnPositionNumber, 3);

        if (numberOfPosition == _topSpawnPositionNumber)
        {
            return _topSpawnPosition;
        }
        else if (numberOfPosition == _middleSpawnPositionNumber)
        {
            return _middleSpawnPosition;
        }

        return _bottomSpawnPosition;
    }

    private bool SelectCoinRespawn()
    {
        int numberOfResorce = Random.Range(_coinRespawnNumber, _maxRespawnNumber);

        if (numberOfResorce == _coinRespawnNumber)
        {
            return false;
        }

        return true;
    }

    private Transform SelectSpawnShape()
    {
        int numberOfShape = Random.Range(0, _shapesToSpawn.Length);
        return _shapesToSpawn[numberOfShape];
    }

    private void ÑreateSelectedResources(bool isCoin, Vector3 position, Transform shape)
    {
        if (isCoin)
        {
            Transform[] respawnPositions = shape.GetComponentsInChildren<Transform>();

            foreach (var respawnPosition in respawnPositions)
            {
                Coin coin = Instantiate(_coin, respawnPosition.transform.position 
                    + position, Quaternion.identity);
                coin.SetSpeed(_speedObjects);
            }
        }
        else
        {
            Battery battery = Instantiate(_battery, position, Quaternion.identity);
            battery.SetSpeed(_speedObjects);
        }
    }

    private void CreateBuff(Vector3 position)
    {
        int numberToCalculateChance = Random.Range(_minNumberToCalculateChance, _maxNumberToCalculateChance);

        if (numberToCalculateChance <= _chanceCreateBuff)
        {
            int additionXPosition = Random.Range(_minAdditionXPosition, _maxAdditionXPosition);
            Vector3 buffPosition = position + new Vector3(additionXPosition, 0f, 0f);
            int numberOfBuff = Random.Range(0, _buffSamples.Count);
            var buff = Instantiate(_buffSamples[numberOfBuff], buffPosition, Quaternion.identity);
            buff.SetSpeed(_speedObjects);
        }
    }

    private IEnumerator CreateResources()
    {
        int timeCreateResources = Random.Range(_minTimeCreateResources, _maxTimeCreateResources);
        var waitForSeconds = new WaitForSeconds(timeCreateResources);

        while (true)
        {
            yield return waitForSeconds;
            Vector3 position = SelectSpawnPosition();
            ÑreateSelectedResources(SelectCoinRespawn(), position, SelectSpawnShape());
            CreateBuff(position);
            timeCreateResources = Random.Range(_minTimeCreateResources, _maxTimeCreateResources);
        }
    }
}