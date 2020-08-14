using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D circle1;
    public Rigidbody2D circle2;

    public float MoveSpeed = 200f;

    void FixedUpdate()
    {
        float dir = Input.GetAxis("Horizontal");

        float torque = MoveSpeed * dir * Time.deltaTime * -1f;

        circle1.AddTorque(torque);
        circle2.AddTorque(torque);
    }
}
