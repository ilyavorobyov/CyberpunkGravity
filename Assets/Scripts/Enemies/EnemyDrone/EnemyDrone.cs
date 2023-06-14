using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDrone : Enemy
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Sprite _runningSprite;

    private float _addToXPosition = 22;
    private SpriteRenderer _spriteRenderer;
    private float _distanceToPlayer;
    private float _attackDistance = 10;
    float _yPosition;
    float _maxY = 7.6f;
    float _minY = 0.1f;
    private bool _isRunning;
    private bool _isAttack;
    private Vector3 _playerPosition;

    private void Start()
    {
        _yPosition = Random.Range(_minY, _maxY);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _runningSprite;
        _isRunning = true;
        _isAttack = false;
        StartPosition = new Vector3(_addToXPosition, _yPosition, 0);
        transform.position = StartPosition;
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
            var step = Speed * Time.deltaTime;
            _spriteRenderer.sprite = _attackSprite;
            transform.position = Vector2.MoveTowards(transform.position, PlayerObject.transform.position, step);
        }
    }

    private void OnEnable()
    {
        _isRunning = true;
        _isAttack = false;
        _yPosition = Random.Range(_minY, _maxY);
        StartPosition = new Vector3(_addToXPosition, _yPosition, 0);
    }

    public override void Die()
    {
        _spriteRenderer.sprite = _runningSprite;
        base.Die();
    }
}