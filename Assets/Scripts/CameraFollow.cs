using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_playerToFollow;
    [SerializeField] private Vector3 m_offSet;
    void Update()
    {
        transform.position = new Vector3(m_playerToFollow.position.x, m_playerToFollow.position.y, m_playerToFollow.position.z) + m_offSet;
    }
}
