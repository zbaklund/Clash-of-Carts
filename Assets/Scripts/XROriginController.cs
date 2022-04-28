using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginController : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;
    public float followspeed = 10;

    public void MoveToTarget(){
        Vector3 targetPos = objectToFollow.position +
                            objectToFollow.forward * offset.z +
                            objectToFollow.right * offset.x +
                            objectToFollow.up * offset.y;
        
        transform.position = Vector3.Lerp(transform.position, targetPos, followspeed * Time.deltaTime);
    }

    private void FixedUpdate(){
        MoveToTarget();
    }
}