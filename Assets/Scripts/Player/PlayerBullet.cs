using Enemies;
using UnityEngine;

namespace PlayerCharacter
{
    [RequireComponent(typeof(Animator))]
    public class PlayerBullet : MonoBehaviour
    {
        private const string DieAnimationName = "Die";
        private const string HitAnimationName = "Hit";

        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        private Animator _animator;
        private float _dieAnimationDuration = 0.2f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Invoke(nameof(Die), _lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                if (enemy.IsCanBeDestroy())
                {
                    enemy.TakeDamage(_damage);
                    HitDie();
                }
                else if (!enemy.IsCanBeDestroy())
                {
                    HitDie();
                }
                else
                {
                    Die();
                }
            }
        }

        private void HitDie()
        {
            _speed = 0;
            _animator.SetTrigger(HitAnimationName);
            Destroy(gameObject, _dieAnimationDuration);
        }

        private void Die()
        {
            _animator.SetTrigger(DieAnimationName);
            Destroy(gameObject, _dieAnimationDuration);
        }
    }
}