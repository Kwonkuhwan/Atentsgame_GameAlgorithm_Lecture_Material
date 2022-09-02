using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target = null;
    public float speed = 3.0f;
    private Vector3 offset;

    private void Start()
    {
        if(target == null)
        {
            target = GameManager.Inst.MainPlayer.transform;
        }
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.fixedDeltaTime);
    }
}
