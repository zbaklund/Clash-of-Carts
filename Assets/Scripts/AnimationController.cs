using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationController : MonoBehaviour
{
    public GameObject cart;

    private void FixedUpdate(){
        Vector3 vel = cart.GetComponent<Rigidbody>().velocity;
        float totalVelocity = vel.x + vel.z;

        // animate based on total velocity
    }
}