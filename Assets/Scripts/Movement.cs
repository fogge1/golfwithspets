using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float speed;
    private float slowBrakeForce = -60;
    private float fastBrakeForce = -24;
    private float oldVelocity;
    private float oldVelocityY;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");

        // m_Rigidbody.AddForce(new Vector3(h * speed, 0, v * speed));
        // Debug.Log(m_Rigidbody.velocity);
        // Debug.Log(Vector3.Distance(new Vector3(0,0,0), m_Rigidbody.velocity));
        float velocity = Vector3.Distance(new Vector3(0,0,0), m_Rigidbody.velocity);

        m_Rigidbody.AddForce(m_Rigidbody.velocity * fastBrakeForce * Time.deltaTime); // add drag
        
        if (velocity < 10)
        {
            // Debug.Log(m_Rigidbody.velocity);
            m_Rigidbody.AddForce(m_Rigidbody.velocity * slowBrakeForce * Time.deltaTime);
            if (velocity < 0.4 && oldVelocity < 0.4 && m_Rigidbody.velocity.y < 0.005 && oldVelocityY < 0.005) // if velocity in all directions and velocity up and down is slow
            {
                m_Rigidbody.velocity = new Vector3(0, 0, 0);
            }
            oldVelocity = velocity;
            oldVelocityY = m_Rigidbody.velocity.y;
        }
    }
}
