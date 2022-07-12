using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleSelect : StateMachineBehaviour
{
    private int waitTimes = 2;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("StateEnter - 애니메이션이 재생될 때마다 실행");
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("StateEnter - 애니메이션이 재생되고 있을 때 실행");
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //    Debug.Log("StateEnter - 애니메이션이 끝날 때 실행");
        waitTimes--;
        if (waitTimes < 0)
        {
            animator.SetInteger("IdleSelect", RandomSelect());
            waitTimes = Random.Range(1, 4);
        }
    }

    int RandomSelect()
    {
        float number = Random.Range(0.0f, 1.0f);
        int select = 0;

        if (number < 0.5f)
            select = 1;
        else if (number < 0.8f)
            select = 2;
        else if (number < 0.95f)
            select = 3;
        else
            select = 4;

        return select;
    }

    //void Test()
    //{
    //    int[] result = new int[4];
    //    for(int i=0; i< 1000000; i++)
    //    {
    //        result[RandomSelect()]++;
    //    }
    //    Debug.Log($"result : {result[0]}, {result[1]}, {result[2]}, {result[3]}");
    //}
}
