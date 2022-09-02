using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test_Seamless : MonoBehaviour
{
    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SubMapManager sub = FindObjectOfType<SubMapManager>();
            sub.Test_KillMonster();
        }
    }
}
