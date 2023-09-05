using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GNUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _victoryPanel;
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private GameObject _defeatPanel;

    private void Start()
    {
        CloseAllPanels();
        OpenGamePanel();
    }

    private void CloseAllPanels()
    {
        _instructionPanel.SetActive(false);
        _gamePanel.SetActive(false);    
        _victoryPanel.SetActive(false);
        _defeatPanel.SetActive(false);
    }

    public void OpenInstructionPanel()
    {
        CloseAllPanels();
        _instructionPanel.SetActive(true);
    }

    public void OpenGamePanel()
    {
        CloseAllPanels();
        _gamePanel.SetActive(true);
    }

    public void OpenVictoryPanel(int score)
    {
        CloseAllPanels();
        _victoryPanel.SetActive(true);
        _scoreText.text = score.ToString();
    }

    public void OpenDefeatPanel()
    {
        CloseAllPanels();
        _defeatPanel?.SetActive(true);
    }
}
