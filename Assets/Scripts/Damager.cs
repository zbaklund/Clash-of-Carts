using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    void OnTriggerEnter(Collider col) {
        GameObject other = col.gameObject;
        if (other.tag == "EnemyRegen") {
            // Do something
        } else if (other.tag == "Player") {
            Debug.Log("Hit player");
            other.GetComponent<Health>().acceptDamage(10);
        } else if (other.tag == "Enemy") {
            Debug.Log("Hit player");
            other.GetComponent<Health>().acceptDamage(10);
        }
    }

}
