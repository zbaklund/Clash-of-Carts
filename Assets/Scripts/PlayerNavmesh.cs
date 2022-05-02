using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavmesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 spawn_position;
    public Transform player_transform;
    private string target;
    // Start is called before the first frame update
    
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spawn_position = transform.position;
        player_transform = GameObject.FindGameObjectsWithTag("Player")[0].transform.Find("PlayerCart").transform;
        target = "Player";
    }

    public void setTarget(string tar) {
        target = tar;
    }

    // Update is called once per frame
    void Update()
    {   
        if (target == "Player") {
            navMeshAgent.destination = player_transform.position;
        } else {
            navMeshAgent.destination = spawn_position;
        }
    }
}
