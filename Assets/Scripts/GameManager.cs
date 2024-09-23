using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;
    public Player player;

    [Header("Fruits Management")]
    public bool fruitHaveRandomLook;
    public int fruitsCollected;
    public int totalFruits;

    [Header("Checkpoint")]
    public bool canReActivate;

    [Header("Traps")]
    public GameObject arrowPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        CollectFruitsInfo();
    }

    private void CollectFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsOfType<Fruit>();
        totalFruits = allFruits.Length;
    }

    public void RespawnPlayer() => StartCoroutine(RespawnCoroutine());

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
    }

    public void UpdateRespawnPosition(Transform newRespawnPoint) => respawnPoint = newRespawnPoint;

    public void AddFruit() => fruitsCollected++;

    public bool FruitHaveRandomLook() => fruitHaveRandomLook;


    public void CreateObject(GameObject prefabs, Transform target, float delay = 0)
    {
        StartCoroutine(CreateObjectCoroutine(prefabs, target, delay));
    }

    private IEnumerator CreateObjectCoroutine(GameObject prefabs, Transform target, float delay)
    {
        Vector3 newPos = target.position;

        yield return new WaitForSeconds(delay);

        GameObject newObject = Instantiate(prefabs, newPos, Quaternion.identity);
    }
}
