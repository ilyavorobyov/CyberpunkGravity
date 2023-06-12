using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction = Vector3.left;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetYDirection(float yDirection)
    {
        _direction.y = yDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BackFrame backFrame))
        {
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Player player))
        {
            player.Die();
        }

        if(collision.TryGetComponent(out PlayerBullet bullet))
        {
            Destroy(gameObject);
            bullet.Die();
        }
    }
}