using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Door
{
    public Key key = null;
    public float closeTime = 3.0f;

    private void Start()
    {
        key.OnKeyPickup += OpenAndClose;
    }

    private void OpenAndClose()
    {
        Open();
        StartCoroutine(OnCloseDoor(closeTime));
    }

    private IEnumerator OnCloseDoor(float delay)
    {
        yield return new WaitForSeconds(delay);

        Close();
    }

    protected override void OnTriggerEnter(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
