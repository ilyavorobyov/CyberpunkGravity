using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(WeaponController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private PlayerBullet _playerBullet;
    [SerializeField] private GameUIController _gameStateController;

    private WeaponController _weaponController;
    private PlayerCollisionHandler _playerCollisionHandler;
    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;
    private bool _isAlteredGravity;
    private bool _canGravityChange;
    private bool _onMenu;
    private float _normalGravity = 2;
    private float _reversedGravity = -2;
    private Vector3 _reversedRotation = new Vector3(0, 180, 180);

    private void Awake()
    {
        _startPosition = new Vector3(0, 0f, -3);
        _onMenu = true;
        _canGravityChange = true;
        _isAlteredGravity = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponController = GetComponent<WeaponController>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
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

    public void TurnOnGravityChanger()
    {
        _canGravityChange = true;
    }

    public void TurnOffGravityChanger()
    {
        _canGravityChange = false;
        SwitchToStandardGravity();
    }

    private void StartGame()
    {
        ChangeState(false);
        SetStartPosition();
        SwitchToStandardGravity();
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    private void GameOverState()
    {
        ChangeState(true);
    }

    private void ChangeState(bool state)
    {
        _onMenu = state;
        _weaponController.ChangeOnMenuValue(_onMenu);
    }

    private void OnJump()
    {
        if (!_onMenu)
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
        if (!_onMenu && _canGravityChange)
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
            }
        }
    }

    private void SwitchToStandardGravity()
    {
        _isAlteredGravity = false;
        _rigidbody.gravityScale = _normalGravity;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}