using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IHealth
{
    float HP { get; set; } // HP를 확인하고
    float MaxHP { get; }    // 최대 HP를 확인할 수 있다.

    System.Action onHealthChage { get; set; }   // HP가 변경될때 실행되는 델리게이트
}
