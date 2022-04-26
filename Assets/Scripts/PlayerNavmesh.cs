using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavmesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target.position;
    }
}
