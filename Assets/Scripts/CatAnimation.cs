using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is use to change the Cat Animation if object touches the ground.
/// </summary>
public class CatAnimation : MonoBehaviour
{
    public static CatAnimation instance;
    private Animator anim;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation()
    {
        anim.Play("stun");
    }
}
