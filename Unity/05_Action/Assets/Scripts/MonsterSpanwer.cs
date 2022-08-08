using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterSpanwer : MonoBehaviour
{
    public GameObject monster;
    public int maxSpawn = 2;
    public float spawnRange = 5.0f;

    public bool showSpawnRange = true;

    private void Update()
    {
        if(transform.childCount <= maxSpawn)
        {
            GameObject obj = Instantiate(monster, this.transform);
            Vector2 randPos = Random.insideUnitCircle * 5.0f;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            obj.GetComponent<Enemy>().patrolRoute = transform.GetChild(0);
        }
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRange)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, spawnRange);
        }
    }
}
