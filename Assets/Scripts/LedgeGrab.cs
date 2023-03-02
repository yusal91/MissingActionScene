using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    public static LedgeGrab instance;
    [SerializeField] public int id;

    [SerializeField] public Transform _handPosition, _standPosition;

    [SerializeField] public float yOffSet = 6.5f;
    [HideInInspector]
     public Vector3 newHandPos;
    
    [SerializeField] public bool _grabbedLedge;

    void Start()
    {
        newHandPos = new Vector3(_handPosition.position.x,_handPosition.position.y - yOffSet, _handPosition.position.z);
        
        Debug.Log(newHandPos);
    }

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _grabbedLedge)
        {
            ClimbUpFromLedge(id);
            Debug.Log("Trying to Climb");
        }
    }

    public Vector3 GetHandPostion()
    {
        return _handPosition.position;
    }

    public Vector3 GetStandUpPos()
    {
        return _standPosition.position;
    }

    public void GrabLedge(Vector3 handPos, int id)
    {
        if (id == this.id)
        {
            GameManager.instance.playerMovement.enabled = false;
            _grabbedLedge = true;            
            handPos = GetHandPostion();

            Debug.Log("check id: " + handPos + id);

            GameManager.instance.playerMovement.rb.isKinematic = true;
        }
    }

    public void ClimbUpFromLedge(int id)
    {
        if (id == this.id)
        {
            _grabbedLedge = false;
            GameManager.instance.playerMovement.transform.position = GetStandUpPos();

            Debug.Log("check id: " + id);
            GameManager.instance.playerMovement.enabled = true;
            GameManager.instance.playerMovement.rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GrabLedge(newHandPos, id);
            Debug.Log("player collided with this trigger");
        }
    }
}
