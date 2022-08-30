using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    public bool Grounded { get; private set; }

    public Vector3 ContactPosition { get; private set; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Grounded = true;
        ContactPosition = other.GetContact(0).point;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Grounded = true;
        ContactPosition = other.GetContact(0).point;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Grounded = false;
    }
}
