using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehiour : MonoBehaviour
{
    AudioSource m_Sfx;
    Rigidbody m_rb;
    GameObject m_Player;

    public bool m_IsFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Sfx = GetComponent<AudioSource>();
        m_rb = GetComponent<Rigidbody>();
        m_Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Level")
        {
            return;
        }
        else if(collision.collider.GetType() == typeof(BoxCollider))
        {
            m_Sfx.Play();
            Rigidbody m_Axe_rb = collision.gameObject.GetComponent<Rigidbody>();
            m_Axe_rb.useGravity = false;
            m_Axe_rb.isKinematic = true;
            AddConstraints(m_Axe_rb);
            if(!m_IsFrozen)
            {
                m_IsFrozen = true;
                RewardPoints();
            }
            return;
        }
        else if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            Rigidbody m_Axe_rb = collision.gameObject.GetComponent<Rigidbody>();
            m_Axe_rb.velocity *= -1.0f;
            m_Axe_rb.AddTorque(0.0f, 0.0f, 20.0f, ForceMode.Impulse);
        }
    }

    void AddConstraints(Rigidbody m_Axe)
    {
        m_Axe.constraints = RigidbodyConstraints.FreezeAll;
        m_rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void RewardPoints()
    {
        m_Player.GetComponent<PlayerBehaviour>().m_Score += 1;
    }
}
