using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMoveZ : MonoBehaviour
{
    Rigidbody m_rb;
    Vector3 m_MoveSpeed = new Vector3(0.0f, 0.0f, 5.0f);
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.velocity = m_MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_rb.transform.position.z < -20)
        {
            m_rb.velocity = m_MoveSpeed;
        }
        if (m_rb.transform.position.z > 20)
        {
            m_rb.velocity = -m_MoveSpeed;
        }
    }
}
