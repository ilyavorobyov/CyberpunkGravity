using UnityEngine;

namespace PlayerCharacter.Buffs
{
    public class ForceField : Buff
    {
        private void Update()
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
}