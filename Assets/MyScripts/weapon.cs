using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public Image sniper_crosshair;
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float range = 100f;

    [SerializeField] Vector3 relativePosition = new Vector3(0.6f, 0.02f, 0.6f);
    [SerializeField] Quaternion bulletRotOffset = new Quaternion(-21.562f, -358.512f, 35.51f, 0f); 

    public Camera myCamera;
    [SerializeField] float cameraFOVNormal = 60f;
    [SerializeField] float cameraFOVZoom = 70f;
    bool zoomVal = false;
    float lastRMouseTime, timeBetweenRMouseClick = 0.3f;

    public GameObject bullet;

    Vector3 bulletposition;
    Quaternion bulletrotation;

    [SerializeField] float bulletForce = 5f; 
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
        if(Input.GetMouseButtonDown(1)){ //RightClick
            Zoom();
        }
    }
    private void Zoom(){
        if(zoomVal){
                if(Time.time - lastRMouseTime > timeBetweenRMouseClick){
                    zoomVal = false;
                    myCamera.fieldOfView = cameraFOVNormal;
                    lastRMouseTime = Time.time;
                    sniper_crosshair.gameObject.SetActive(true);
                }
            }
            else{
                if(Time.time - lastRMouseTime > timeBetweenRMouseClick){
                    zoomVal = true;
                    myCamera.fieldOfView = cameraFOVZoom;
                    lastRMouseTime = Time.time;
                    sniper_crosshair.gameObject.SetActive(false);
                }    
            }
    }
    private void Shoot(){
        FlashMuzzle();
        Shell();
        EnemyHit();
        GetComponent<Animator>().SetTrigger("shootingTrigger");
    }
    private void FlashMuzzle(){
        muzzleFlash.Play();
    }
    private void Shell(){

        bulletposition = FPCamera.transform.TransformPoint(relativePosition);
        GameObject shellclone = Instantiate(bullet, bulletposition, transform.rotation);
        Rigidbody rb = shellclone.GetComponent<Rigidbody>();
        rb.AddForce(-transform.right * bulletForce);
        Destroy(shellclone, 3);
    }
    private void EnemyHit(){
        RaycastHit hit; //What you hit
        float damage = 10f;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)){ //position direction whatyouhit range
            ApplyHitEffect(hit);
            
            EnemyHealth targetDamaged = hit.transform.GetComponent<EnemyHealth>();
            if(targetDamaged == null) return; 
            targetDamaged.TakeDamage(damage);
            Debug.Log("I hit this thing: " + hit.transform.name);
        } 
        else{
            return;
        }
    }
    private void ApplyHitEffect(RaycastHit hit){
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.5f);
    }
}
