using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : Collidable
{
    [SerializeField] KeyCode interactionKey;
    
    public event System.Action<Interactable> OnInteractedWith;

    private bool interacted;

    protected override void HandleOnCollide(Collider2D collider)
    {
        if (Input.anyKeyDown && !interacted)
        {
            HandleOnInteractedWith(collider.gameObject);
        } else if (!Input.GetKey(interactionKey))
        {
            interacted = false;
        }
    }    

    protected virtual void HandleOnInteractedWith(GameObject other)
    {
        Debug.Log(this.name + " interacted by " + other.name);
        OnInteractedWith?.Invoke(this);
    }

}
