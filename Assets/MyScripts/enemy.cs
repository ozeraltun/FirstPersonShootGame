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
    bool move;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectRadius = beforeDetectRadius;
        detected = false;
        move = false;
        GetComponent<Animator>().SetBool("move", false);
        detectionCheck();
    }

    // Update is called once per frame
    void Update()
    {
        detectionCheck();
        movementCheck();
    }
    void movementCheck(){
        if(move == true){
            //Invoke("MovementStart", 3.0f);
            MovementStart();
        }
        else{
            //Invoke("MovementStop", 3.0f);
            MovementStop();
        }
    }
    void detectionCheck(){
        //Calculate distance between enemy and player
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if(distanceToTarget<detectRadius){
            detected = true;
            move = true;
            detectRadius = afterDetectRadius;
        }
        else{
            detected = false;
            detectRadius = beforeDetectRadius;
            move = false;
        }
    }
    void MovementStart(){
        navMeshAgent.SetDestination(target.position);
        GetComponent<Animator>().SetBool("move", move);
    }
    void MovementStop(){
        navMeshAgent.SetDestination(transform.position);
		GetComponent<Animator>().SetBool("move", move);
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, beforeDetectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, afterDetectRadius);
    }
}
