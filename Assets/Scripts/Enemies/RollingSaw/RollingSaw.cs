using UnityEngine;

public class RollingSaw : Enemy
{
    [SerializeField] private float _speed;

    private float _maxYPosition = 7.36f;
    private float _minYPosition = 0.12f;
    private float _addToXPosition = 21;
  //  private Vector3 _playerPosition;

    private void Start()
    {
      //  _playerPosition = PlayerObject.transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        int yPositionNumber = Random.Range(0, 2);

        if (yPositionNumber == 0 )
        {
            StartPosition = new Vector3(_addToXPosition, _maxYPosition, 0f);
        }
        else
        {
            StartPosition = new Vector3(_addToXPosition, _minYPosition, 0f);
        }
    }
}