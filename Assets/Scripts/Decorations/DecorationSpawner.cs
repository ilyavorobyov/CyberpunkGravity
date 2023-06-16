using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] private List<Decoration> _decorationSamples;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameUIController _gameUIController;
    [SerializeField] private int _maxRespawnTime;
    [SerializeField] private int _minRespawnTime;

    private Coroutine _respawnDecoration;
    private List<Decoration> _decorations = new List<Decoration>();
    private Vector3 _respawnPosition;

    private void Start()
    {
        _respawnPosition = new Vector3(21f, 0f, 1f);

        foreach (var decorationSample in _decorationSamples)
        {
            _respawnPosition = new Vector3(23f, decorationSample.GetYPosition(), 1f);
            var decoration = Instantiate(decorationSample, _respawnPosition, Quaternion.identity);
            decoration.Init(_scoreManager, _respawnPosition);
            decoration.transform.SetParent(transform, false);
            decoration.gameObject.SetActive(false);
            _decorations.Add(decoration);
        }
    }

    private void OnEnable()
    {
        _gameUIController.ChangeState += ControlSpawner;
    }

    private void OnDisable()
    {
        _gameUIController.ChangeState -= ControlSpawner;
    }

    private bool CheckActive()
    {
        foreach (var decoration in _decorations)
        {
            if (decoration.gameObject.activeSelf == true)
            {
                return true;
            }
        }

        return false;
    }

    private void ControlSpawner(bool state)
    {
        if (!state)
        {
            if (_respawnDecoration != null)
            {
                StopCoroutine(_respawnDecoration);
            }

            _respawnDecoration = StartCoroutine(RespawnDecoration());
        }
        else
        {
            if (_respawnDecoration != null)
            {
                StopCoroutine(_respawnDecoration);
            }
        }
    }

    private int GetRandomNumberDecoration()
    {
        int randomNumber = Random.Range(0, _decorations.Count);
        return randomNumber;
    }

    private IEnumerator RespawnDecoration()
    {
        int waitTime = Random.Range(_minRespawnTime, _maxRespawnTime);
        var waitForSeconds = new WaitForSeconds(waitTime);

        while(true)
        {
            if(!CheckActive())
            {
                _decorations[GetRandomNumberDecoration()].gameObject.SetActive(true);
            }

            yield return waitForSeconds;
        }
    }
}