using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //......................................................Movement Variables.........................................................//
    [Header("Movement Variables")]
    [HideInInspector]    
    public Rigidbody rb;
    //public Vector3 movement;
    public float moveSpeed = 100f;
    public float movementForce = 200;

    //......................................................Wobble Rotations...........................................................//
    [Header("Wobble Rotations")]
    public float force = 1f;
    public float dampenForce = 1f;

    private float curAngel;
    private float startAngel;
    private Vector3 startAxis;
    private Vector3 curAxis;

    private float rotRadiance;
    //......................................................Float Variables ...........................................................// 
    [Header("Float Variables")]
    public float maxDampen = 1f;
   
    public float onGroundThreshold = 1f;
    public bool onGround;

    public float checkHight = 1f;
    public float targetHight = 1f;
    [SerializeField]
    private float groundDistance;

    public float stopMult = 10f;
    [Header("Jump Setting")]
    [SerializeField] bool justJumped = false;
    [SerializeField] bool landedAgain = true;
    [SerializeField] float jumpForce = 10;
    private float floatComp;

    

    private Intention intention;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation.ToAngleAxis(out startAngel, out startAxis);                   // start Angel, start Axis
        intention = GetComponent<Intention>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pMovement();
        wobblyPlayer();
        bounce();
        
        SettingJump();
    }

    void pMovement()
    {
        var xInput = intention.GetAxis("Horizontal");
        var vel = rb.velocity;

        // Check if direction change 
        bool directionChange = (xInput < 0 && rb.velocity.x > 0
                             || xInput > 0 && rb.velocity.x < 0);

        var velocityDampen = Mathf.Clamp(moveSpeed - Mathf.Abs(vel.x), 0.0f, moveSpeed);
        
        var V = velocityDampen / moveSpeed;
        var f = xInput * movementForce * V;
        

        if (directionChange)
        {
            f *= 2.0f;
        }
        else if(xInput == 0)
        {
            f = Mathf.Clamp(-vel.x * stopMult, -movementForce, movementForce);
        }

        rb.AddForce(new Vector3(f, 0.0f, 0.0f));
      
    }

    void SettingJump()
    {
        var yInput = intention.GetAxis("Vertical");

        if(yInput > 0 && !justJumped && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            justJumped= true;
        }

        if(justJumped && !onGround)
        {
            justJumped = false;
        }

        if(!justJumped && !landedAgain && onGround)
        {
            landedAgain = true;
        }
    }
  

    void bounce()
    {
        checkGroundDistance();
        onGround = groundDistance <= onGroundThreshold;

        if (onGround)
        {
            var th = targetHight - groundDistance;
            var distance = th;
            var f = distance * force - Mathf.Clamp(rb.velocity.y * dampenForce, -maxDampen, maxDampen);
            
            rb.AddForce(new Vector3(0, f, 0));
           
        }
    }

    void checkGroundDistance()
    {

        RaycastHit hit;
        var range = 100f;
        var hitSomthing = Physics.Raycast(transform.position, Vector3.down, out hit, range);
        Debug.DrawRay(transform.position, Vector3.down * range, Color.yellow);
        if (hitSomthing)
        {
            //Debug.Log("hit" + hit);
            groundDistance = hit.distance;
        }
        else
        {
            //Debug.Log("no hit" + hit);
            groundDistance = range;
        }
    }
    void wobblyPlayer()
    {
        var goalRotation = ShortestRotation(Quaternion.AngleAxis(startAngel, startAxis), transform.rotation);
        curAngel = 0f;                                         // Current angle
        curAxis = Vector3.zero;
        goalRotation.ToAngleAxis(out curAngel, out curAxis);

        rotRadiance = curAngel * Mathf.Deg2Rad;
        curAxis.Normalize();

        rb.AddTorque(rotRadiance * curAxis * force - (rb.angularVelocity * dampenForce));             // rotate to start angle from current angle
                                                                                                      //rotationForce          instead of force
    }

    public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Dot(a, b) < 0)
        {
            return a * Quaternion.Inverse(Multiply(b, -1));
        }
        else
        {
            return a * Quaternion.Inverse(b);
        }
    }

    public static Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
}
