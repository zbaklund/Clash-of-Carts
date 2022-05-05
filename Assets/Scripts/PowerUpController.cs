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
    private bool is_grabbed;
    [SerializeField] private Transform cart_transform;
    // private void OnTriggerEnter(Collider other) {
    //     string powerUpName;
    //     if (other.gameObject.tag == "Player") {
    //         // TODO: something here to update player inventory
    //         // other.gameObject.GetComponent<XRPlayerController>().updateInventory(powerUpName)
    //         Destroy(this.gameObject);
    //     }
    // }

    void Update() {
        if (is_grabbed) {
            this.transform.position = cart_transform.position + (cart_transform.forward * -0.5f);
        }
    }
    public void onGrab() {
        Debug.Log("GRABBED Powerup");
        this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.transform.position = cart_transform.position + (Vector3.forward * -0.5f);
        this.transform.rotation = Quaternion.Euler(90,0,0);
        EventBus.Publish<GrabbedInvincibilityEvent>(new GrabbedInvincibilityEvent());
        is_grabbed = true;
        Destroy(gameObject, 5.0f);
    }
}
