using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private Vector3 SpawnPos;
    [SerializeField]
    public GameObject spawnObject;
    [SerializeField]
    private float newSpawnDuration = 0.1f;

    private Quaternion m_SpawnRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

    private void Start()
    {
        
    }

    // Because there is only one we can make it a singleton
    #region Singleton

    public static SpawnerBehaviour Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        SpawnPos = transform.position;
    }

    public void SpawnNewObject()
    {
        Instantiate(spawnObject, SpawnPos, m_SpawnRotation);
    }

    public void NewSpawnRequest()
    {
        Invoke("SpawnNewObject", newSpawnDuration);
    }
}
