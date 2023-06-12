using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDrone : Enemy
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Sprite _runningSprite;

    private SpriteRenderer _spriteRenderer;
    private float _distanceToPlayer;
    private float _attackDistance = 10;
    private bool _isRunning;
    private bool _isAttack;
    private Vector3 _startPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isRunning = true;
        _isAttack = false;
        _startPosition = new Vector3(StartPositionFromPlayer.x, 3, 0);
        transform.position = _startPosition;
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
        _spriteRenderer.sprite = _runningSprite;
        float _maxY = 7.6f;
        float _minY = 0.1f;
        float _yPosition = Random.Range(_minY, _maxY);
        Vector3 position = new Vector3(StartPositionFromPlayer.x, _yPosition, StartPositionFromPlayer.z);
        transform.position = position;
    }

    public override void Die()
    {
        base.Die();
    }
}