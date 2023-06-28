using UnityEngine;

public class Saw : Enemy
{
    [SerializeField] private bool _isSmallSaw;
    [SerializeField] private AudioSource _workSound;

    private float _maxYSmallSaw = 7;
    private float _minYSmallSaw = 0.6f;
    private float _maxYBigSaw = 6.2f;
    private float _minYBigSaw = 1.2f;
    private float _yPosition;

    private void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);

        if (_isSmallSaw)
        {
            _yPosition = Random.Range(_minYSmallSaw, _maxYSmallSaw);
            StartPosition = new Vector3(AddToXPosition, _yPosition, 0);
        }
        else
        {
            _yPosition = Random.Range(_minYBigSaw, _maxYBigSaw);
            StartPosition = new Vector3(AddToXPosition, _yPosition, 0);
        }

        transform.position = StartPosition;
        _workSound.PlayDelayed(0);
    }
}