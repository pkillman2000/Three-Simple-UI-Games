using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class GNManager : MonoBehaviour
{
    private string _randomCode;
    [SerializeField]
    private List<string> _randomValues;
    private string _currentGuess;

    [SerializeField]
    private GameObject _guessWidgetPrefab;
    [SerializeField]
    private Transform _guessesParent;
    private GameObject _currentGuessWidgetPrefab;
    private GuessWidget _currentGuessWidgetScript;
    private int _currentCorrectPosition;
    private int _currentCorrectDigit;

    private GNUIManager _gnUIManager;
    private int _currentTurn;

    private void Start()
    {
        _gnUIManager = FindObjectOfType<GNUIManager>();
        if(_gnUIManager == null)
        {
            Debug.LogError("GN UI Manager is NULL!");
        }
        _currentGuess = "";
        _currentTurn = 0;

        CreateRandomCode();
        SpawnNewGuessWidget();
    }

    private void CreateRandomCode()
    {
        List<string> tempValues = _randomValues; // Create a list that can have digits removed from
        int tempIndex = 0;
        _randomCode = "";

        for (int i = 0; i < 4; i++) 
        {
            tempIndex = Random.Range(0, tempValues.Count);
            _randomCode += tempValues[tempIndex]; // Add digit to code
            tempValues.RemoveAt(tempIndex); // Stop digit from being used again
        }
    }

    private void SpawnNewGuessWidget()
    {
        _currentGuessWidgetPrefab = null;
        _currentGuessWidgetPrefab = Instantiate(_guessWidgetPrefab);
        _currentGuessWidgetPrefab.transform.SetParent(_guessesParent);
        _currentGuessWidgetScript = _currentGuessWidgetPrefab.GetComponent<GuessWidget>();
        _currentTurn++;
        _currentGuess = "";
    }

    public void SetCurrentGuess(string inputCode)
    {
        int currentIndex = _currentGuess.Length;

        // Cannot be longer than 4 digits
        // Cannot have repeat digits
        if((currentIndex < 4) && (!_currentGuess.Contains(inputCode)))
        {
            _currentGuess += inputCode;
            _currentGuessWidgetScript.SetGuess(currentIndex, inputCode);
        }
    }

    public void ClearCurrentGuess()
    {
        _currentGuess = "";
        for (int i = 0; i < 4; i++) 
        {
            _currentGuessWidgetScript.SetGuess(i, "");
        }
    }

    public void TestCurrentGuess()
    {
        _currentCorrectDigit = 0;
        _currentCorrectPosition = 0;
        int currentIndex;
        char[] brokenGuess = _currentGuess.ToCharArray();
        char[] brokenCode = _randomCode.ToCharArray();

        if (_currentTurn < 10) // Cannot be more than 10 turns
        {
            if (_currentGuess.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    currentIndex = _randomCode.IndexOf(brokenGuess[i]);
                    if (brokenGuess[i] == brokenCode[i]) // Correct Position
                    {
                        _currentCorrectPosition++;
                    }
                    else if (currentIndex >= 0) // Correct Digit
                    {
                        _currentCorrectDigit++;
                    }
                }
            }
        }
        else
        {
            _gnUIManager.OpenDefeatPanel();
        }

        _currentGuessWidgetScript.SetCorrectPosition(_currentCorrectPosition.ToString());
        _currentGuessWidgetScript.SetCorrectDigit(_currentCorrectDigit.ToString());

        if(_currentCorrectPosition < 4)
        {
            SpawnNewGuessWidget();
        }
        else
        {
            _gnUIManager.OpenVictoryPanel(_currentTurn);
        }
    }
}
