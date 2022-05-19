using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowNPC : MonoBehaviour
{
    public Transform preyNPC;
    private Vector3 initTransf;
    NavMeshAgent agent;
    public float maxDistance = 7f;
    public Quaternion leftAngle;
    public Quaternion rightAngle;
    // Start is called before the first frame update
    void Start()
    {
        initTransf = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       if(CanChase3()){
           agent.destination = preyNPC.position;
       }else if(!CanChase3()){
           agent.destination = initTransf;
       }
    }

    public bool CanChase(){
        if(Physics.Raycast(transform.position, (preyNPC.position-transform.position).normalized, maxDistance)){
            Debug.Log("CanChase(): Objetivo encontrado");
           return true;
       }else{
           Debug.Log("CanChase(): No hay objetivo");
           return false;
       }
    }

    public bool CanChase2(){
                
        if(Physics.Raycast(transform.position+Vector3.up*0.1f, (preyNPC.position-transform.position).normalized,
        out RaycastHit hitInfo ,maxDistance, -1, QueryTriggerInteraction.Ignore)){
            Debug.Log("Objecto encontrado: "+hitInfo.collider.gameObject.name);
            Debug.Log("CanChase2(): Objetivo encontrado");
           if(hitInfo.collider.gameObject.tag == "NPC"){
               return true;
           }else{
               return false;
           }
        }else{
           Debug.Log("CanChase2(): No hay objetivo");
           return false;
       }
    }

    public bool CanChase3(){

        if(Physics.Raycast(transform.position+Vector3.up*0.1f, (preyNPC.position-transform.position).normalized,
        out RaycastHit hitInfo, maxDistance, -1, QueryTriggerInteraction.Ignore)){
            
            if(leftAngle.eulerAngles.magnitude <= hitInfo.collider.transform.eulerAngles.magnitude &&
             hitInfo.collider.transform.eulerAngles.magnitude >= leftAngle.eulerAngles.magnitude){

                if(hitInfo.collider.gameObject.tag == "NPC"){
                    return true;
                }else{
                    return false;
                }
            }else{ 
                return false;
                }
        }else{
            Debug.Log("CanChase2(): No hay objetivo");
            return false;
       }
    }
}
