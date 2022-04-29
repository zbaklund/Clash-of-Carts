using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Health of gameobjects that have health.
public class Health : MonoBehaviour
{
    public float health = 100;
    public HealthBar healthBar;
    private string enemy_tag;
    private bool spawned = false;

    public void acceptDamage(float damage) {
        health -= damage;
        if (this.gameObject.tag == "Enemy") {
            healthBar.SetCurrHealth(health);
        }

        if (health <= 0) {
            if (this.gameObject.tag == "Enemy") {
                EventBus.Publish<EnemyDeathEvent>(new EnemyDeathEvent());
                Destroy(this.gameObject); // Change to signal death event.
            } else {
                EventBus.Publish<PlayerDeathEvent>(new PlayerDeathEvent());
            }
        } 
    }

    void Update() {
        if (enemy_tag == "Player") {
            // Enemy
            if (!spawned) {
                spawned = true;
                EventBus.Publish<EnemySpawnEvent>(new EnemySpawnEvent());
            }

        } else {
            // Player
        }
    }
    void Start() {
        if (this.gameObject.tag == "Enemy") {
            enemy_tag = "Player";
            healthBar.SetMaxHealth(health);
        } else {
            enemy_tag = "Enemy";
        }
    }

    
}
