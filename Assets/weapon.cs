using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;

    [SerializeField] Vector3 relativePosition = new Vector3(0.6f, 0.02f, 0.6f);
    [SerializeField] Quaternion bulletRotOffset = new Quaternion(-21.562f, -358.512f, 35.51f, 0f); 
    public Transform bullet;

    Vector3 bulletposition;
    Quaternion bulletrotation;


    void Update(){
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    private void Shoot(){
        RaycastHit hit; //What you hit
        float damage = 10f;
        //Create Bullet
        bulletposition = FPCamera.transform.TransformPoint(relativePosition);
        //bulletrotation = transform.rotation;
        //bullet.rotation = bullet.rotation * bulletRotOffset;
        Instantiate(bullet, bulletposition, transform.rotation);
        //Bullet Created

        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)){ //position direction whatyouhit range
            EnemyHealth targetDamaged = hit.transform.GetComponent<EnemyHealth>();
            if(targetDamaged == null) return; 
            targetDamaged.TakeDamage(damage);
            Debug.Log("I hit this thing: " + hit.transform.name);
            
        } 
        else{
            return;
        }
        
    }
}
