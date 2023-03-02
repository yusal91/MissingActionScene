using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LedgeController : MonoBehaviour
{
    //public int id;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    GameManager.instance.onLedgeTriggerEnter += OnGrabTheLedge;
    //    GameManager.instance.OnClimbingFromTheLedge += OnClimbingUp;
    //}

    //private void OnGrabTheLedge(int id)
    //{
    //    if(id == this.id)
    //    {
    //         Vector3 handPos;
    //         GameManager.instance.playerMovement.enabled = false;
    //         LedgeGrab.instance._grabbedLedge = true;
    //         //transform.position = handPos;
    //         handPos = LedgeGrab.instance.GetHandPostion();

    //         Debug.Log("check id: " + handPos + id);

    //         GameManager.instance.playerMovement.rb.isKinematic = true;
    //    }

    //}

    //private void OnClimbingUp(int id)
    //{
    //    if (id == this.id)
    //    {
    //        Debug.Log("Climbing up");

    //        LedgeGrab.instance._grabbedLedge = false;
    //        transform.position = LedgeGrab.instance.GetStandUpPos();
    //        GameManager.instance.playerMovement.enabled = true;
    //        GameManager.instance.playerMovement.rb.isKinematic = false;
    //    }        
    //}

}
