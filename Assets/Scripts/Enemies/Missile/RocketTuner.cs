using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketTuner : Enemy
{
    [SerializeField] private Rocket _rocket;
    [SerializeField] private float _iterationsNumber;
    [SerializeField] private float _iterationTime;
    [SerializeField] private Sprite _secondSprite;

    private Sprite _startSprite;
    private Coroutine _launchRocket;
    private Camera _camera;
    private Vector3 _positionToConvert;
    private float _xPosition;

    private void Start()
    {
        _camera = Camera.main;
        _positionToConvert = new Vector3(Screen.width, 0, 0);
        _startSprite = SpriteRenderer.sprite;
    }

    private void Update()
    {
        _xPosition = _camera.ScreenToWorldPoint(_positionToConvert).x -1;
        transform.position = new Vector3(_xPosition, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
    }

    private void OnDisable()
    {
        if (_launchRocket != null)
        {
            StopCoroutine(_launchRocket);
        }
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);

        if (_launchRocket != null)
        {
            StopCoroutine(_launchRocket);
        }

        _launchRocket = StartCoroutine(LaunchRocket());
        PlayerPosition = PlayerObject.transform.position;
    }

    private IEnumerator LaunchRocket()
    {
        var waitForSeconds = new WaitForSeconds(_iterationTime);

        for (int i = 0; i <= _iterationsNumber; i++)
        {
            yield return waitForSeconds;
            SpriteRenderer.sprite = _startSprite;
            yield return waitForSeconds;
            SpriteRenderer.sprite = _secondSprite;
        }

        Instantiate(_rocket, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}