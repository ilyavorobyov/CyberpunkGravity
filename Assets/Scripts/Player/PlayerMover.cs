using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCollisionHandler))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private PlayerBullet _playerBullet;

    public event UnityAction<int> Shot;

    private PlayerCollisionHandler _playercollisionhandler;
    private Rigidbody2D _rigidbody;
    private bool _isAlteredGravity;
    private bool _canGravityChange;
    private int _energy;
    private float _normalGravity = 2;
    private float _reversedGravity = -2;

    private void Awake()
    {
        _canGravityChange = true;
        _isAlteredGravity = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playercollisionhandler = GetComponent<PlayerCollisionHandler>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    public void SetEnergy(int energy)
    {
        _energy = energy;
    }

    public void TurnOffGravityChanger()
    {
        SwitchToStandardGravity();
        _canGravityChange = false;
    }

    private void OnJump()
    {
        if (!_isAlteredGravity)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.down * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnChangeGravity()
    {
        if(_canGravityChange)
        {
            if (!_isAlteredGravity)
            {
                _isAlteredGravity = true;
                _rigidbody.gravityScale = _reversedGravity;
            }
            else
            {
                SwitchToStandardGravity();
            }
        }
    }
    
    private void SwitchToStandardGravity()
    {
        _isAlteredGravity = false;
        _rigidbody.gravityScale = _normalGravity;
    }

    private void TurnOnGravityChanger()
    {
        _canGravityChange = true;
    }

    private void OnEnable()
    {
        _playercollisionhandler.AntiGravitySwitchEnabled += TurnOffGravityChanger;
        _playercollisionhandler.AntiGravitySwitchOffed += TurnOnGravityChanger;
    }

    private void OnDisable()
    {
        _playercollisionhandler.AntiGravitySwitchEnabled -= TurnOffGravityChanger;
        _playercollisionhandler.AntiGravitySwitchOffed -= TurnOnGravityChanger;
    }
}