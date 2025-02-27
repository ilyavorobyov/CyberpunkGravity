using PlayerCharacter.Weapons;
using UnityEngine;

namespace ShopBehaviour.GoodsActions
{
    public class BlasterSale : PurchasedEffect
    {
        [SerializeField] private SuperBlaster _superBlaster;

        public override void Buy()
        {
            _superBlaster.Buying();
        }
    }
}