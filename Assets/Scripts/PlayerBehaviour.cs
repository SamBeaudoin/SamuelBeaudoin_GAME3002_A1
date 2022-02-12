using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public Button m_RestartButton;

    private Rigidbody m_rb;

    public static bool m_IsPaused = false;

    public static bool m_DrawLine = false;

    bool m_VictorySoundPlayed = false;

    [SerializeField]
    private float m_MoveSpeed = 500.0f;

    [SerializeField]
    private GameObject m_Axe;

    [SerializeField]
    private float m_ThrowIncrement = -0.01f;

    public float m_ThrowStrength;

    [SerializeField]
    private Transform m_SpawnLocation;
    private Vector3 m_ThrowDirection = new Vector3(1.0f, 1.0f, 0.0f);

    private bool m_HasAxe = true;

    [SerializeField]
    public int m_Score = 0;

    [SerializeField]
    public Text m_PlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_RestartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            m_IsPaused = !m_IsPaused;

            if(m_IsPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (!m_IsPaused)
        {
            // Movement Controls
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(Vector3.back * m_MoveSpeed, ForceMode.Force);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(Vector3.forward * m_MoveSpeed, ForceMode.Force);
            }

            // Axe Wind-Up
            if (Input.GetKey(KeyCode.Space))
            {
                if (!(m_Axe.transform.rotation.x <= -0.5))
                {
                    m_Axe.transform.Rotate(m_ThrowIncrement, 0.0f, 0.0f, Space.Self);
                    m_ThrowStrength += 1.0f;
                }
            }
            // Axe Throw
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (m_HasAxe)
                {
                    Shoot();
                }
                m_HasAxe = false;
                m_Axe.SetActive(false);
            }

            // Reset Axe
            if (!m_HasAxe)
            {
                if (m_Axe.transform.rotation.x < -0.1227879f)
                {
                    m_Axe.transform.Rotate(-m_ThrowIncrement, 0.0f, 0.0f, Space.Self);
                }
                else
                {
                    m_HasAxe = true;
                    m_Axe.SetActive(true);
                    m_ThrowStrength = 0.0f;
                }
            }

            // Update Score Text
            m_PlayerScore.text = "Score: " + m_Score;

            // Draw Trajectory
            if (m_DrawLine)
            {
                DrawTrajectory.Instance.UpdateTrajectory(m_ThrowDirection * (m_ThrowStrength / 10.0f), m_rb, m_SpawnLocation.position);
            }

            if(m_Score == 10)
            {
                m_RestartButton.gameObject.SetActive(true);
                if (!m_VictorySoundPlayed)
                {
                    m_RestartButton.GetComponent<AudioSource>().Play();
                    m_VictorySoundPlayed = true;
                }
            }
        }
    }
    // Spawn Axe
    void Shoot()
    {
        SpawnerBehaviour.Instance.SpawnNewObject();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void DrawLines(bool state)
    {
        m_DrawLine = state;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
