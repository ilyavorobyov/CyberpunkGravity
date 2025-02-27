using UnityEngine;

namespace PlayerCharacter.Buffs
{
    public class Buff : MonoBehaviour
    {
        protected float Speed;

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
    }
}