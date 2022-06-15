using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public float lifeTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelay());     // 코루틴 시작
    }

    IEnumerator DestroyDelay()      // 코루틴 콜백 함수
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

}
