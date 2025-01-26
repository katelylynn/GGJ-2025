using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(Collidable))]
public class ResourceNode : MonoBehaviour
{
    private Draggable draggable;
    private Collidable collidable;

    void Start()
    {
        draggable = GetComponent<Draggable>();
        collidable = GetComponent<Collidable>();

        collidable.OnCollide += HandleOnCollide;
    }

    private void HandleOnCollide(Collider2D collider)
    {
        Debug.Log("Resource Node collided with " + collider.name);

        HandleDrag hd = collider.GetComponent<HandleDrag>();
        if (hd != null) {
            if (hd.isDragging) {
                draggable.Attach(collider.gameObject);
            }
        } else {
        } 

    }
}
