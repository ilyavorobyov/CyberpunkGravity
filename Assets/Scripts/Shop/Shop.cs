using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Good> _goods;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GoodsView _goodsView;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        foreach (var item in _goods)
        {
            item.SetInfo();
            var good = Instantiate(_goodsView, _itemContainer.transform);
            good.Render(item, _wallet);
        }
    }
}