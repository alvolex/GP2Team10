using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed = 5f;

    private const float ISOMETRIC_MOVEMENT_MULTIPLIER = 0.7071f; //Sin 45° angle of movement when going sideways

    private Camera cam;
    private Vector3 movementVector;
    private new Rigidbody rigidbody;
    private new BoxCollider collider;

    private int remainingJumps;

    void Start()
    {
        cam = Camera.main;
        collider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovementInput();
        HandlePlayerRotation();
    }

    private void HandleMovementInput()
    {
        //"Sideways" movement
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)        
        {
            movementVector = (Vector3.right * movementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * ISOMETRIC_MOVEMENT_MULTIPLIER) + (Vector3.forward * movementSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical")* ISOMETRIC_MOVEMENT_MULTIPLIER);
            rigidbody.MovePosition( transform.position += IsoVectorConvert(movementVector));
            
            //Leaving this for now because I don't know if RB movement will cause any problems
            //transform.position += IsoVectorConvert(movementVector); 
            return;
        }
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            movementVector = Vector3.right * movementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
            //transform.position += IsoVectorConvert(movementVector);
            rigidbody.MovePosition( transform.position += IsoVectorConvert(movementVector));
        }
        
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            movementVector = Vector3.forward * movementSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            //transform.position += IsoVectorConvert(movementVector);
            rigidbody.MovePosition( transform.position += IsoVectorConvert(movementVector));
        }
        
        /*if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }*/
    }

    private void HandlePlayerRotation()
    {
        Vector3 targetDirection = IsoVectorConvert(movementVector);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    Vector3 IsoVectorConvert(Vector3 vector)
    {
        Vector3 cameraRot = cam.transform.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(0, cameraRot.y, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
    
}
