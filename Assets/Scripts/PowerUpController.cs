using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public enum PowerUpTypes {
    speedBoost,
    invincible
}

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private Transform cart_transform;
    private bool is_rotating = true;
    // private void OnTriggerEnter(Collider other) {
    //     string powerUpName;
    //     if (other.gameObject.tag == "Player") {
    //         // TODO: something here to update player inventory
    //         // other.gameObject.GetComponent<XRPlayerController>().updateInventory(powerUpName)
    //         Destroy(this.gameObject);
    //     }
    // }

    void Update() {
       
    }
    public void onGrab() {
        Debug.Log("GRABBED Powerup");
        this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.transform.position = cart_transform.position + (Vector3.up * 0.5f);
        this.transform.rotation = Quaternion.Euler(90,0,0);
    }
    
}
