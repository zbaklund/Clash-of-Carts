using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    private bool is_enemy;
    private PlayerNavmesh nav_script;

    void Start() {
        if (this.gameObject.tag == "Enemy") {
            is_enemy = true;
            nav_script = this.gameObject.GetComponent<PlayerNavmesh>();
        } else {
            is_enemy = false;
        }
    }

    void OnTriggerEnter(Collider col) {
        GameObject other = col.gameObject;
        if (is_enemy) {
            if (other.tag == "EnemyRegen") {
                nav_script.setTarget("Player");
            } else if (other.name == "PlayerCart") {
                Debug.Log("Hit player");
                other.transform.parent.GetComponent<Health>().acceptDamage(10);
                nav_script.setTarget("EnemyRegen");
            }
        } else {
            if (other.name == "EnemyCart") {
                Debug.Log("Hit enemy");
                other.transform.parent.GetComponent<Health>().acceptDamage(10);
            }
        }
    }

}
