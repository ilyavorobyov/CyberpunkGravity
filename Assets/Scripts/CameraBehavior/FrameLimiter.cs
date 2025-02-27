using UnityEngine;

namespace CameraBehavior
{
    public class FrameLimiter : MonoBehaviour
    {
        private int _targetFrameRate = 60;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}