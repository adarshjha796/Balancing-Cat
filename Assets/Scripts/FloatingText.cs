using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    //public static FloatingText ft;

    [SerializeField]
    private GameObject floatingTextPrefab;

    //private void Awake()
    //{
    //    if (ft == null)
    //    {
    //        ft = this;
    //    }
    //}

    public void ShowDamage()
    {
        if (floatingTextPrefab != null)
        {
            //Debug.Log("aaa");
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity) ;
            prefab.transform.Rotate(transform.rotation.x, transform.rotation.x, Random.Range(-45f, 45f));
            //prefab.GetComponent<TextMesh>().text = text;
        }
    }
}
