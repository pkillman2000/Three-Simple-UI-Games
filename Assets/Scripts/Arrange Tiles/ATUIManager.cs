using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ATUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject _difficultyPanel;
    [SerializeField]
    private GameObject _instructionPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _victoryPanel;

    [SerializeField]
    private TMP_Text _scoreText;
    private int _currentScore;

    [SerializeField]
    private TMP_Text _victoryScore;

    private void Start()
    {
        DifficultyPanelActive();
        _currentScore = 0;
    }
    
    private void HideAllPanels()
    {
        _difficultyPanel.SetActive(false);
        _instructionPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _victoryPanel.SetActive(false);
    }

    public void DifficultyPanelActive()
    {
        HideAllPanels();
        _difficultyPanel.SetActive(true);
    }

    public void InstructionPanelActive() 
    {
        HideAllPanels();
        _instructionPanel.SetActive(true);
    }

    public void GamePanelActive() 
    {
        HideAllPanels();
        _gamePanel.SetActive(true);
    }

    public void VictoryPanelActive() 
    {
        HideAllPanels();
        _victoryPanel.SetActive(true);
        _victoryScore.text = _currentScore.ToString();
        StartCoroutine(VictoryTextBlinkRoutine());
    }

    private IEnumerator VictoryTextBlinkRoutine()
    {
        bool isOn = true;

        while (true)
        {
            _victoryScore.gameObject.SetActive(isOn);
            yield return new WaitForSeconds(0.5f);
            isOn = !isOn;
        }
    }

    public void IncrementScore()
    {
        _currentScore++;
        _scoreText.text = "Score: " + _currentScore.ToString();
    }
}
