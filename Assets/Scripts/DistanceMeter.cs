using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeter : MonoBehaviour
{
    public Transform currentTargetItem;

    [SerializeField]
    private TextMesh distanceTxt;

    /// <summary>
    /// This Method will return the meter's current distance up to 1 decimal place.
    /// </summary>
    public float GetCurrentDistanceTextValue()
    {
        string[] str = distanceTxt.text.Split(' ');

        float distance = float.Parse(str[0]);
        return distance;
    }
}
