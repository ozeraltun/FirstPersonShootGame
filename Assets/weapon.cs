using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;

    void Update(){
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    private void Shoot(){
        RaycastHit hit; //What you hit
        Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range); //position direction whatyouhit range
        Debug.Log("I hit this thing: " + hit.transform.name);
    }
}
