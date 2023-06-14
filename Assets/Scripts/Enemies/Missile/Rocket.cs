using UnityEngine;

public class Rocket : Enemy
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

    public override void Die()
    {
        Destroy(gameObject);
    }
}