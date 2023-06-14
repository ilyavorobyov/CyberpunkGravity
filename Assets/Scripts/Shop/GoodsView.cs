using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    public void Render(Good good, Wallet wallet)
    {
        _label.text = good.Label;
        _price.text = good.Price.ToString();
        _icon.sprite = good.Icon;

        if(wallet.Coins <= good.Price)
        {
            _sellButton.interactable = false;
        }
    }
}