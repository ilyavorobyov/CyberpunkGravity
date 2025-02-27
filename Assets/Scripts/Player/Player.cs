using UnityEngine;
using UnityEngine.Events;

namespace PlayerCharacter
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;

        public event UnityAction PlayerDied;

        public PlayerMover GetMover()
        {
            _playerMover = GetComponent<PlayerMover>();
            return _playerMover;
        }

        public void Die()
        {
            PlayerDied.Invoke();
        }
    }
}