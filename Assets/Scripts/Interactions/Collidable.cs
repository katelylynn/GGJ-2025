using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collidable : MonoBehaviour
{
    [SerializeField] private ContactFilter2D filter;
    private readonly List<Collider2D> collidedObjs = new();

    private Collider2D colliderObj;

    protected virtual void Start()
    {
        colliderObj = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        collidedObjs.Clear();
        colliderObj.OverlapCollider(filter, collidedObjs);
        foreach (var collider in collidedObjs) HandleOnCollide(collider);
    }

    public event Action<Collider2D> OnCollide;

    protected virtual void HandleOnCollide(Collider2D collider)
    {
        Debug.Log("Collided with " + collider.name);
        if (OnCollide == null)
            return;
        OnCollide.Invoke(collider);
    }
}