using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;


    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log("Our health is : " + health);
    }
    void Start(){
        
    }
}
