using UnityEngine;

public class EnemyLaserShot : Enemy
{
    private float _speed = 6;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ObjectAnimator.SetTrigger(DieAnimationName);
            Destroy(gameObject, 0.2f);
        }
    }
}