using UnityEngine;

namespace ShopBehaviour.GoodsActions
{
    public class ShieldTime : PurchasedEffect
    {
        private const string ForceFieldDurationName = "ForceFieldDuration";

        private int _duration;

        public override void Buy()
        {
            _duration = PlayerPrefs.GetInt(ForceFieldDurationName);
            _duration++;
            PlayerPrefs.SetInt(ForceFieldDurationName, _duration);
            Debug.Log(_duration);
        }
    }
}