using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private GameObject objRef;

    private enum Direction 
    {
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT,
        NONE
    }

    private Direction currentDir = Direction.NONE;
    private bool isForce = false;

    Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        this.InputListen();
        this.Move();
    }

    private void InputListen()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            this.currentDir = Direction.FORWARD;
        }
        
        else if(Input.GetKeyDown(KeyCode.S))
        {
            this.currentDir = Direction.BACKWARD;
        }

        else if(Input.GetKeyDown(KeyCode.A))
        {
            this.currentDir = Direction.LEFT;
        }

        else if(Input.GetKeyDown(KeyCode.D)) 
        {
            this.currentDir = Direction.RIGHT;
        }

        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            this.currentDir = Direction.NONE;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Holding Key.");
            this.isForce = true;
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Releasing Key.");
            this.isForce = false;
        }
    }

    private void Move()
    {
        if(this.currentDir == Direction.FORWARD) 
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * this.speed);
        }

        else if(this.currentDir == Direction.BACKWARD) 
        {
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * this.speed);
        }

        else if(this.currentDir == Direction.LEFT) 
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * this.speed);
        }

        else if(this.currentDir == Direction.RIGHT) 
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * this.speed);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(this.isForce == true)
        {
            if(collider.gameObject.tag == "Objects")
            {
                rb = collider.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(0, 20f, 0, ForceMode.Force);
            }
        }
    }
}
