using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider2D fireCollider;

    [SerializeField] private Trap_FireButton fireButton;
    [SerializeField] private float offDuration;

    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fireCollider = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetFire(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchOffFire()
    {
        if (!isActive)
            return;

        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        SetFire(false);

        yield return new WaitForSeconds(offDuration);

        SetFire(true);
    }

    private void SetFire(bool active)
    {
        isActive = active;
        fireCollider.enabled = active;
        anim.SetBool("active", active);
    }

}
