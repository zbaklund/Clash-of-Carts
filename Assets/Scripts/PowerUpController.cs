using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpTypes {
    speedBoost,
    invincible
}

public class PowerUpController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        string powerUpName;
        if (other.gameObject.tag == "Player") {
            // TODO: something here to update player inventory
            // other.gameObject.GetComponent<XRPlayerController>().updateInventory(powerUpName)
            Destroy(this.gameObject);
        }
    }
    
}