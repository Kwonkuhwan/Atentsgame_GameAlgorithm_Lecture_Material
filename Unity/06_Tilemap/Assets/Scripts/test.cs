using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = FindObjectOfType<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.black;
    }
}
