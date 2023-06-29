using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _volume;
    [SerializeField] private int _minVolume;
    [SerializeField] private int _maxVolume;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnValidate()
    {
        if (_minVolume >= _maxVolume)
        {
            _minVolume = _maxVolume - 1;
        }
    }

    public void SetVolume()
    {
        int randomVolume = Random.Range(_minVolume, _maxVolume);
        _volume = randomVolume;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public int GetVolume()
    {
        return _volume;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}