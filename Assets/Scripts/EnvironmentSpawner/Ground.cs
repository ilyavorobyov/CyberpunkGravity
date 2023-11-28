using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    private float _speed;
    private Vector3 _startPosition = new Vector3(14.5f, -1.26f, 0f);
    private float _minX = 1;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < _minX)
        {
           transform.position = _startPosition;
        }
    }

    private void OnEnable()
    {
        _scoreManager.SpeedChange += OnSpeedChange;
    }

    private void OnDisable()
    {
        _scoreManager.SpeedChange -= OnSpeedChange;
    }

    private void OnSpeedChange(float speed)
    {
        _speed = speed;
    }
}