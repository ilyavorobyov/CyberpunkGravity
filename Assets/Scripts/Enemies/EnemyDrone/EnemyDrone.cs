using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDrone : Enemy
{
    [SerializeField] private Sprite _attackSprite;
    private float _distanceToPlayer;
    private float _attackDistance = 10;
    private bool _isRunning;
    private bool _isAttack;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isRunning = true;
        _isAttack = false;
    }

    private void Update()
    {
        if (_isRunning)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
            _distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

            if (_distanceToPlayer <= _attackDistance)
            {
                _isRunning = false;
                _isAttack = true;
            }
        }

        if(_isAttack)
        {
            var step = _speed * Time.deltaTime;
            _spriteRenderer.sprite = _attackSprite;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
