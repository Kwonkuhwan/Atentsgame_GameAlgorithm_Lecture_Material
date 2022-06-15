using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform target = null;
    public Transform start = null;
    public float targetLength = 10.0f;

    public float spawnInterval = 1.0f;
    public GameObject EnemyPrefab = null;
    public float randomRange = 8.0f;
    public Color GizmosColor = Color.white;

    protected Vector3 startPosition = Vector3.zero;
    protected Vector3 endPosition = Vector3.zero;

    protected WaitForSeconds waitSecond = null;

    void Start()
    {
       waitSecond = new WaitForSeconds(spawnInterval);
       StartCoroutine(Spawn());
    }


    protected virtual IEnumerator Spawn()      // 코루틴 콜백 함수
    {
        while (true)
        {
            yield return waitSecond;
            GameObject obj = Instantiate(EnemyPrefab);
            obj.transform.position = transform.position;
            startPosition = Vector3.up * Random.Range(0.0f, randomRange);
            obj.transform.Translate(startPosition);
            startPosition = transform.position + startPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawLine(target.position, target.position + Vector3.up * targetLength);
        Gizmos.DrawLine(start.position, start.position + Vector3.up * targetLength);
    }
}
