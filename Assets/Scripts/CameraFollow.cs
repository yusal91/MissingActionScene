using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    [SerializeField] private Transform target;
    

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

    [Header("Camera Shake Settings")]
    public float shakeDuration = 0.15f;
    
    public AnimationCurve animCurve;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {   
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        SmoothFollow();

        // will be removed
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CameraShake(shakeDuration));            
        }
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }

    public IEnumerator CameraShake(float duration)   // to call this on an other script
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float strenght = animCurve.Evaluate(elapsed / duration);
            transform.position = originalPos + Random.insideUnitSphere * strenght;


            yield return null;
        }

        transform.position = originalPos + offset;
    }

    
}
