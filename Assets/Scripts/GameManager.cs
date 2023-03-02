using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMovement playerMovement;
    public List<string> coinType;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        coinType= new List<string>();
    }

    //public event Action <int> onLedgeTriggerEnter;

    //public void LedgeTriggerEnter(int id)
    //{
    //    if(onLedgeTriggerEnter != null)
    //    {
    //        Debug.Log(onLedgeTriggerEnter);
    //        onLedgeTriggerEnter(id);
    //    }
    //}

    //public event Action<int> OnClimbingFromTheLedge;

    //public void ClimbFromTheLedge(int id)
    //{
    //    if (OnClimbingFromTheLedge != null)
    //    {
    //        Debug.Log(OnClimbingFromTheLedge);
    //        OnClimbingFromTheLedge(id);
    //    }
    //}
}
