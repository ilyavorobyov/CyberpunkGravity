using UnityEngine;

public class WindowState : MonoBehaviour
{
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private GameUI _gameUi;

    private void OnApplicationPause(bool pause)
    {
        _audioListener.enabled = false;

        if (_gameUi.IsGameOn)
        {
            _gameUi.PauseGame();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        _audioListener.enabled = true;
    }
}