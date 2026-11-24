using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_platforms;
    [SerializeField] private GameObject m_player;
    [SerializeField] private Transform m_startintPoint;
    [SerializeField] private float m_spawnDist;
    private Transform m_lastPlatformSpawn;
    void Start()
    {
        m_lastPlatformSpawn = m_startintPoint;
    }

    void Update()
    {
        if (Vector3.Distance(m_player.transform.position, m_lastPlatformSpawn.position) < m_spawnDist)
        {
            Transform platformEndPoint = m_lastPlatformSpawn.Find("End");
            m_lastPlatformSpawn = SpawnPlatform(platformEndPoint.position);
        }
    }
private Transform SpawnPlatform(Vector3 spawnPosition)
    {
        GameObject newPlatform = Instantiate(m_platforms[Random.Range(0, m_platforms.Length)], spawnPosition, Quaternion.identity);
        return newPlatform.transform;

    }
}
