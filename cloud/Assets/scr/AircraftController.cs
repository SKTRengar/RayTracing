using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    public float rotationSpeed = 20f; 
    public float acceleration = 10f;   
    private float currentSpeed = 0f;   
    float rotationX = 0f;
    float rotationY = 0f;

    // Update is called once per frame
    void Update()
    {
        changeRotate();
        changeSpeed();
        transform.Rotate(rotationX * rotationSpeed * Time.deltaTime, rotationY * rotationSpeed * Time.deltaTime, 0f);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime*8);
    }

    void changeRotate()
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            rotationX = -1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            rotationX = 1f;
        }
        else{
            rotationX = 0f; 
        }

        if (Input.GetKey(KeyCode.LeftArrow)){
            rotationY = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
            rotationY = 1f;
        }
        else{
            rotationY = 0f; 
        }
    }

    void changeSpeed()
    {
        if (Input.GetKey(KeyCode.W)){
            currentSpeed += acceleration * Time.deltaTime*10; 
        }
        else if (Input.GetKey(KeyCode.S)){
            currentSpeed -= acceleration * Time.deltaTime*10; 
        }
    }
}
