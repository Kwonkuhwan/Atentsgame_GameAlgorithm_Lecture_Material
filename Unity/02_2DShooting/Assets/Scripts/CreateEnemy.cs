using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public float CreateTime = 5.0f;
    public GameObject EnemyPrefab = null;

    private bool b_enemy = false;

    void Update()
    {
        Create();
    }

    void Create()
    {
        if (!b_enemy)
        {
            Debug.Log("Create");
            transform.position = new Vector3(8, Random.Range(-4.0f, 3.0f), 0);
            StartCoroutine(CreateDelay());
        }
    }

    IEnumerator CreateDelay()      // 코루틴 콜백 함수
    {
        b_enemy = true;

        yield return new WaitForSeconds(CreateTime);
        GameObject obj = Instantiate(EnemyPrefab);
        obj.transform.position = transform.position;

        yield return new WaitForSeconds(1.0f);

        b_enemy = false;
    }
}
