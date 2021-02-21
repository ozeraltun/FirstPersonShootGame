using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    [SerializeField] Transform target; //target is the player
    [SerializeField] float beforeDetectRadius = 5f;
    [SerializeField] float afterDetectRadius = 12f;
    float detectRadius;
    float distanceToTarget = Mathf.Infinity;
    bool detected;
    NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectRadius = beforeDetectRadius;
        detected = false;
        detectionCheck();
    }

    // Update is called once per frame
    void Update()
    {
        detectionCheck();
    }
    void detectionCheck(){
        //Calculate distance between enemy and player
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if(distanceToTarget<detectRadius){
            navMeshAgent.SetDestination(target.position);
            detectRadius = afterDetectRadius;
            detected = true;    
        }
        else{
            detected = false;
            detectRadius = beforeDetectRadius;
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, beforeDetectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, afterDetectRadius);
    }
}
