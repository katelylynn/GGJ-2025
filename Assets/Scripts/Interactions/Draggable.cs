using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maintainedDistance = 1f;
    [SerializeField] private float minVelocity = 0.1f;
    [SerializeField] private float dampingFactor = 0.9f; // Smooth damping for velocity
    [SerializeField] private Transform attachedTo;

    private Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attach(GameObject target)
    {
        attachedTo = target.transform;
    }

    public void Detach()
    {
        attachedTo = null;
    }

    void FixedUpdate()
    {
        if (attachedTo != null)
        {
            Vector2 toTarget = attachedTo.position - transform.position;
            float distance = toTarget.magnitude;

            if (distance > maintainedDistance)
            {
                Vector2 desiredVelocity = toTarget.normalized * acceleration;
                rb.velocity = Vector2.Lerp(rb.velocity, desiredVelocity, Time.fixedDeltaTime * dampingFactor);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.fixedDeltaTime * dampingFactor);
            }

            if (rb.velocity.magnitude < minVelocity)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
