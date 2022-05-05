using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMovement : MonoBehaviour
{

    public Transform target;
    public float offset = 10;
    void Update () {
        target.position = transform.position + transform.forward * offset;
        target.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
    }

}
