using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    private float mass = 3.0f;
    private Vector3 impact = Vector3.zero;
    private CharacterController controller;
    
    void Start(){
    controller = GetComponent<CharacterController>();
    }
    
    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force){
        Debug.Log("Add Impact");
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }
    
    void Update(){
        // apply the impact force:
        if (impact.magnitude > 0.2) controller.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
 }
}
