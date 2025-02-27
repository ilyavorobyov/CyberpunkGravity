using UnityEngine;

namespace Enemies
{
    public class RollingSaw : Enemy
    {
        [SerializeField] private float _addSpeed;
        [SerializeField] private AudioSource _workSound;

        private Vector2 _yPositions = new Vector2(8.15f, 0.78f);
        private int _minPositionNumber = 0;
        private int _maxPositionNumber = 2;

        private void Update()
        {
            transform.Translate(Vector3.left * (Speed + _addSpeed) * Time.deltaTime);
        }

        public override void SetStartInfo()
        {
            gameObject.SetActive(true);

            int yPositionNumber = Random.Range(_minPositionNumber, _maxPositionNumber);

            if (yPositionNumber == _minPositionNumber)
                StartPosition = new Vector3(AddToXPosition, _yPositions.y, 0f);
            else
                StartPosition = new Vector3(AddToXPosition, _yPositions.x, 0f);

            transform.position = StartPosition;
            _workSound.PlayDelayed(0);
        }
    }
}