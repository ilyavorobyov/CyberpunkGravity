using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDrone : Enemy
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Sprite _runningSprite;

    private SpriteRenderer _spriteRenderer;
    private float _distanceToPlayer;
    private float _attackDistance = 10;
    private float _yPosition;
    private float _maxY = 7.6f;
    private float _minY = 0.1f;
    private bool _isRunning;
    private bool _isAttack;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            var step = (Speed + 2) * Time.deltaTime;
            _spriteRenderer.sprite = _attackSprite;
            transform.position = Vector2.MoveTowards(transform.position, PlayerObject.transform.position, step);
        }
    }

    public override void SetStartInfo()
    {
        _isRunning = true;
        _isAttack = false;
        _spriteRenderer.sprite = _runningSprite;
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