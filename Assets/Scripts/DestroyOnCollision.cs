using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}