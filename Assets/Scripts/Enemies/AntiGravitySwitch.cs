using PlayerCharacter;
using UnityEngine;

namespace Enemies
{
    public class AntiGravitySwitch : Enemy
    {
        [SerializeField] private float _maxY = 7.6f;
        [SerializeField] private float _minY = 0.1f;
        [SerializeField] private AudioSource _soundOfDevice;

        private PlayerMover _playerMover;
        private float _yPosition;

        private void Start()
        {
            _playerMover = PlayerObject.GetMover();
        }

        private void Update()
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }

        public override void SetStartInfo()
        {
            CurrentHealth = MaxHealth;
            EnemyHealthBar.HealthChange(CurrentHealth);
            _yPosition = Random.Range(_minY, _maxY);
            PlayerPosition = PlayerObject.transform.position;
            StartPosition = new Vector3(AddToXPosition, _yPosition, 0);
            transform.position = StartPosition;
            gameObject.SetActive(true);
            _soundOfDevice.PlayDelayed(0);
        }

        public override void Die()
        {
            base.Die();
            _playerMover.TurnOnGravityChanger();
            _soundOfDevice.Stop();
        }
    }
}