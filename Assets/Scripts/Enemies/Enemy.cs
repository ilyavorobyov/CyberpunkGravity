using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected AudioSource DieSound;
    [SerializeField] protected AudioSource HitSound;
    [SerializeField] protected int AddToXPosition;
    [SerializeField] private Coin _coin;
    [SerializeField] private Battery _battery;
    [SerializeField] private int _maxHealth;
    [SerializeField] private EnemyHealthBar _healthBarSample;
    [SerializeField] private bool _canBeDestroyedByPlayer;
    [SerializeField] private bool _isDropResourceAfterDie;

    protected const string AttackAnimationName = "Attack";
    protected const string DieAnimationName = "Die";
    protected const string IdleAnimationName = "Idle";

    protected SpriteRenderer SpriteRenderer;
    protected Animator ObjectAnimator;
    protected Player PlayerObject;
    protected int MaxHealth;
    protected int CurrentHealth;
    protected EnemyHealthBar EnemyHealthBar;
    protected Vector3 StartPosition;
    protected Vector3 PlayerPosition;

    private UnityEngine.Color _hitColor = UnityEngine.Color.red;
    private Vector3 _healthBarPosition;
    private UnityEngine.Color _defaultColor;
    private float _colorReturnTime;

    public float Speed { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = _maxHealth;
        _healthBarPosition = new Vector3(0f, 0.6f, 0f);
        _colorReturnTime = 0.1f;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        ObjectAnimator = GetComponent<Animator>();
        _defaultColor = SpriteRenderer.color;
        ShowHealthBar();
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
            Invoke(nameof(ChangeColorToDefault), _colorReturnTime);
            CurrentHealth -= damage;
            EnemyHealthBar.HealthChange(CurrentHealth);
            HitSound.PlayDelayed(0);
        }
    }

    public bool IsCanBeDestroy()
    {
        if (_canBeDestroyedByPlayer) return true;
        return false;
    }

    public void Init(Player player, float speed)
    {
        PlayerObject = player;
        Speed = speed;
    }

    public void AttackPlayer(float speed)
    {
        Speed = speed;
        SetStartInfo();
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

    protected void SetEnemyActive()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetStartInfo() { }

    public virtual void Die()
    {
        if (_isDropResourceAfterDie)
        {
            int numberOfResourse = Random.Range(0, 2);

            if(numberOfResourse == 0)
            {
                var coin = Instantiate(_coin, transform.position, Quaternion.identity);
                coin.SetVolume();
                coin.SetSpeed(Speed);
            }
            else
            {
                var battery = Instantiate(_battery, transform.position, Quaternion.identity);
                battery.SetVolume();
                battery.SetSpeed(Speed);
            }
        }

        ObjectAnimator.SetTrigger(DieAnimationName);
        Invoke(nameof(SetEnemyActive), 0.3f);
        DieSound.PlayDelayed(0);
    }
}