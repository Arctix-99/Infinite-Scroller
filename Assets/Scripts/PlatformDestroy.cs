using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Destroy(collision.gameObject);
        }
    }
}
