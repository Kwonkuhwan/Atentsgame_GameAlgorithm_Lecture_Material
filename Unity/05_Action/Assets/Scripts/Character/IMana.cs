using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IMana
{
    float MP { get; set; }       // MP를 확인하고 설정할 수 있다.
    float MaxMP { get; }    // 최대 MP를 확인할 수 있다.

    System.Action onManaChage { get; set; }   // HP가 변경될때 실행되는 델리게이트
}
