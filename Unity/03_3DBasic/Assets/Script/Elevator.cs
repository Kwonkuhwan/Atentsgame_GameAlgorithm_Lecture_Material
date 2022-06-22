using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IUseable
{
    private Animator ani = null;
    bool moveUp = true;

    List<Rigidbody> passengers = new List<Rigidbody>();

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        foreach(Rigidbody rigid in passengers)
        {
            rigid.MovePosition(new Vector3(rigid.position.x, transform.position.y + 0.25f, rigid.position.z));
        }
    }

    public void Use()
    {
        if (moveUp) 
        {
            ani.SetTrigger("Up");
            moveUp = false;
        }
        else
        {
            ani.SetTrigger("Down");
            moveUp = true;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rigid = other.GetComponent<Rigidbody>();
        if (rigid)
        {
            Rigidbody find = passengers.Find((x) => x == rigid);

            if (find == null)
            {
                passengers.Add(rigid);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigid = other.GetComponent<Rigidbody>();
        if (rigid)
        {
            passengers.Remove(rigid);
        }
    }
}
