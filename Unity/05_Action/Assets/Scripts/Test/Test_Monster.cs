using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Monster : MonoBehaviour
{
    public Enemy enemy = null;

    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            enemy.TakeDamage(20);
        }
    }
}
