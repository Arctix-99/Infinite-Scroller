using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHider : MonoBehaviour
{
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private InputActionAsset m_inputAction;
    private InputAction m_attack;
    private void Start()
    {
        m_attack = m_inputAction.FindAction("Attack");
    }
    public void HideObject()
    {
        PlayerBehavior player = GetComponent<PlayerBehavior>();
        if (m_attack.WasPressedThisFrame())
        {
            objectToHide.SetActive(true);

        }
    }
}
