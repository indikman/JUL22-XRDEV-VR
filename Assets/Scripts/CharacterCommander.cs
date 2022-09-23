using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterCommander : MonoBehaviour
{
    public LayerMask mask;
    public InputAction action;
    public NavMeshAgent agent;
    public GameObject agentReticle;
    public Transform raycastOrigin;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        //action.performed += Action_performed;

        //action.started += Action_started;
    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        agent.destination = hit.point;
        Debug.Log(hit.point);
    }

    private void Update()
    {
        if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 100, mask))
        {
            agentReticle.transform.position = hit.point;
        }

        if(Input.GetButtonDown("XRI_Right_PrimaryButton"))
        {
            agent.destination = hit.point;
            Debug.Log(hit.point);
        }
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        agent.destination = hit.point;
        Debug.Log(hit.point);
    }
}
