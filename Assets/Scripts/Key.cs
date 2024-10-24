using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            door.GetComponent<Animator>().SetBool("haveKey", true);
            Destroy(this.gameObject);
        }
    }
}
