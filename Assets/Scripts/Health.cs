using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script for handling Health of gameobjects that have health.
public class Health : MonoBehaviour
{
    public float health;
    public HealthBar healthBar;
    private string enemy_tag;
    private bool spawned = false;
    [SerializeField] private GameObject cart;

    public void acceptDamage(float damage) {
        health -= damage;
        if (this.gameObject.tag == "Enemy") {
            healthBar.SetCurrHealth(health);
        }

        if (health <= 0) {
            if (this.gameObject.tag == "Enemy") {
                EventBus.Publish<EnemyDeathEvent>(new EnemyDeathEvent());
                deathFunction();
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

    void deathFunction() {
        // Disable the "Damaging collider", set cart gameobject to "ragdoll"
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        this.gameObject.GetComponent<PlayerNavmesh>().enabled = false;
        cart.layer = 3; // 3 is ragdoll layer, will not collide with player or other alive players,
                        // but will still interact with the rest of the map
        
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 dir = Random.onUnitSphere;
        dir.y = 1;
        rb.AddForceAtPosition(dir * 100, transform.position +  new Vector3(1, 1, 1));
        Destroy(gameObject, 3.0f);
    }


}
