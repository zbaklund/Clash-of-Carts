using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Health of gameobjects that have health.
public class Health : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
    void OnCollisionEnter(Collision col) {
        if (true) { // Need to modify to be only happen on collision with player.
            health -= 10;
            if (health == 0) {
                Destroy(this.gameObject); // Change to signal death event.
            } 
        }
    }


}
