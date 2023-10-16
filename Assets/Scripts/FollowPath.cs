using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] paths;

    [HideInInspector]
    public int routeToGo;

    [HideInInspector]
    public float tParam;

    [SerializeField]
    private float speedModifier;

    public Vector2 objectPosition;

    [HideInInspector]
    public bool coroutineAllowed;

    private ObjectScript objectScript;
    private ObjectSpawner objectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        objectScript = GetComponent<ObjectScript>();
        objectSpawner = FindObjectOfType<ObjectSpawner>();

        paths[0] = FindObjectOfType<BezierPath>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Stop the path follow coroutine
        if (!objectScript.canMove)
        {
            StopAllCoroutines();
            return;
        }
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    /// <summary>
    /// This coroutine moves the stackable item in a curved path set in the editor.
    /// </summary>
    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector2 p0 = paths[routeNum].GetChild(0).position;
        Vector2 p1 = paths[routeNum].GetChild(1).position;
        Vector2 p2 = paths[routeNum].GetChild(2).position;
        Vector2 p3 = paths[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        // Send the box back to pool when it reaches the last path point.
        // Decrement score
        if (routeToGo > paths.Length - 1)
        {
            //Subtract one score.
            if (objectScript.cat.Score >= 1)
            {
                objectScript.cat.Score = -1;
            }

            objectSpawner.SendBackToPool(this.gameObject);
            StopAllCoroutines();
        }
    }
}
