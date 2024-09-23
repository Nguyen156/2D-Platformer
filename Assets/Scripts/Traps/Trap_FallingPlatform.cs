using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_FallingPlatform : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D[] colliders;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float travelDistance;
    private Vector3[] wayPoints;
    private int wayPointIndex;
    private bool canMove = false;

    [Header("Platfrom Fall details")]
    [SerializeField] private float impactSpeed = 3f;
    [SerializeField] private float impactDuration = .1f;
    private float impactTimer;
    private bool impactHappend;
    [Space]
    [SerializeField] private float fallDelay = .5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<BoxCollider2D>();
    }

    private IEnumerator Start()
    {
        SetupWaypoints();

        float randomDelay = Random.Range(0f, .6f);

        yield return new WaitForSeconds(randomDelay);

        canMove = true;

    }

    private void Update()
    {
        HandleImpact();
        HandleMovement();
    }

    private void SetupWaypoints()
    {
        wayPoints = new Vector3[2];

        float yOffset = travelDistance / 2;

        wayPoints[0] = transform.position + new Vector3(0, yOffset, 0);
        wayPoints[1] = transform.position + new Vector3(0, -yOffset, 0);
    }

    private void HandleMovement()
    {
        if(!canMove)
            return;                                      

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex], moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex]) < .1f)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Length)
                wayPointIndex = 0;
        }
    }

    private void HandleImpact()
    {
        if (impactTimer < 0)
            return;

        impactTimer -= Time.deltaTime;
        transform.position = 
            Vector2.MoveTowards(transform.position, transform.position + (Vector3.down * 10), impactSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(impactHappend)
            return;

        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Invoke(nameof(Falling), fallDelay);
            impactTimer = impactDuration;
            impactHappend = true;
        }

    }

    private void Falling()
    {
        anim.SetTrigger("deactive");
        canMove = false;
        rb.isKinematic = false;

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

    }
}
