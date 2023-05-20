using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected Player _player;
    [SerializeField] private bool _candestroyedCollisionWithPlayer;

    public void Init(Player player)
    {
        _player = player;
    }

    public void TakeDamage(int damage)
    {
        if (_health - damage <= 0)
        {
            Die();
        }
        else
        {
            _health -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);

            if(_candestroyedCollisionWithPlayer)
            {
                Die();
            }
        }
    }

    protected virtual void UseSpecialAbility() { }

    protected virtual void Die() { }
}