using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowNPC : MonoBehaviour
{
    public Transform preyNPC;
    public Vector3 initTransf;
    NavMeshAgent agent;
    public float maxDistance = 7f;

    // Start is called before the first frame update
    void Start()
    {
        initTransf = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       if(CanChase()){
           agent.destination = preyNPC.position;
       }else if(!CanChase()){
           agent.destination = initTransf;
       }
    }

    public bool CanChase(){
        if(Physics.Raycast(transform.position, (preyNPC.position-transform.position).normalized, maxDistance)){
            Debug.Log("Objetivo encontrado");
           return true;
       }else{
           Debug.Log("No hay objetivo");
           return false;
       }
    }

    public bool CanChase2(){
        if(Physics.Raycast(transform.position, (preyNPC.position-transform.position).normalized, maxDistance, -1, QueryTriggerInteraction.Ignore)){
            Debug.Log("Objetivo encontrado");
           return true;
       }else{
           Debug.Log("No hay objetivo");
           return false;
       }
    }
}
