using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject pickupVfx;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetRandomFruitIfNeeded();
    }

    private void SetRandomFruitIfNeeded()
    {
        if (!GameManager.instance.FruitHaveRandomLook())
            return;

        int randomIndex = Random.Range(0, 8);
        anim.SetFloat("fruitIndex", randomIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Player player = collision.GetComponent<Player>();

       if(player != null)
       {
            GameManager.instance.AddFruit();
            Destroy(gameObject);

            GameObject newVfx = Instantiate(pickupVfx, transform.position, Quaternion.identity);
       }
    }
}
