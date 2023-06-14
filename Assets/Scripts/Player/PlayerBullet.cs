using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;

    private const string DieMethodName = "Die";

    private void Start()
    {
        Invoke(DieMethodName, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.CanBeDestroyedByPlayer == true)
            {
                enemy.TakeDamage(_damage);
                Die();
            }
            else
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}