using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSale : PurchasedEffect
{
    [SerializeField] private SuperBlaster _superBlaster;

    public override void Buy()
    {
        _superBlaster.Buying();
    }
}
