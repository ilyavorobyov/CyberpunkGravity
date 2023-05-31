using System.Drawing;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    const string ChangeColorToDefaultMethodName = "ChangeColorToDefault";
    [SerializeField] private int _health;
    [SerializeField] private EnemyHealthBar _healthBarSample;
    [SerializeField] private Coin _coin;
    [SerializeField] private Battery _battery;
    [SerializeField] private int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] private bool _canBeDestroyedByPlayer;

    private UnityEngine.Color _hitColor = UnityEngine.Color.red;
    private Vector3 _healthBarPosition;
    private UnityEngine.Color _defaultColor;
    protected SpriteRenderer SpriteRenderer;
    private float _colorReturnTime;
    private EnemyHealthBar _healthBar;
    protected Player Player;
    public bool CanBeDestroyedByPlayer { get; private set; }

    private void Awake()
    {
        _healthBarPosition = new Vector3(0f, 0.6f, 0f);
        _colorReturnTime = 0.1f;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = SpriteRenderer.color;
        CanBeDestroyedByPlayer = _canBeDestroyedByPlayer;
        ShowHealthBar();
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

    private void ChangeColorToDefault()
    {
        SpriteRenderer.color = _defaultColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Die();
        }
    }

    public virtual void Die() 
    {
        Destroy(gameObject);
        Debug.Log("die enemy");
        int randomResourceNumber = Random.Range(0, 2);

        if(randomResourceNumber == 0)
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