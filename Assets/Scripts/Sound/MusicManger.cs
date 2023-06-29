using UnityEngine;

public class MusicManger : MonoBehaviour
{
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private AudioSource _gameMusic;
    [SerializeField] private GameUI _gameUI;

    private void Start()
    {
        _menuMusic.PlayDelayed(0);
    }

    private void OnEnable()
    {
        _gameUI.StartGame += TurnGameMusic;
        _gameUI.MenuButtonClick += TurnMenuMusic;
    }

    private void OnDisable()
    {
        _gameUI.StartGame -= TurnGameMusic;
        _gameUI.MenuButtonClick -= TurnMenuMusic;
    }

    private void TurnMenuMusic()
    {
        _menuMusic.PlayDelayed(0);
        _gameMusic.Stop();
    }

    private void TurnGameMusic()
    {
        _gameMusic.PlayDelayed(0);
        _menuMusic.Stop();
    }
}