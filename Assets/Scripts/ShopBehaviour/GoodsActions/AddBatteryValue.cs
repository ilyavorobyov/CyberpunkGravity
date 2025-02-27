using UnityEngine;

namespace ShopBehaviour.GoodsActions
{
    public class AddBatteryValue : PurchasedEffect
    {
        private const string BatteryValueName = "BatteryValue";

        private int _value;
        private int _additionalEnergy = 5;

        public override void Buy()
        {
            _value = PlayerPrefs.GetInt(BatteryValueName);
            _value += _additionalEnergy;
            PlayerPrefs.SetInt(BatteryValueName, _value);
        }
    }
}