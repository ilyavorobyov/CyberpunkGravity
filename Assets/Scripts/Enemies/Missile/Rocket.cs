using UnityEngine;

public class Rocket : Enemy
{
    [SerializeField] private float _additionSpeed;

    private Vector3 _direction = Vector3.left;
    private float _speed;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        GameUI.RocketsRemoval += OnRocketsRemoval;
    }

    private void OnDisable()
    {
        GameUI.RocketsRemoval -= OnRocketsRemoval;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) 
            || collision.TryGetComponent(out PlayerForceField playerForceField))
        {
            Die();
        }
    }

    public void SetYDirection(float yDirection)
    {
        _direction.y = yDirection;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed + _additionSpeed;
    }

    public void OnRocketsRemoval()
    {
        Destroy(gameObject);
    }
}