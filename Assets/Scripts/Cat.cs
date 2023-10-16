using System.Collections;
using System.Collections.Generic;
//using DatabaseAPI.Account;
using Lean.Transition;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private UIHandler uIHandler;
    private Animator anim;
    private CameraFollow cam;

    public float minusCounter = 0;

    public static float latestDistanceMeterValue = -1.2f;
    public static float latestDistance = 0;

    public float currentScore;

    private void Awake()
    {
        uIHandler = FindObjectOfType<UIHandler>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = FindObjectOfType<CameraFollow>();
    }
    public float Score
    {
        get
        {
            return currentScore;
        }

        set
        {
            if (!GamePlayController.instance.isGameOver)
            {
                // If positive then add it. If negative then subtract it.
                if (Mathf.Sign(value) == 1)
                {
                    currentScore = (GamePlayController.instance.numberOfBoxesOnCatsHead * ObjectScript.distance) - minusCounter;                 
                }

                else if (Mathf.Sign(value) == -1)
                {
                    // Subtract one score.
                    if (currentScore >= 1)
                    {
                        FindObjectOfType<FloatingText>().ShowDamage();
                        currentScore--;
                        minusCounter++;
                    }
                }
                uIHandler.UpdateScore(currentScore);
            }
        }
    }

    // <summary>
    // This method is used to change the Cat animation if box touches the ground.
    // </summary>
    public void ChangeAnimation()
    {
        anim.Play("stun");
    }
}
