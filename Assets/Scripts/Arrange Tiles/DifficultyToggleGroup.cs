using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyToggleGroup : MonoBehaviour
{
    [SerializeField]
    private Toggle _mediumToggle;

    private void Start() // Set initial difficulty to Easy
    {
        _mediumToggle.Select();
    }
}
