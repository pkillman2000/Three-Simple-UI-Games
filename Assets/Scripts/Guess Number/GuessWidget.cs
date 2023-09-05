using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessWidget : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] _guesses;
    [SerializeField]
    private TMP_Text _correctPosition;
    [SerializeField]
    private TMP_Text _correctDigit;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public void SetGuess(int index, string guess)
    {
        _guesses[index].text = guess;
    }

    public void SetCorrectPosition(string correct)
    {
        _correctPosition.text = correct;
    }

    public void SetCorrectDigit(string correct) 
    {
        _correctDigit.text = correct;
    }
}
