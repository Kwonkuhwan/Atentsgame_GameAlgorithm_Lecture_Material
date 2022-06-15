using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator ani = null;

    private void Awake()
    {
        ani = GetComponent<Animator>();

        // 애니메이션 클립정보 가져오기 GetCurrentAnimatorClipInfo
        Destroy(this.gameObject, ani.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    //private void Update()
    //{
    //    if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
    //        Destroy(this.gameObject);
    //}
}
