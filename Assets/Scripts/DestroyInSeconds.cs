using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }
}
