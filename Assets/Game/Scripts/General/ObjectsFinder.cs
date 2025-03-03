using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFinder : MonoBehaviour
{
    [SerializeField] private Bot_behavior[] _yourObjects;
    [SerializeField] private bool _includeInactive;

    void Start()
    {
        _yourObjects = FindObjectsOfType<Bot_behavior>(_includeInactive);
    }

}
