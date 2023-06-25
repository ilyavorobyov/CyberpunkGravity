using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDrone : Enemy
{
    private float _distanceToPlayer;
    private float _attackDistance = 10;
    private float _yPosition;
    private float _maxY = 7.6f;
    private float _minY = 0.1f;
    private bool _isRunning;
    private bool _isAttack;

    private void Update()
    {
        if (_isRunning)
        {
            _distanceToPlayer = Vector3.Distance(PlayerObject.transform.position, transform.position);
            transform.Translate(Vector3.left * Speed * Time.deltaTime);

            if (_distanceToPlayer <= _attackDistance)
            {
                _isRunning = false;
                _isAttack = true;
            }
        }

        if (_isAttack)
        {
            ObjectAnimator.SetTrigger(AttackAnimationName);
            var step = (Speed + 2) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, PlayerObject.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ObjectAnimator.SetTrigger(DieAnimationName);
            Invoke("SetEnemyActive", 0.3f);
        }

        if (collision.TryGetComponent(out PlayerForceField forceField))
        {
            ObjectAnimator.SetTrigger(DieAnimationName);
            Invoke("SetEnemyActive", 0.3f);
        }
    }

    public override void SetStartInfo()
    {
        _isRunning = true;
        _isAttack = false;
        _yPosition = Random.Range(_minY, _maxY);
        gameObject.SetActive(true);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = new Vector3(AddToXPosition, _yPosition, 0);
        transform.position = StartPosition;
    }

    public override void Die()
    {
        base.Die();
    }
}