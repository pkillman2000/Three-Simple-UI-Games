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
    private TMP_Text _victoryScoreText;
    [SerializeField]
    private GameObject _instructionPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _victoryPanel;

    private int _score = 0;
    private SceneLoader _sceneLoader;
        
    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        if(_sceneLoader == null)
        {
            Debug.LogError("Scene Loader is Null!");
        }

        _scoreText.text = "Score: " + _score.ToString();
        ShowGamePanel();
    }

    public void IncrementScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void HideAllPanels()
    {
        _instructionPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _victoryPanel.SetActive(false);
    }

    public void ShowInstructions()
    {
        HideAllPanels();
        _instructionPanel.SetActive(true);
    }

    public void ShowGamePanel()
    {
        HideAllPanels();
        _gamePanel.SetActive(true);
    }

    public void ShowVictoryPanel()
    {
        HideAllPanels();
        _victoryPanel.SetActive(true);
        _victoryScoreText.text = _score.ToString();
        StartCoroutine(VictoryTextBlinkRoutine());

    }

    private IEnumerator VictoryTextBlinkRoutine()
    {
        bool isOn = true;

        while(true)
        {
            _victoryScoreText.gameObject.SetActive(isOn);
            yield return new WaitForSeconds(0.5f);
            isOn = !isOn;
        }
    }

    public void ReturnToMenu()
    {
        _sceneLoader.LoadScene("Choose Your Game");
    }

}
