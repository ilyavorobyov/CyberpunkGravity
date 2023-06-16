using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu(fileName = "Good", menuName = "Create New Good")]
public class Good : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private PurchasedEffect _purchasedEffect;
    [SerializeField] private bool _isRepurchasable;
    [SerializeField] private bool _isBuyed;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public PurchasedEffect PurchasedEffect => _purchasedEffect;
    public bool IsRepurchasable => _isRepurchasable;
    public bool IsBuyed => _isBuyed;

    public void SetBuyed()
    {
        _isBuyed = true;
    }
}