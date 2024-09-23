using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SnailBody : MonoBehaviour
{
    private Rigidbody2D rb;
    private float zRotation;

    public void SetupBody(float _yVelocity, float _zRotation)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(rb.velocity.x, _yVelocity);
        this.zRotation = _zRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, zRotation * Time.deltaTime);
    }
}
