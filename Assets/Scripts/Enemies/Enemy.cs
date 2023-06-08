using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private EnemyHealthBar _healthBarSample;
    [SerializeField] private Coin _coin;
    [SerializeField] private Battery _battery;
    [SerializeField] private Vector3 _additionalPositionToPlayer;
    [SerializeField] private int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] private bool _canBeDestroyedByPlayer;
    [SerializeField] private bool _giveRewardAfterDie;

    const string ChangeColorToDefaultMethodName = "ChangeColorToDefault";

    protected SpriteRenderer SpriteRenderer;
    protected Player Player;

    private UnityEngine.Color _hitColor = UnityEngine.Color.red;
    private Vector3 _healthBarPosition;
    private UnityEngine.Color _defaultColor;
    private float _colorReturnTime;
    private EnemyHealthBar _healthBar;

    public bool CanBeDestroyedByPlayer { get; private set; }
    public Vector3 AdditionalPositionToPlayer { get; private set; }

    private void Awake()
    {
        _healthBarPosition = new Vector3(0f, 0.6f, 0f);
        _colorReturnTime = 0.1f;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = SpriteRenderer.color;
        CanBeDestroyedByPlayer = _canBeDestroyedByPlayer;
        AdditionalPositionToPlayer = _additionalPositionToPlayer;
        ShowHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _speed = 0;
            player.Die();
        }
    }

    public void Init(Player player)
    {
        Player = player;
    }

    public void TakeDamage(int damage)
    {
        if (_health - damage <= 0)
        {
            Die();
        }
        else
        {
            SpriteRenderer.color = _hitColor;
            Invoke(ChangeColorToDefaultMethodName, _colorReturnTime);
            _health -= damage;
            _healthBar.HealthChange(_health);
        }
    }

    private void ShowHealthBar()
    {
        if (_health > 1 && _canBeDestroyedByPlayer == true)
        {
            _healthBar = Instantiate(_healthBarSample, transform.position + _healthBarPosition, Quaternion.identity);
            _healthBar.SetMaxHealth(_health);
            _healthBar.transform.SetParent(transform);
        }
    }

    private void ChangeColorToDefault()
    {
        SpriteRenderer.color = _defaultColor;
    }

    public virtual void Die() 
    {
        Destroy(gameObject);

        if(_giveRewardAfterDie)
        {
            int randomResourceNumber = Random.Range(0, 2);

            if (randomResourceNumber == 0)
            {
                var dropdownResource = Instantiate(_coin, transform.position, Quaternion.identity);
                dropdownResource.SetVolume();
            }
            else
            {
                var dropdownResource = Instantiate(_battery, transform.position, Quaternion.identity);
                dropdownResource.SetVolume();
            }
        }
    }
}