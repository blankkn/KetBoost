using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    //anything above a method, will be a variable.
    Rigidbody rb;
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float mainRotation = 100f;
    [SerializeField] AudioClip mainEngine;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotation();
        
    }
    void Thrust() 
    
    {
        if (Input.GetKey(KeyCode.Space))
        
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
       else 
        {
            audioSource.Stop();
        }
    }
    void Rotation()
    //if else is present next to an if statement, it will check the code directly above it. 
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(mainRotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-mainRotation);
        }
    }

     void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation of constraints so we can rotate without any conflict in physics.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}