using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class LearningPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private TMP_Text _controlText;
    [SerializeField] private TMP_Text _weaponText;
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private TMP_Text _enemyText;
    [SerializeField] private Button _closeButton;

    private bool isRead = false;

    private void OnEnable()
    {
        _storyText.gameObject.SetActive(true);
        _controlText.gameObject.SetActive(true);
        _closeButton.onClick.AddListener(CloseButtonClick);
    }

    private void CloseButtonClick()
    {
        if(!isRead)
        {
            _storyText.gameObject.SetActive(false);
            _controlText.gameObject.SetActive(false);
            _weaponText.gameObject.SetActive(true);
            _bonusText.gameObject.SetActive(true);
            _enemyText.gameObject.SetActive(true);
            isRead = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
