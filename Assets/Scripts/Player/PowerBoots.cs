using UnityEngine;

public class PowerBoots : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.CanBeDestroyedByPlayer == true)
            {
                enemy.Die();
            }
        }
    }
}