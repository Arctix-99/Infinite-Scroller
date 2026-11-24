using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleDeath : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehavior player = collision.gameObject.GetComponent<PlayerBehavior>();
        if (player != null)
        {
            player.OnDeath();

        }
    }
}

//{
//    Destroy(collision.gameObject);
//    SceneManager.LoadScene("DeathScene");

//}




