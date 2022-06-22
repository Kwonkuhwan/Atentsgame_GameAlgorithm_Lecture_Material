using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    public Action OnKeyPickup = null;

    private void OnTriggerEnter(Collider other)
    {
        OnKeyPickup?.Invoke();
        Destroy(this.gameObject, 0.1f);
    }
}
