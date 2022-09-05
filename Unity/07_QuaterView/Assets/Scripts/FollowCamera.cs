using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetPosition;

    public float cameraSpeed = 3.0f;

    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        targetPosition = FindObjectOfType<Tank>()?.transform;

        offset = transform.position - targetPosition.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition.position + offset,
            cameraSpeed * Time.deltaTime);
    }
}
