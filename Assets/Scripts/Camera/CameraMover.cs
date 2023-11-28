using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _xOffset;

    private void Awake()
    {
        transform.position = new Vector3(_playerPosition.position.x 
            + _xOffset, transform.position.y, transform.position.z);
    }
}