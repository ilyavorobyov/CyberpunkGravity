using UnityEngine;

namespace PlayerCharacter
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponViewObject : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}