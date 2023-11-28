using UnityEngine;

public class Decoration : MonoBehaviour
{
    [SerializeField] private float _speedReducer;
    [SerializeField] private float _yPosition;

    private float _speed;
    private float _minSpeed = 1;
    private ScoreManager _scoreManager;
    private Vector3 _startPosition;

    private void Start()
    {
        _scoreManager.SpeedChange += SetSpeed;
        SetSpeed(_scoreManager.GetSpeed());
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BackFrame backFrame))
        {
            gameObject.SetActive(false);
            transform.position = _startPosition;
        }
    }

    public void Init(ScoreManager scoreManager, Vector3 startPosition)
    {
        _scoreManager = scoreManager;
        _startPosition = startPosition;
    }

    public float GetYPosition()
    {
        return _yPosition;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed - _speedReducer;

        if (_speed <= 0)
            _speed = _minSpeed;
    }
}