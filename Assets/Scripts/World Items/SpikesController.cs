using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> spikesRb2D;

    void DropSpikes()
    {
        foreach (var rbs in spikesRb2D)
        {
            rbs.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DropSpikes();
            Debug.Log("is here");
        }
    }
}
