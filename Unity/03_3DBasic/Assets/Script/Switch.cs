using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IUseable
{
    public Door door = null;

    private Animator ani = null;
    private bool switchOnOff = false;

    void Awake()
    {
        ani = GetComponent<Animator>();
        door = transform.parent.transform.Find("DoorSwitch").GetComponent<Door>();
    }

    public void Use()
    {
        switchOnOff = !switchOnOff;
        ani.SetBool("IsSwitchOn", switchOnOff);

        if (switchOnOff == true)
        {
            door.Open();
        }
        else
            door.Close();
    }
}
