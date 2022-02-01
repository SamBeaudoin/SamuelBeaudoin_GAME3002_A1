using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private Vector3 m_Direction = new Vector3(1.0f, 1.0f, 0.0f);
    [SerializeField]
    private float m_ShotStrength;
    [SerializeField]
    private Rigidbody m_rb;


    private Vector3 m_SpinDirection = new Vector3(0.0f, 0.0f, -20.0f);


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_Player = GameObject.FindWithTag("Player");
        m_ShotStrength = m_Player.GetComponent<PlayerBehaviour>().m_ThrowStrength / 10.0f;
        m_rb.AddForce(m_Direction * m_ShotStrength, ForceMode.VelocityChange);
        m_rb.AddTorque(m_SpinDirection, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
