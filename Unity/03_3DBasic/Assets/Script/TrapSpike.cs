using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{

    private Animator ani = null;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ani.SetTrigger("Activate");
            IDead dead = other.GetComponent<IDead>();
            dead?.Die();
        }
    }
}
