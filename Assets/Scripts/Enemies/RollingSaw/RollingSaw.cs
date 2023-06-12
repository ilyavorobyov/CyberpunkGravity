using UnityEngine;

public class RollingSaw : Enemy
{
    [SerializeField] private float _speed;

    private float _maxYPosition = 7.36f;
    private float _minYPosition = 0.12f;
    private Vector3 _position;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        int yPositionNumber = Random.Range(0, 2);

        if (yPositionNumber == 0 )
        {
            _position = StartPositionFromPlayer + new Vector3(0f, _maxYPosition, 0f);
        }
        else
        {
            _position = StartPositionFromPlayer + new Vector3(0f, _minYPosition, 0f);
        }

        transform.position = _position;
    }
}