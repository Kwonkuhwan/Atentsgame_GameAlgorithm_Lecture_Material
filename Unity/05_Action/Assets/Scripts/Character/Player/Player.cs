using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject weapon = null;
    private GameObject sheild = null;

    private ParticleSystem ps = null;
    private void Awake()
    {
        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        sheild = GetComponentInChildren<FindShield>().gameObject;

        ps = weapon.GetComponentInChildren<ParticleSystem>();
    }

    public void ShowWeapons(bool isShow)
    {
        weapon.SetActive(isShow);
        sheild.SetActive(isShow);
    }

    public void TurnOnAura(bool turnOn)
    {
        if (turnOn)
            ps.Play();
        else
            ps.Stop();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("OnTriggerEnter - " + other.name);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("OnCollisionEnter - " + collision.gameObject.name);
    //}
}
