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
    private TMP_Text _savedScore;
    [SerializeField]
    private GameObject _instructionPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _victoryPanel;
    [SerializeField]
    private Canvas _canvas;

    private int _currentScore = 0;
    private SceneLoader _sceneLoader;
        
    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        if(_sceneLoader == null)
        {
            Debug.LogError("Scene Loader is Null!");
        }

        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _scoreText.text = "Score: " + _currentScore.ToString();
        ShowGamePanel();
    }

    public void IncrementScore()
    {
        _currentScore++;
        _scoreText.text = "Score: " + _currentScore.ToString();
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
        int savedScore;
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        HideAllPanels();
        _victoryPanel.SetActive(true);
        _victoryScoreText.text = _currentScore.ToString();

        savedScore = PlayerPrefs.GetInt("DiscPeg", 999);

        if (_currentScore < savedScore)
        {
            PlayerPrefs.SetInt("DiscPeg", _currentScore);
        }

        _savedScore.text = savedScore.ToString();

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
