using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _volume;

    public void Die()
    {
        Destroy(gameObject);
    }

    public int GetVolume()
    {
        return _volume;
    }
}