using System.Collections;
using UnityEngine;

public class EnemyMissileSystem : Enemy
{
    [SerializeField] private Rocket _enemyShooterMissile;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _minYDirectionMissle;
    [SerializeField] private float _maxYDirectionMissle;
    [SerializeField] private float _shootXPosition;
    [SerializeField] private float _startYPosition;
    [SerializeField] private float _shootYPosition;
    [SerializeField] private Sprite _shotPointAttackSprite;
    [SerializeField] private AudioSource _shootSound;

    private Sprite _shotPointIdleSprite;
    private ShotPoint _shotPoint;
    private SpriteRenderer _shotPointSpriteRenderer;
    private Coroutine _missileShooting;
    private bool _isShooting = true;
    private Vector3 _shootingPosition;
    private float _step = 0.015f;
    private bool _isStarted = false;

    private void Start()
    {
        _shotPoint = GetComponentInChildren<ShotPoint>();
        _shotPointSpriteRenderer = _shotPoint.GetComponent<SpriteRenderer>();
        _shotPointIdleSprite = _shotPointSpriteRenderer.sprite;

        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }

        _missileShooting = StartCoroutine(MissileShooting());

        _isStarted = true;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _shootingPosition, _step);
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);
        CurrentHealth = MaxHealth;
        EnemyHealthBar.HealthChange(CurrentHealth);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = PlayerPosition + new Vector3(PlayerPosition.x + AddToXPosition, _startYPosition, 0);
        transform.position = StartPosition;
        _shootingPosition = new Vector3(_shootXPosition, _shootYPosition, 0);
    }

    private void OnDisable()
    {
        if (_missileShooting != null)
        {
            StopCoroutine(_missileShooting);
        }
    }

    private void OnEnable()
    {
        if (_isStarted)
        {
            if (_missileShooting != null)
            {
                StopCoroutine(_missileShooting);
            }

            _missileShooting = StartCoroutine(MissileShooting());
        }
    }

    private IEnumerator MissileShooting()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        while (_isShooting)
        {
            _shotPointSpriteRenderer.sprite = _shotPointIdleSprite;
            yield return waitForSeconds;
            float yDirectionMissle = Random.Range(_minYDirectionMissle, _maxYDirectionMissle);
            var missile = Instantiate(_enemyShooterMissile, _shotPoint.transform.position, Quaternion.identity);
            missile.SetSpeed(Speed);
            missile.SetYDirection(yDirectionMissle);
            _shootSound.PlayDelayed(0);
            _shotPointSpriteRenderer.sprite = _shotPointAttackSprite;
            yield return waitForSeconds;
        }
    }
}