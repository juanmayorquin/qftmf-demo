using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBoxController : MonoBehaviour
{
    [SerializeField] private float defaultMass;
    [SerializeField] private float lightMass;
    
    
    private SwitchCharacter _switchCharacter;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        if (!_switchCharacter)
        {
            _switchCharacter = FindObjectOfType<SwitchCharacter>();
        }

        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangeMass();
    }

    private void ChangeMass()
    {
        if (_switchCharacter.characterEnum == SwitchCharacter.Character.Mario)
        {
            _rb2D.mass = lightMass;
        }
        else
        {
            _rb2D.mass = defaultMass;
        }
    }
}
