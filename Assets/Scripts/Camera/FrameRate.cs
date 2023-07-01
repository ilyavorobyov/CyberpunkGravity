using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}