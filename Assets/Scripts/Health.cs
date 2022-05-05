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
    public bool is_invincible = false;
    private Subscription<GrabbedInvincibilityEvent> inv_event;
    [SerializeField] private GameObject cart;

    public void acceptDamage(float damage) {
        if (!is_invincible) {
            health -= damage;
        } else {
            return;
        }

        if (this.gameObject.tag == "Enemy") {
            healthBar.SetCurrHealth(health);
        } else {
            EventBus.Publish<PlayerHitEvent>(new PlayerHitEvent());
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
        inv_event = EventBus.Subscribe<GrabbedInvincibilityEvent>(_setInvHelper);
        if (this.gameObject.tag == "Enemy") {
            enemy_tag = "Player";
            healthBar.SetMaxHealth(health);
        } else {
            enemy_tag = "Enemy";
        }
    }

    void _setInvHelper(GrabbedInvincibilityEvent e) {
        StartCoroutine(_setInv());
    }

    private IEnumerator _setInv() {
        is_invincible = true;
        yield return new WaitForSeconds(5f);
        is_invincible = false;
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
        rb.mass = 1f;
        Vector3 dir = Random.onUnitSphere;
        dir.y = 1;
        rb.AddForceAtPosition(dir * 100, transform.position +  new Vector3(1, 1, 1));
        Destroy(gameObject, 3.0f);
    }


}
