using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAsteroid : MonoBehaviour
{
    public float spawnInterval = 1.0f;
    public GameObject AsteroidPrefab = null;
    public float UprandomRange = 8.0f;
    public float RightrandomRange = 8.0f;

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
            GameObject obj = Instantiate(AsteroidPrefab);
            obj.transform.position = transform.position;
            obj.transform.Translate(Vector3.up * Random.Range(-UprandomRange, UprandomRange));
            obj.transform.Translate(Vector3.right * Random.Range(-RightrandomRange, RightrandomRange));
        }
    }
}
