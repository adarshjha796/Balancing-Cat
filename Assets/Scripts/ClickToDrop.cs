using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Common;

public class ClickToDrop : LeanSelectableBehaviour
{
    private new Rigidbody2D rigidbody;

    public GameObject distanceMeter;

    [HideInInspector]
    public TextMesh distanceFromCatsHeadTxt;

    public bool isEnabled;

    protected override void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        distanceMeter = GameObject.FindGameObjectWithTag("Meter");
        distanceFromCatsHeadTxt = distanceMeter.transform.GetChild(0).GetComponent<TextMesh>();
    }

    /// <summary>
    /// This is an overriden method which gets called whenever a LeanSelectableObject is selected
    /// </summary>
    protected override void OnSelected()
    {
        GetComponent<ObjectScript>().canMove = false;
        rigidbody.gravityScale = 0f;
        isEnabled = true;
    }

    /// <summary>
    /// This is an overriden method which gets called whenever a LeanSelectableObject is deselected
    /// </summary>
    protected override void OnDeselected()
    {
        EnableGravity();
        EnableDistanceMeter();
    }

    /// <summary>
    /// This Method will enable gravity for the object that this script is attached to.
    /// </summary>
    private void EnableGravity()
    {
        GetComponent<ObjectScript>().canMove = false;
        rigidbody.gravityScale = 1f;
    }

    /// <summary>
    /// This method will enable the distance meter 
    /// </summary>
    private void EnableDistanceMeter()
    {
        isEnabled = true;
        distanceMeter.SetActive(true);
    }

    /// <summary>
    /// This method will disable the child of this gameobject which is the distance meter 
    /// </summary>
    public void DisableDistanceMeter()
    {
        isEnabled = false;
        distanceMeter.GetComponent<DistanceMeter>().currentTargetItem = null;
    }

    private void Update()
    {
        if (isEnabled && transform.position.y > Cat.latestDistanceMeterValue && GamePlayController.instance.numberOfBoxesOnCatsHead >= 0)
        {
            distanceMeter.transform.position = new Vector3(0, GetComponent<ObjectScript>().itemTop.position.y, 0);
        }
    }
}
