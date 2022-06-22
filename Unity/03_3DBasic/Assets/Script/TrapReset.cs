using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapReset : MonoBehaviour
{
    public GameObject player = null;

    private Vector3 ResetPosition = Vector3.zero;

    private void Start()
    {
        ResetPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("발생...");
            player.transform.Translate(ResetPosition);
            Debug.Log(player.transform.position);
        }
    }
}
