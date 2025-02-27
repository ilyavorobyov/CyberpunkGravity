using UI;
using UnityEngine;

namespace PlayerCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerCollisionHandler))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private PlayerBullet _playerBullet;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private AudioSource _jumpSound;
        [SerializeField] private AudioSource _changeGravitySound;

        private const string RunAnimationName = "Run";
        private const string IdleAnimationName = "Idle";
        private const string JumpAnimationName = "Jump";
        private const string DieAnimationName = "Die";

        private WeaponController _weaponController;
        private WeaponViewObject _weaponViewObject;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
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
            _weaponViewObject = GetComponentInChildren<WeaponViewObject>();
            _animator = GetComponent<Animator>();
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        private void OnEnable()
        {
            _gameUI.ChangeState += OnChangeState;
            _gameUI.StartGame += OnStartGame;
            _gameUI.GameOver += OnGameOverState;
            _gameUI.MenuButtonClick += OnMenuButtonClick;
        }

        private void OnDisable()
        {
            _gameUI.ChangeState -= OnChangeState;
            _gameUI.StartGame -= OnStartGame;
            _gameUI.GameOver -= OnGameOverState;
            _gameUI.MenuButtonClick -= OnMenuButtonClick;
        }

        public void PlayDeathAnimation()
        {
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animator.SetTrigger(DieAnimationName);
            _weaponViewObject.gameObject.SetActive(false);
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

        private void OnStartGame()
        {
            OnChangeState(false);
            SwitchToStandardGravity();
            _animator.updateMode = AnimatorUpdateMode.Normal;
            _animator.SetTrigger(RunAnimationName);
            _weaponViewObject.gameObject.SetActive(true);
        }

        private void OnMenuButtonClick()
        {
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            SwitchToStandardGravity();
            _animator.SetTrigger(IdleAnimationName);
            transform.position = _startPosition;
            _weaponViewObject.gameObject.SetActive(true);
        }

        private void OnGameOverState()
        {
            OnChangeState(true);
        }

        private void OnChangeState(bool state)
        {
            _onMenu = state;
            _weaponController.ChangeOnMenuValue(_onMenu);
        }

        private void OnJump()
        {
            if (!_onMenu)
            {
                _jumpSound.PlayDelayed(0);

                if (!_isAlteredGravity)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                    _animator.SetTrigger(JumpAnimationName);
                }
                else
                {
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.AddForce(Vector2.down * _jumpForce, ForceMode2D.Impulse);
                    _animator.SetTrigger(JumpAnimationName);
                }
            }
        }

        private void OnChangeGravity()
        {
            if (!_onMenu && _canGravityChange)
            {
                _changeGravitySound.PlayDelayed(0);

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
}