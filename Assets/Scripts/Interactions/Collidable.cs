using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collidable : MonoBehaviour
{
    [SerializeField] private ContactFilter2D filter;

    public event System.Action<Collider2D> OnCollide;

    private Collider2D colliderObj;
    private List<Collider2D> collidedObjs = new List<Collider2D>();

    protected virtual void Start()
    {
        colliderObj = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        collidedObjs.Clear();
        colliderObj.OverlapCollider(filter, collidedObjs);
        foreach (Collider2D collider in collidedObjs)
        {
            HandleOnCollide(collider);
        }
    }

    protected virtual void HandleOnCollide(Collider2D collider)
    {
        Debug.Log("Collided with " + collider.name);
        OnCollide.Invoke(collider);
    }
}
