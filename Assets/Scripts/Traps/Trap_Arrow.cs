using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : Trap_Trampoline
{
    [Header("Additional Info")]
    [SerializeField] private bool canRotate;
    [SerializeField] private float cooldown;
    [SerializeField] private bool rotateRight;
    [SerializeField] private float rotateSpeed = 120f;
    private int rotateDir;

    [Space]
    [SerializeField] private float scaleUpSpeed = 10;
    [SerializeField] private Vector3 targetScale;

    private void Start()
    {
        transform.localScale = new Vector3(.3f, .3f, transform.localScale.z);
    }

    private void Update()
    {
        HandleScaleUp();

        if (!canRotate)
            return;

        HandleRotation();
    }

    private void HandleScaleUp()
    {
        if (transform.localScale.x < targetScale.x)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleUpSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        rotateDir = rotateRight ? -1 : 1;

        transform.Rotate(0, 0, rotateSpeed * rotateDir * Time.deltaTime);
    }

    private void DestroyMe()
    {
        GameObject arrowPrefab = GameManager.instance.arrowPrefab;
        GameManager.instance.CreateObject(arrowPrefab, transform, cooldown);

        Destroy(gameObject);
    }
}
