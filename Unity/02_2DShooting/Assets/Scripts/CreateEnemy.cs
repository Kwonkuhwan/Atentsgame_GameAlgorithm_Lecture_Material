using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public float spawnInterval = 1.0f;
    public GameObject EnemyPrefab = null;
    public float randomRange = 8.0f;

    WaitForSeconds waitSecond = null;

    void Start()
    {
        waitSecond = new WaitForSeconds(spawnInterval);
        Create();
    }

    void Create()
    {
       StartCoroutine(Spawn());
    }

    IEnumerator Spawn()      // 코루틴 콜백 함수
    {
        while (true)
        {
            yield return waitSecond;
            GameObject obj = Instantiate(EnemyPrefab);
            obj.transform.position = transform.position;
            obj.transform.Translate(Vector3.up* Random.Range(0.0f, randomRange));
        }
    }
}
