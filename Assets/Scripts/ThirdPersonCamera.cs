using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    public float turnSpeed = 4.0f;

    public GameObject target;
    private float targetDistance;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    private float rotX;
    private float oldRotX;

    public Rigidbody m_Rigidbody; // move Sphere
    public float force;
    private Vector3 vector;
    public TMP_Text clockText;
    public int hits = 0;
    public TMP_Text fpsText;
    [SerializeField] GameObject mouseImg;
    int timeToShowImg = 3000;
    RectTransform mousePos;
    RectTransform startPos;
    int shownTime = 200;
    

    void Start ()
    {
        targetDistance = 2f; //Vector3.Distance(transform.position, target.transform.position);
        mouseImg.SetActive(false);
        
        
        mousePos = mouseImg.GetComponent<RectTransform>();
        startPos = mousePos;
    }

    void Update ()
    {   
        if (menucontroller.paused) {
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked; // lock mouse in center of screen

            timeToShowImg--;
            if (timeToShowImg < 0) {
                mouseImg.SetActive(true);
                Debug.Log("test");
                shownTime--;
                mousePos.anchoredPosition -= new Vector2(0, 1f);
                if (shownTime < 0) {
                    shownTime = 200;
                    mousePos.anchoredPosition = new Vector2(0f, 0f);
                }
            }

            // get the mouse inputs 
            float y = Input.GetAxis("Mouse X") * turnSpeed;
            rotX += Input.GetAxis("Mouse Y") * turnSpeed;

            float velocity = Vector3.Distance(new Vector3(0,0,0), m_Rigidbody.velocity);
            // Debug.Log(velocity);

            if (Input.GetMouseButtonDown(0) && velocity == 0) // only run once, save camera position after hit
            {
                timeToShowImg = 3000;
                mouseImg.SetActive(false);
                oldRotX = rotX;
                //Debug.Log("Pressed left click.");
            }
            if (Input.GetMouseButtonUp(0) && velocity == 0) // only run once, GetMouseButtonUp / GetMouseButton
            {
                vector = new Vector3(0, 0, Mathf.Abs(oldRotX - rotX) * force); // calculate force from mouse movement

                vector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * vector; // translate force from camera rotation 

                m_Rigidbody.AddForce(vector);

                rotX = oldRotX; // return mouse to previous position
                //Debug.Log("Released left click.");

                hits += 1; // add one hit to counter 
                clockText.text = "hits: " + hits.ToString();  // show hits on screen
            }

            if (Input.GetMouseButton(0) == false || velocity != 0) // only overide mouse movement when hit is possible
            {
                targetDistance += Input.GetAxis("Mouse ScrollWheel") * -8; // move camera distance with scroll
                targetDistance = Mathf.Clamp(targetDistance, 1, 70);
                // Debug.Log(targetDistance);

                // clamp the vertical rotation 
                rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
            
                // rotate the camera
                transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);

                // move the camera position
                transform.position = target.transform.position - (transform.forward * targetDistance);
            }
            //fpsText.text = (1/Time.deltaTime).ToString();

            if (Input.GetKeyDown("r"))
            {
                print("r key was pressed");
                m_Rigidbody.transform.position = new Vector3(0, 0, 0);
                m_Rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        // clockText.text = DateTime.Now.ToString();       

        // Debug.Log(transform.eulerAngles.y);

        // move sphere
        // float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");

        // vector = new Vector3(h * force, 0, v * force);

        // vector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * vector;

        // m_Rigidbody.AddForce(vector);


        // if (Input.GetMouseButton(0))
        // {
        //     Debug.Log("Pressed left click.");
        // }

        // if (Input.GetMouseButton(0) == false)
        // {
        //     Debug.Log("Left click release");
        // }
    }
}