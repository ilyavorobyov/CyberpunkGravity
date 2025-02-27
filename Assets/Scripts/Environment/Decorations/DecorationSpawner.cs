using System.Collections;
using System.Collections.Generic;
using Scorer;
using UI;
using UnityEngine;

namespace Environment.Decorations
{
    public class DecorationSpawner : MonoBehaviour
    {
        [SerializeField] private List<Decoration> _decorationSamples;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private int _maxRespawnTime;
        [SerializeField] private int _minRespawnTime;

        private List<Decoration> _decorations = new List<Decoration>();
        private Coroutine _respawnDecoration;
        private Vector3 _respawnPosition;
        private float _xPosition = 16;

        private void Start()
        {
            foreach (var decorationSample in _decorationSamples)
            {
                _respawnPosition = new Vector3(_xPosition, decorationSample.GetYPosition(), 1f);
                var decoration = Instantiate(decorationSample, _respawnPosition, Quaternion.identity);
                decoration.Init(_scoreManager, _respawnPosition);
                decoration.transform.SetParent(transform, false);
                decoration.gameObject.SetActive(false);
                _decorations.Add(decoration);
            }
        }

        private void OnEnable()
        {
            _gameUI.ChangeState += OnChangeState;
        }

        private void OnDisable()
        {
            _gameUI.ChangeState -= OnChangeState;
        }

        private bool CheckActive()
        {
            foreach (var decoration in _decorations)
            {
                if (decoration.gameObject.activeSelf == true)
                    return true;
            }

            return false;
        }

        private void OnChangeState(bool state)
        {
            if (!state)
            {
                if (_respawnDecoration != null)
                    StopCoroutine(_respawnDecoration);

                _respawnDecoration = StartCoroutine(RespawnDecoration());
            }
            else
            {
                if (_respawnDecoration != null)
                    StopCoroutine(_respawnDecoration);
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

            while (true)
            {
                if (!CheckActive())
                {
                    _decorations[GetRandomNumberDecoration()].gameObject.SetActive(true);
                    _decorations[GetRandomNumberDecoration()].SetSpeed(_scoreManager.GetSpeed());
                }

                yield return waitForSeconds;
            }
        }
    }
}