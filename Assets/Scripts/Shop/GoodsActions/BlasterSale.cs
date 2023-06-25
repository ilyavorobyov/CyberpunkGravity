using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSale : PurchasedEffect
{
    [SerializeField] private SuperBlaster _blackBlaster;

    public override void Buy()
    {
        _blackBlaster.Buying();
    }
}
