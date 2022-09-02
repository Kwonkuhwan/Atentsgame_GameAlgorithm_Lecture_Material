using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MyVignette : MonoBehaviour
{
    public float vaignttevalue;

    private void Start()
    {
        Volume volume = GetComponent<Volume>();
        Vignette vignette;
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = vaignttevalue;
        }
    }
}
