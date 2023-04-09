using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2;
    public Transform interactionTransform;

    public Transform player;

    void Start()
    {
        Init();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with:" + transform.name);
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Init()
    {
        player = PlayerController.instance.GetComponent<Transform>();
    }
}
