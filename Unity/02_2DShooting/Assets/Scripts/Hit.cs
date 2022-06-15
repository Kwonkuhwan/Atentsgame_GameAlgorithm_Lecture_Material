using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private void Awake()
    {
        Animator ani = GetComponent<Animator>();

        // 애니메이션 클립정보 가져오기 GetCurrentAnimatorClipInfo
        Destroy(this.gameObject, ani.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
} 
