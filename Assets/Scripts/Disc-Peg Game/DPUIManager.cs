using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class DPUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private GameObject _instructionPanel;
    [SerializeField]
    private GameObject _gamePanel;

    private int _score = 0;
        
    private void Start()
    {
        _scoreText.text = "Score: " + _score.ToString();
        HideInstructions();
    }

    public void IncrementScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score.ToString();
    }

    public void ShowInstructions()
    {
        _instructionPanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void HideInstructions()
    {
        _instructionPanel.SetActive(false);
        _gamePanel.SetActive(true);
    }
}
