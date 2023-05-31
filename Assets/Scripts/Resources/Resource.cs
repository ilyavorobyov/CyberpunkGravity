using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _volume;
    [SerializeField] private int _minVolume;
    [SerializeField] private int _maxVolume;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetVolume()
    {
        int randomVolume = Random.Range(_minVolume, _maxVolume);
        _volume = randomVolume;
    }

    public int GetVolume()
    {
        return _volume;
    }

    private void OnValidate()
    {
        if (_minVolume >= _maxVolume)
        {
            _minVolume = _maxVolume - 1;
        }
    }
}