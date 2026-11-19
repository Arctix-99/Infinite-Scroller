using UnityEngine;

public class ObstacleDeath : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
