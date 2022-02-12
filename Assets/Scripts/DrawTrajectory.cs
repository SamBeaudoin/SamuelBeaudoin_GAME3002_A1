using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_LineRenderer;

    [SerializeField]
    [Range(5, 30)]
    private int m_LineSegmentCount = 20;

    private List<Vector3> m_linePoints = new List<Vector3>();

    // Is a Singleton as we only ever need one line
    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTrajectory(Vector3 velocityVector, Rigidbody rb, Vector3 startPoint)
    {
        // T range = 2 * Velocity.Y / Gravity
        float FlightDuration = (2 * velocityVector.y) / Physics.gravity.y;

        // getting timeStep from total duration / number of segments
        float TimeStep = FlightDuration / m_LineSegmentCount;

        // Clear Previous Line
        m_linePoints.Clear();

        for (int i = 0; i < m_LineSegmentCount; i++)
        {
            float StepTimePassed = TimeStep * i;

            // Apply Motion calculation
            Vector3 MovementVector = new Vector3(
                velocityVector.x * StepTimePassed,

                // Y position = Y Velocity * Time - ( 1/2 Gravity * T^2 )
                velocityVector.y * StepTimePassed - 0.5f * Physics.gravity.y * StepTimePassed * StepTimePassed,
                velocityVector.z * StepTimePassed
            );

            m_linePoints.Add(-MovementVector + startPoint);
        }

        m_LineRenderer.positionCount = m_linePoints.Count;
        m_LineRenderer.SetPositions(m_linePoints.ToArray());
        
    }
}
