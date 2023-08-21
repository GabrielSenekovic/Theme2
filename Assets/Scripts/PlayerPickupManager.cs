using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    List<IPickupable> heldObjects = new List<IPickupable>();

    PlayerMovement playerMovement;

    [SerializeField] BoxCollider2D boxCollider;

    Vector2 boxColliderStartSize;
    Vector2 boxColliderStartOffset;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        boxColliderStartOffset = boxCollider.offset;
        boxColliderStartSize = boxCollider.size;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && heldObjects.Count != 0)
        {
            if(heldObjects[heldObjects.Count - 1].UseItem(playerMovement.GetDirecton()))
            {
                PushHeldObjects(-1);
                heldObjects.RemoveAt(heldObjects.Count - 1);
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IPickupable pickup) && other.transform.parent != transform)
        {
            if (playerMovement.bGrounded)
            {
                other.transform.parent = transform;
                other.transform.localPosition = new Vector3(0, 1, 0);
                heldObjects.Add(pickup);
                pickup.OnPickUp();
                PushHeldObjects(1);
            }
        }
    }
    void PushHeldObjects(int newHeight)
    {
        for(int i = 0; i < heldObjects.Count - 1; i++)
        {
            heldObjects[i].GameObject().transform.localPosition += new Vector3(0, newHeight, 0);
        }

        boxCollider.size = boxColliderStartSize + new Vector2(0, heldObjects.Count);
        boxCollider.offset = boxColliderStartOffset + new Vector2(0, (heldObjects.Count) / 2.0f);
    }
}
