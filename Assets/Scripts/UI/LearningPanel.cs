using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LearningPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private TMP_Text _controlText;
    [SerializeField] private TMP_Text _weaponText;
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private TMP_Text _enemyText;
    [SerializeField] private TMP_Text _antigravText;
    [SerializeField] private TMP_Text _unkillableEnemiesText;
    [SerializeField] private Image _antigravImage;
    [SerializeField] private Button _closeButton;

    private int _clicksCounter = 0;

    private void OnEnable()
    {
        _storyText.gameObject.SetActive(true);
        _controlText.gameObject.SetActive(true);
        _closeButton.onClick.AddListener(CloseButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(CloseButtonClick);
    }

    public void ShowControlInfo()
    {
        _antigravText.gameObject.SetActive(false);
        _unkillableEnemiesText.gameObject.SetActive(false);
        _antigravImage.gameObject.SetActive(false);
        _storyText.gameObject.SetActive(true);
        _controlText.gameObject.SetActive(true);
        _clicksCounter = 2;
    }

    private void CloseButtonClick()
    {
        if (_clicksCounter == 0)
        {
            _storyText.gameObject.SetActive(false);
            _controlText.gameObject.SetActive(false);
            _weaponText.gameObject.SetActive(true);
            _bonusText.gameObject.SetActive(true);
            _enemyText.gameObject.SetActive(true);
            _clicksCounter++;
        }
        else if (_clicksCounter == 1)
        {
            _weaponText.gameObject.SetActive(false);
            _bonusText.gameObject.SetActive(false);
            _enemyText.gameObject.SetActive(false);
            _antigravText.gameObject.SetActive(true);
            _unkillableEnemiesText.gameObject.SetActive(true);
            _antigravImage.gameObject.SetActive(true);
            _clicksCounter++;
        }
        else if(_clicksCounter == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
