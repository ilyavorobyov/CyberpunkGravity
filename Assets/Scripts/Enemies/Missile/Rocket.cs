using UnityEngine;

public class Rocket : Enemy
{
    [SerializeField] private float _speed;

    private Vector3 _direction = Vector3.left;

    private void OnEnable()
    {
        GameUIController.RocketsRemovalEvent += Delete;
    }

    private void OnDisable()
    {
        GameUIController.RocketsRemovalEvent -= Delete;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ObjectAnimator.SetTrigger(DieAnimationName);
            _speed = 0;
            Destroy(gameObject, 0.2f);
        }

        if (collision.TryGetComponent(out PlayerBullet playerBullet))
        {
            ObjectAnimator.SetTrigger(DieAnimationName);
            _speed = 0;
            Destroy(gameObject, 0.2f);
        }
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void AddSpeed(float speed)
    {
        _speed += speed;
    }
    public void SetYDirection(float yDirection)
    {
        _direction.y = yDirection;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}