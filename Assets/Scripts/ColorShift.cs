using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShift : MonoBehaviour
{
    // Interpolate light color between two colors back and forth
    float duration = 1.0f;
    public Color color0;
    public Color color1;

    Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        // set light color
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);
    }
}
