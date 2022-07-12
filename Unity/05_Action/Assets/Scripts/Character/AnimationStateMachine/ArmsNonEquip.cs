using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsNonEquip : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Inst.MainPlayer.ShowWeapons(false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Inst.MainPlayer.ShowWeapons(true);
    }
}
