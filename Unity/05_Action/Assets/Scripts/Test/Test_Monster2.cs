using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Monster2 : MonoBehaviour
{
    private void Start()
    {
        GameManager.Inst.MainPlayer.Test();
    }
}
