using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Good> _goods;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GoodsView _goodsView;
    [SerializeField] private GameObject _itemContainer;

    private List<GoodsView> _availableGoods = new List<GoodsView>();
    private bool _isRendered = false;

    public void RenderGoods()
    {
        if (!_isRendered)
        {
            foreach (var item in _goods)
            {
                var good = Instantiate(_goodsView, _itemContainer.transform);
                good.Render(item, _wallet);
                _availableGoods.Add(good);
                _isRendered = true;
            }
        }
        else
        {
            CheckWallet();
        }
    }

    private void CheckWallet()
    {
        foreach (var item in _availableGoods)
        {
            item.CheckWallet();
        }
    }
}