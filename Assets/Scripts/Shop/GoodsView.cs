using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GoodsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Good _good;
    private PurchasedEffect _purchasedEffect;
    private int _priceValue;
    private Wallet _wallet;
    private bool _repurchasable;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Good good, Wallet wallet)
    {
        _good = good;
        _label.text = good.Label;
        _price.text = good.Price.ToString();
        _icon.sprite = good.Icon;
        _priceValue = good.Price;
        _purchasedEffect = good.PurchasedEffect;
        _wallet = wallet;
        _repurchasable = good.IsRepurchasable;
        CheckWallet();

    }

    public void CheckWallet()
    {
        if (_priceValue > _wallet.Coins)
        {
            _sellButton.interactable = false;
        }
        else
        {
            _sellButton.interactable = true;
        }

        if (_good.IsBuyed)
        {
            _sellButton.interactable = false;
        }
    }

    private void OnButtonClick()
    {
        _purchasedEffect.Buy();
        _wallet.RemoveCoins(_priceValue);
        _wallet.SaveCoins();

        if(!_good.IsRepurchasable)
        {
            _good.SetBuyed();
        }

        CheckWallet();
    }
}