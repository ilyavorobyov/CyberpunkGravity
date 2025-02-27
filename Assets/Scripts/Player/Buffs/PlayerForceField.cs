using Enemies;
using UnityEngine;

namespace PlayerCharacter.Buffs
{
    public class PlayerForceField : MonoBehaviour
    {
        private const string ForceFieldDurationName = "ForceFieldDuration";

        private int _defaultDuration = 6;
        private int _duration;

        private void Awake()
        {
            SetDuration();
            gameObject.SetActive(false);
        }

        public int GetDuration()
        {
            SetDuration();
            return _duration;
        }

        private void SetDuration()
        {
            if (PlayerPrefs.HasKey(ForceFieldDurationName))
            {
                _duration = PlayerPrefs.GetInt(ForceFieldDurationName);
            }
            else
            {
                _duration = _defaultDuration;
                PlayerPrefs.SetInt(ForceFieldDurationName, _duration);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EnemyDrone enemyDrone))
                enemyDrone.Die();
        }
    }
}