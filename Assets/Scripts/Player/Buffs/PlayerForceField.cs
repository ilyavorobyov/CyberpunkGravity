using UnityEngine;

public class PlayerForceField : MonoBehaviour
{
    [SerializeField] private int _duration;

    public int GetDuration()
    {
        return _duration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyDrone enemyDrone))
        {
            Debug.Log("coollll");
            enemyDrone.Die();
        }
    }
}