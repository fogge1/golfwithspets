using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmovement : MonoBehaviour
{

    public Rigidbody ball;

    [SerializeField] public float shootpower = 10f;
    [SerializeField] public float rotationspeed = 5f;
    public LineRenderer line;

    float yRot = 0f;
    float xRot = 0f;

    int Shots = 0;
   

    void Update()
    {
       
            CheckForShot();
            
    }



   
    void CheckForShot()
    {
       
        transform.position = ball.position;//on mouse press
        if (Input.GetMouseButton(0))
        {
            xRot += Input.GetAxis("Mouse X") * rotationspeed;
            yRot += Input.GetAxis("Mouse Y") * rotationspeed;
            if (yRot < -35f)
            {
                yRot = -35f;
            }
            transform.rotation = Quaternion.Euler(yRot, xRot, 0f);
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 4f);
        }
        if (Input.GetMouseButtonUp(0)) //on mouse release
        {
            ball.velocity = transform.forward * shootpower;
            line.gameObject.SetActive(false);
            Shots++;
            Debug.Log(Shots);
        }

        

    }




} 
