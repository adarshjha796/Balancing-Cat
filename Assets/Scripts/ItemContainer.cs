using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public int GetColliders() { return colliders.Count; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            //hashset automatically handles duplicates
            colliders.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!GamePlayController.instance.isGameOver)
        {
            colliders.Remove(other);
        }
    }
}
