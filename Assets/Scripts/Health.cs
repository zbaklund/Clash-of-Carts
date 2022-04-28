using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Health of gameobjects that have health.
public class Health : MonoBehaviour
{
    public float health = 100;
    public HealthBar healthBar;
    private string enemy_tag;

    void OnCollisionEnter(Collision col) {
        GameObject other = col.gameObject;
        // First check if we are the one "attacking"
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f)) {
            Debug.Log("hit " + hit.collider.gameObject.name);
            if (other.tag == enemy_tag) {
                other.GetComponent<Health>().acceptDamage(10);
            }
        }
    }

    public void acceptDamage(float damage) {
        health -= damage;
        healthBar.SetCurrHealth(health);
        if (health <= 0) {
            Destroy(this.gameObject); // Change to signal death event.
        } 
    }

    void Start() {
        if (this.gameObject.tag == "Enemy") {
            enemy_tag = "Player";
        } else {
            enemy_tag = "Enemy";
        }
        healthBar.SetMaxHealth(health);
    }


}
