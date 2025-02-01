using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public int maxDrop;

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (!_other.gameObject.CompareTag("Player")) return;

        _other.gameObject.GetComponent<PlayerInventory>().AddItem(gameObject, Random.Range(0, maxDrop));
    }
}