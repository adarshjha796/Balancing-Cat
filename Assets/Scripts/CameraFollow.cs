using System.Collections;
using System.Collections.Generic;
//using DatabaseAPI.Account;
using Lean.Transition;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public void CamMove()
    {
        transform.positionTransition_y(transform.position.y + 2.8f, 2.8f);
        VFXHandler.Instance.explosionOffset += 2.8f;
    }
}
