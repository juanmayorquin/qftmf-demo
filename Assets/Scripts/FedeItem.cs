using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FedeItem : MonoBehaviour
{
    [SerializeField] private SwitchCharacter _switchCharacter;

    [SerializeField] private List<GameObject> itemsToActive;

    [SerializeField] private bool isFede;

    private void Update()
    {
        ActiveItems(isFede);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _switchCharacter.characterEnum == SwitchCharacter.Character.Fede)
        {
            isFede = true;
        }
        else
        {
            isFede = false;
        }
    }

    private void ActiveItems(bool value)
    {
        foreach (var item in itemsToActive)
        {
            if (item != null)
            {
                item.SetActive(value);
            }
        }
    }
}
