using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Snail : Enemy
{
    [Header("Snail Details")]
    [SerializeField] private Enemy_SnailBody bodyPrefab;
    [SerializeField] private float maxSpeed = 10;
    private bool hasBody = true;

    protected override void Start()
    {
        base.Start();
        canMove = true;
    }

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        HandleMovement();

        if (isGrounded)
            HandleTurnAround();
    }

    public override void Die()
    {
        if (hasBody)
        {
            hasBody = false;
            canMove = false;
            anim.SetTrigger("hit");

            rb.velocity = Vector2.zero;
            idleDuration = 0;
        }
        else if(!hasBody && !canMove)
        {
            anim.SetTrigger("hit");
            canMove = true;
            moveSpeed = maxSpeed;
        }
        else
        {
            base.Die();
        }
    }

    private void HandleTurnAround()
    {
        bool canFlipFromLedge = !isGroundInfrontDetected && hasBody; 
        if (canFlipFromLedge || isWallDetected)
        {
            if(!hasBody)
                anim.SetTrigger("wallHit");

            Flip();
            idleTimer = idleDuration;
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (idleTimer > 0 || !canMove)
            return;

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }

    private void CreateBody()
    {
        Enemy_SnailBody newBody = Instantiate(bodyPrefab, transform.position, Quaternion.identity);

        if (facingRight)
            newBody.transform.Rotate(0, 180, 0);

        if (Random.Range(0, 100) < 50)
            deathRotationDir *= -1;

        newBody.SetupBody(deathImpact, deathRotationSpeed * deathRotationDir);

        Destroy(newBody.gameObject, 10f);
    }
}
