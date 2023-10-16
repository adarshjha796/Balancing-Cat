using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Pool;

/// <summary>
/// This script is atached to the object which we need to satck on each other.
/// </summary>
public class ObjectScript : MonoBehaviour
{
    [Header("floats")]
    public static float distance;

    [Header("transform")]
    public Transform itemTop;
    [SerializeField]
    private Transform catTop;
    [SerializeField]
    private Transform itemContainer;

    [Header("booleans")]
    [HideInInspector]
    public bool canMove;
    //[HideInInspector] 
    public bool hasCollided = false;

    [Header("scripts")]
    [HideInInspector]
    public Cat cat;
    private ClickToDrop clickToDrop;
    private CameraFollow cam;
    private UIHandler uIHandler;

    [Header("gameObjects")]
    public GameObject distanceMeter;
    public GameObject catPlatform;
    private GameObject boxes;

    [Header("rigidbodies")]
    private Rigidbody2D rb;

    private void Awake()
    {
        uIHandler = FindObjectOfType<UIHandler>();
        rb = GetComponent<Rigidbody2D>();
        cat = FindObjectOfType<Cat>();
        clickToDrop = GetComponent<ClickToDrop>();
        distanceMeter = GameObject.FindGameObjectWithTag("Meter");
        catPlatform = GameObject.FindGameObjectWithTag("Platform");
        rb.gravityScale = 0f;
    }

    void Start()
    {
        canMove = true;
        catTop = GameObject.FindGameObjectWithTag("Cat top").transform;
        itemContainer = GameObject.FindGameObjectWithTag("ItemContainer").transform;
        GamePlayController.instance.currentObject = this;
        cam = FindObjectOfType<CameraFollow>();
        boxes = GameObject.FindGameObjectWithTag("Respawn");
    }

    void Update()
    {
        if (boxes.transform.childCount < 2)
        {
            distanceMeter.transform.position = new Vector3(0.039f, 6.58f, 0);
        }

        /// Update the distance between this object and cat's head.
        if (canMove) return;

        if (clickToDrop.isEnabled)
        {
            UpdateDisanceFromCatsHead();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(0, 0, 90);
            }
        }
    }

    /// <summary>
    /// Check if 2 boxes have hit the floor and show gameover screen if they have.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GameOver") && !GamePlayController.instance.isGameOver)
        {
            GetComponent<ClickToDrop>().DisableDistanceMeter();

            VFXHandler.Instance.SpawnExplosionVFX(transform.position);

            if (GamePlayController.instance.BoxesOnFloor >= 2)
            {
                GamePlayController.instance.isGameOver = true;

                itemContainer.GetComponent<BoxCollider2D>().enabled = false;

                GameObject boxes = GameObject.FindGameObjectWithTag("Respawn");

                for (int i = 0; i < boxes.transform.childCount; i++)
                {
                    boxes.transform.GetChild(i).GetComponent<ClickToDrop>().isEnabled = false;
                }

                cat.ChangeAnimation();
                GamePlayController.instance.OnGameOver();
                Destroy(gameObject);
            }
            else
            {
                GamePlayController.instance.BoxesOnFloor = 1;
                //GamePlayController.instance.BoxesOnHead = -1;
                Destroy(gameObject);
            }
        }

        //if (collision.CompareTag("GameOver") && hasCollided)
        //{
        //    // Subtract one score.
        //    if (cat.Score >= 1)
        //    {
        //        //FindObjectOfType<FloatingText>().ShowDamage();
        //        cat.Score = -1;
        //        cat.minusCounter++;
        //    }

        //    hasCollided = false;
        //}


        if (collision.CompareTag("Drop"))
        {
            clickToDrop.isEnabled = true;
            hasCollided = true;

            // Check if box has collided already..
            if (hasCollided)
            {
                GameObject dropPoint = GameObject.FindGameObjectWithTag("Drop");
                dropPoint.GetComponent<BoxCollider2D>().enabled = false;

                //hasCollided = true;

                GamePlayController.instance.BoxesOnHead = 1;

                uIHandler.UpdateItem(GamePlayController.instance.numberOfBoxesOnCatsHead);

                // Add one score
                cat.Score = (GamePlayController.instance.numberOfBoxesOnCatsHead * distance) - cat.minusCounter;
                cat.Score = Mathf.Round(cat.Score * 100f) / 100f;

                GamePlayController.instance.BoxesOnHead = 1;
            }
        }
    }


    /// <summary>
    /// If Collectable objects touch each other, the distance meter will get disabled.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Box") && other.gameObject.GetComponent<Rigidbody2D>().gravityScale == 1)
        {
            if (!hasCollided)
            {
                GamePlayController.instance.numberOfBoxesOnCatsHead += 1;

                uIHandler.UpdateItem(GamePlayController.instance.numberOfBoxesOnCatsHead);

                hasCollided = true;
                cat.Score = (GamePlayController.instance.numberOfBoxesOnCatsHead * distance) - cat.minusCounter;
                cat.Score = Mathf.Round(cat.Score * 100f) / 100f;
                Cat.latestDistanceMeterValue = distanceMeter.transform.position.y;
                Cat.latestDistance = distance;

                if (distance >= (3.5f * GamePlayController.instance.nextCameraMoveThreshold))
                {
                    GamePlayController.instance.nextCameraMoveThreshold += 1;
                    cam.CamMove();
                }
            }
        }
    }


    /// <summary>
    /// Keeps updating the score in the update function.
    /// </summary>
    public void UpdateDisanceFromCatsHead()
    {
        if (itemTop.transform.position.y > Cat.latestDistanceMeterValue)
        {
            distance = Vector2.Distance(itemTop.position, catTop.position);
            distance = Mathf.Round(distance * 10f) / 10f;
            clickToDrop.distanceFromCatsHeadTxt.text = distance.ToString("f1") + " cm";
        }
        else
        {
            distance = Cat.latestDistance;
            distance = Mathf.Round(distance * 10f) / 10f;
            clickToDrop.distanceFromCatsHeadTxt.text = distance.ToString("f1") + " cm";

            distanceMeter.transform.position = new Vector3(distanceMeter.transform.position.x, Cat.latestDistanceMeterValue, distanceMeter.transform.position.z);
        }
    }
}
