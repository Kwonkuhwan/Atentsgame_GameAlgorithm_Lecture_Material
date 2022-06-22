using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    protected Animator ani = null;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void Open()
    {
        ani.SetBool("IsOpen", true);
    }

    public void Close()
    {
        ani.SetBool("IsOpen", false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }
}
