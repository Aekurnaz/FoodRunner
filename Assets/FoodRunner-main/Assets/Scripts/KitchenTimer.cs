using Kitchen;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Timer;

public class KitchenTimer : IKitchen
{

    private float _fillAmount;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
