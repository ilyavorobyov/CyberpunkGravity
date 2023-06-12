using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private EnemyHealthBar _healthBarSample;
    [SerializeField] private Vector3 _startPositionFromPlayer;
    [SerializeField] private int _damage;
    [SerializeField] private bool _canBeDestroyedByPlayer;

    const string ChangeColorToDefaultMethodName = "ChangeColorToDefault";

    protected SpriteRenderer SpriteRenderer;
    protected Player PlayerObject;
    protected Vector3 StartPositionFromPlayer;
    protected int MaxHealth;
    protected int CurrentHealth;
    protected EnemyHealthBar EnemyHealthBar;

    private UnityEngine.Color _hitColor = UnityEngine.Color.red;
    private Vector3 _healthBarPosition;
    private UnityEngine.Color _defaultColor;
    private float _colorReturnTime;

    public bool CanBeDestroyedByPlayer { get; private set; }
    public float Speed { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = _maxHealth;
        _healthBarPosition = new Vector3(0f, 0.6f, 0f);
        _colorReturnTime = 0.1f;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = SpriteRenderer.color;
        CanBeDestroyedByPlayer = _canBeDestroyedByPlayer;
        StartPositionFromPlayer = _startPositionFromPlayer;
        ShowHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Speed = 0;
            player.Die();
        }

        if(collision.TryGetComponent(out BackFrame backFrame))
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(Player player, float speed)
    {
        PlayerObject = player;
        Speed = speed;
    }
    public void TakeDamage(int damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            Die();
        }
        else
        {
            SpriteRenderer.color = _hitColor;
            Invoke(ChangeColorToDefaultMethodName, _colorReturnTime);
            CurrentHealth -= damage;
            EnemyHealthBar.HealthChange(CurrentHealth);
        }
    }

    public void AttackPlayer(float speed)
    {
        Speed = speed;
        gameObject.SetActive(true);
    }

    private void ShowHealthBar()
    {
        if (CurrentHealth > 1 && _canBeDestroyedByPlayer == true)
        {
            EnemyHealthBar = Instantiate(_healthBarSample, transform.position + _healthBarPosition, Quaternion.identity);
            EnemyHealthBar.SetMaxHealth(CurrentHealth);
            EnemyHealthBar.transform.SetParent(transform);
        }
    }

    private void ChangeColorToDefault()
    {
        SpriteRenderer.color = _defaultColor;
    }

    public virtual void Die() 
    {
        gameObject.SetActive(false);
    }
}