using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallClimbing : MonoBehaviour
{
    private Intention intention;
    [SerializeField] bool climingWall;
    [SerializeField] Rigidbody rb;
    [SerializeField] float climbSpeed = 0.5f ;


    private void Start()
    {
       rb= GetComponent<Rigidbody>();
       intention= GetComponent<Intention>();
    }

    private void Update()
    {
        WallClimbingInput();
    } 
    
    void WallClimbingInput()
    {  
        if(climingWall && intention != null)           
        {
            var vi = intention.GetAxis("Vertical");
            rb.velocity = transform.up * vi * climbSpeed;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Wall")
        {
            rb.useGravity = false;
            GameManager.instance.playerMovement.enabled = false;
            climingWall = true;

            Debug.Log("Starting Wall Climbing: " + Col.gameObject.tag == "Wall");
        }

        if (Col.CompareTag("Collectable"))
        {            
            string coinType = Col.gameObject.GetComponent<Coin>().coinType;
            print("Coin Collected a:" + coinType);
            ScoreManager.instance.AddCoins(coinType);

            GameManager.instance.coinType.Add(coinType);
            Destroy(Col.gameObject);

            StartCoroutine(CameraFollow.instance.CameraShake(CameraFollow.instance.shakeDuration));
        }
    }

    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "Wall")
        {
            rb.useGravity = true;
            GameManager.instance.playerMovement.enabled = true;
            climingWall = false;

            Debug.Log("leaving Wall Climbing");
        }
    }   
}
