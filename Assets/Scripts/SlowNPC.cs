using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowNPC : MonoBehaviour
{
    public Transform preyNPC;
    private Vector3 initPos;
    NavMeshAgent agent;
    AgentState state;
    AgentType type;
    public float maxDistance = 7f;
    public Quaternion leftAngle;
    public Quaternion rightAngle;
    bool canChase = false;
    // Start is called before the first frame update
    void Start()
    {
        state = AgentState.Idle;
        type = AgentType.None;
        initPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       /*if(CanChase3()){
           agent.destination = preyNPC.position;
       }else if(!CanChase3()){
           agent.destination = initTransf;
       }*/
       //bool canChase = CanChase3();
       switch(type){
           case AgentType.Full360:
           canChase = CanChase();
           break;
           case AgentType.Block360:
           canChase = CanChase2();
           break;
           case AgentType.Range:
           canChase = CanChase3();
           break;
           case AgentType.None:
           canChase = false;
           break;
       }
       switch(state){
            case AgentState.Idle:
            if(canChase){
                state = AgentState.Chasing;
            }
            break;
            case AgentState.Chasing:
            if(canChase){
                agent.destination = preyNPC.position;
            }else{
                state = AgentState.Returning;
            }
            break;
            case AgentState.Returning:
            if(canChase){
                state = AgentState.Chasing;
            }else{
                agent.destination = initPos;
            }
            break;
        }
        Debug.Log("Estado actual: "+state.ToString());
        Debug.Log("Valor de canChase "+canChase.ToString());
        Debug.DrawRay(transform.position, transform.forward*10f, Color.red, 1f);
    }

    public bool CanChase(){
        if(Physics.Raycast(transform.position, (preyNPC.position-transform.position).normalized, maxDistance)){
            //Debug.Log("CanChase(): Objetivo encontrado");
           return true;
       }else{
           //Debug.Log("CanChase(): No hay objetivo");
           return false;
       }
    }

    public bool CanChase2(){
                
        if(Physics.Raycast(transform.position+Vector3.up*0.1f, (preyNPC.position-transform.position).normalized,
        out RaycastHit hitInfo ,maxDistance, -1, QueryTriggerInteraction.Ignore)){
            //Debug.Log("Objecto encontrado: "+hitInfo.collider.gameObject.name);
            //Debug.Log("CanChase2(): Objetivo encontrado");
           if(hitInfo.collider.gameObject.tag == "NPC"){
               return true;
           }else{
               return false;
           }
        }else{
           //Debug.Log("CanChase2(): No hay objetivo");
           return false;
       }
    }

    public bool CanChase3(){

        Vector3 targetDir = preyNPC.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        //Debug.Log("Ángulo respecto a target "+angle);
        if(Physics.Raycast(transform.position+Vector3.up*0.1f, (preyNPC.position-transform.position).normalized,
        out RaycastHit hitInfo, maxDistance, -1, QueryTriggerInteraction.Ignore)){

            if(angle<30f&&angle>-30f){
                if(hitInfo.collider.gameObject.tag == "NPC"){
                    return true;
                }else{
                    return false;
                }
            }else {
                return false;
            }
        }else{
            return false;
        }
    }
    void SetState(AgentState newState){
        if(state != newState){
            switch(newState){
                case AgentState.Idle:
                break;
                case AgentState.Chasing:
                break;
                case AgentState.Returning:
                transform.position = initPos;
                break;
            }
        }
    }
    public void ChangeFull360(){
        Debug.Log("Cambio a Full360");
        type = AgentType.Full360;
    }
    public void ChangeBlock360(){
        Debug.Log("Cambio a Block360");
        type = AgentType.Block360;
    }
    public void ChangeRange(){
        Debug.Log("Cambio a Range");
        type = AgentType.Range;
    }
    public void ChangeNone(){
        Debug.Log("Cambio a None");
        type = AgentType.None;
    }

    public enum AgentState{
        Idle,
        Chasing,
        Returning
    }
    public enum AgentType{
        None,
        Full360,
        Block360, 
        Range
    }
}
