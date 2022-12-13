using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public float turnSpeed = 4.0f;

    public GameObject target;
    private float targetDistance;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    private float rotX;

    public Rigidbody m_Rigidbody; // move Sphere
    public float force;
    private Vector3 vector;

    void Start ()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }

    void Update ()
    {
        // get the mouse inputs 
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // clamp the vertical rotation 
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);

        // move the camera position
        transform.position = target.transform.position - (transform.forward * targetDistance);


        // Debug.Log(transform.eulerAngles.y);

        // move sphere
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        vector = new Vector3(h * force, 0, v * force);

        vector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * vector;

        m_Rigidbody.AddForce(vector);


        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed left click.");
        }

        if (Input.GetMouseButton(0) == false)
        {
            Debug.Log("Left click release");
        }
    }
}