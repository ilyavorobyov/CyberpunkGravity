using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(WeaponController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private PlayerBullet _playerBullet;
    [SerializeField] private GameUIController _gameStateController;
    [SerializeField] private PowerBoots _powerBoots;

    private WeaponController _weaponController;
    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;
    private bool _isAlteredGravity;
    private bool _canGravityChange;
    private bool _onMenu;
    private float _normalGravity = 2;
    private float _reversedGravity = -2;
    private Vector3 _reversedRotation = new Vector3(0, 180, 180);

    public float Speed { get; private set; }

    private void Awake()
    {
        _startPosition = new Vector3(0, -3.96f, -3);
        _onMenu = true;
        Speed = 0;
        _canGravityChange = true;
        _isAlteredGravity = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponController = GetComponent<WeaponController>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);

        if(_rigidbody.velocity.y != 0)
        {
            _powerBoots.gameObject.SetActive(true);
        }
        else
        {
            _powerBoots.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _gameStateController.ChangeState += ChangeState;
        _gameStateController.StartGame += StartGame;
        _gameStateController.GameOver += GameOverState;
        _gameStateController.MenuButtonClick += SetStartPosition;
    }

    private void OnDisable()
    {
        _gameStateController.ChangeState -= ChangeState;
        _gameStateController.StartGame -= StartGame;
        _gameStateController.GameOver -= GameOverState;
        _gameStateController.MenuButtonClick -= SetStartPosition;
    }

    private void StartGame()
    {
        Speed = _speed;
        ChangeState(false);
        SetStartPosition();
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    private void GameOverState()
    {
        Speed = 0;
        ChangeState(true);
    }

    private void ChangeState(bool state)
    {
        _onMenu = state;
        _weaponController.ChangeOnMenuValue(_onMenu);
    }

    private void OnJump()
    {
        if(!_onMenu)
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
    }

    private void OnChangeGravity()
    {
        if(!_onMenu)
        {
            if (_canGravityChange)
            {
                if (!_isAlteredGravity)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _isAlteredGravity = true;
                    _rigidbody.gravityScale = _reversedGravity;
                    transform.rotation = Quaternion.Euler(_reversedRotation);
                }
                else
                {
                    _rigidbody.velocity = Vector2.zero;
                    SwitchToStandardGravity();
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }
    }
    
    private void SwitchToStandardGravity()
    {
        _isAlteredGravity = false;
        _rigidbody.gravityScale = _normalGravity;
    }

    public void TurnOnGravityChanger()
    {
        _canGravityChange = true;
    }

    public void TurnOffGravityChanger()
    {
        SwitchToStandardGravity();
        _canGravityChange = false;
    }
}