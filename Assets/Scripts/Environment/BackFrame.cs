using CollectedResources;
using Enemies;
using PlayerCharacter.Buffs;
using UnityEngine;

namespace Environment
{
    public class BackFrame : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                enemy.gameObject.SetActive(false);
            }

            if (collision.TryGetComponent(out Rocket rocket)
                || collision.TryGetComponent(out Resource resource)
                || collision.TryGetComponent(out ForceField forceField))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}