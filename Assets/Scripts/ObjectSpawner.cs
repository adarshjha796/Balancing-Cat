using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

/// <summary>
/// This script is aused to spawn the collectibles.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    private Transform boxes;

    [SerializeField]
    private float spawnRate = 5f;

    /// <summary>
    /// Array of different sprites for the items.
    /// </summary>
    [SerializeField]
    private Sprite[] itemSprites;

    /// <summary>
    /// Array of different prafabs.
    /// </summary>
    public GameObject[] boxPrefab;

    private void Awake()
    {
        boxes = GameObject.Find("Boxes").transform;
    }

    private void Start()
    {
        GamePlayController.instance.isGameOver = true;
        InvokeRepeating("SpawnObject", 0.05f, spawnRate);
    }

    /// <summary>
    /// Spawn random objects from the object pool.
    /// </summary>
    public void SpawnObject()
    {
        if (GamePlayController.instance.isGameOver) return;

        //GameObject box_obj = Instantiate(boxPrefab[Random.Range(0, 2)]);
        GameObject box_obj = LeanPool.Spawn(boxPrefab[Random.Range(0, 2)], boxes);

        // Setting a random sprite for the instantiated box.
        box_obj.GetComponent<SpriteRenderer>().sprite = itemSprites[Random.Range(0, itemSprites.Length)];

        // Resetting the box collider.
        Destroy(box_obj.GetComponent<Collider2D>());
        box_obj.AddComponent<BoxCollider2D>();

        // Resetting path variables for the box
        FollowPath path = box_obj.GetComponent<FollowPath>();

        if (!path.coroutineAllowed)
        {
            path.routeToGo = 0;
            path.tParam = 0f;
            path.coroutineAllowed = true;
        }

        Vector3 temp = transform.position;
        temp.z = 0f;

        box_obj.transform.position = temp;
    }

    public void SendBackToPool(GameObject obj)
    {
        LeanPool.Despawn(obj);
    }
}
