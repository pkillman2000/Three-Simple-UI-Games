using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ATManager : MonoBehaviour
{
    /*
    * Difficulty is set by the On Value Changed in the 
    * difficulty toggle.  Easy = 5, Medium = 10, Hard = 20
    * The numbers are the number of tile swaps when initially 
    * setting up the game.
    */
    [SerializeField]
    private int _difficulty;

    /*
     * if _firstButtonIndex != -1 then the first button has been pressed.
     * If _secondButtonIndex != -1 then the second button has been pressed.
    */
    private int _firstButtonIndex = -1;
    private int _secondButtonIndex = -1;
    private int _tempButtonValue;

    /*
     * 
    */
    [SerializeField]
    private List<int> _neighbors = new List<int>();

    [SerializeField]
    private GameObject[] _buttonTiles;

    private ATUIManager _uiManager;

    private void Start()
    {
        // Default Difficulty is Medium
        SetDifficulty(10);

        _uiManager = FindObjectOfType<ATUIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is Null!");
        }
    }

    public void SetDifficulty(int difficulty)
    {
        _difficulty = difficulty;
    }

    public void TilePressed(int tileIndex)
    {

        if (_firstButtonIndex == -1) 
        {
            _firstButtonIndex = tileIndex;
            TurnOffTiles();
            _buttonTiles[_firstButtonIndex].GetComponent<ATButton>().SetButtonStatus("Selected");

            FindNeighbors(_firstButtonIndex);
            DisplayNeighbors();
        }
        else 
        { 
            _secondButtonIndex = tileIndex;
            SwapTiles(_firstButtonIndex, _secondButtonIndex);
            _uiManager.IncrementScore();
            TurnOnTiles();
        }
    }

    public void ShuffleTiles()
    {
        for (int i = 1; i <= _difficulty; i++) 
        { 
            _firstButtonIndex = Random.Range(0, _buttonTiles.Length);
            FindNeighbors(_firstButtonIndex);
            _secondButtonIndex = _neighbors[Random.Range(0, _neighbors.Count)];
            SwapTiles(_firstButtonIndex, _secondButtonIndex);
        }

        /*
         * resets values so that first or second button press
         * can be detected.
        */
        _firstButtonIndex = -1;
        _secondButtonIndex = -1;
    }

    private void FindNeighbors(int tileIndex)
    {
        _neighbors.Clear();
        if(tileIndex > 3) // Bottom
        {
            _neighbors.Add(tileIndex - 4);
        }
        if(tileIndex < 11) // Top
        {
            _neighbors.Add(tileIndex + 4);
        }
        if((tileIndex > 0) && (tileIndex%4 != 0)) // Left - if on left side of grid, next lower index cannot be neighbor
        {
            _neighbors.Add(tileIndex - 1);
        }
        if((tileIndex < 15) && ((tileIndex+1)%4 != 0)) // Right - if on right side of grid, next higer index cannot be neighbor
        {
            _neighbors.Add(tileIndex + 1);
        }
    }

    private void SwapTiles(int indexOne, int indexTwo)
    {
        int buttonOneValue = _buttonTiles[indexOne].GetComponent<ATButton>().GetButtonValue();
        int buttonTwoValue = _buttonTiles[indexTwo].GetComponent<ATButton>().GetButtonValue();
        int tempButtonValue = buttonTwoValue;

        _buttonTiles[indexTwo].GetComponent<ATButton>().SetButtonValue(buttonOneValue);
        _buttonTiles[indexOne].GetComponent<ATButton>().SetButtonValue(tempButtonValue);

    }

    private void TurnOffTiles()
    {
        for(int i = 0;  i < _buttonTiles.Length; i++)
        {
            _buttonTiles[i].GetComponent<ATButton>().SetButtonStatus("Not Neighbor");
        }
    }

    private void TurnOnTiles()
    {
        for (int i = 0; i < _buttonTiles.Length; i++)
        {
            _buttonTiles[i].GetComponent<ATButton>().SetButtonStatus("Normal");
        }

        /*
        * resets values so that first or second button press
        * can be detected.
        */
        _firstButtonIndex = -1;
        _secondButtonIndex = -1;
        CheckForVictory();
    }

    private void DisplayNeighbors()
    {
        for(int i = 0; i < _neighbors.Count; i++) 
        {
            _buttonTiles[_neighbors[i]].GetComponent<ATButton>().SetButtonStatus("Neighbor");
        }
    }

    private void CheckForVictory()
    {
        bool isVictory = true;

        for(int i = 0; i < _buttonTiles.Length; i++) 
        {
            if(i+1 != _buttonTiles[i].GetComponent<ATButton>().GetButtonValue())
            {
                isVictory = false;
                break;
            }
        }

        if(isVictory) 
        {
            _uiManager.VictoryPanelActive();
        }
    }
}
