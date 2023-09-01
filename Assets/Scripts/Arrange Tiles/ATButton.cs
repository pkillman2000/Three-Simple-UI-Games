using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ATButton : MonoBehaviour, IPointerClickHandler
{
    private Image _buttonImage;
    private TMP_Text _valueText;
    private ATManager _atManager;

    [SerializeField]
    private Sprite _buttonSprite;
    [SerializeField]
    private Sprite _selectedSprite;
    [SerializeField]
    private Sprite _neighborSprite;
    [SerializeField]
    private Sprite _notNeighborSprite;

    [SerializeField]
    private int _buttonIndex;
    [SerializeField]
    private int _buttonValue;
    [SerializeField]
    private string _buttonStatus;


    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        if (_buttonImage == null)
        {
            Debug.LogError("Image is null!");
        }

        //This only works if buttons are in order at beginning of game
        _valueText = this.gameObject.GetComponentInChildren<TMP_Text>();
        if( _valueText == null)
        {
            Debug.LogError("Text is Null!");
        }
        else
        {
            _buttonValue = Int32.Parse(_valueText.text);
            _buttonIndex = _buttonValue - 1;
        }

        _atManager = FindObjectOfType<ATManager>();
        if(_atManager == null)
        {
            Debug.LogError("AT Manager is Null!");
        }

    }

    public void SetButtonStatus(string status)
    {
        _buttonStatus = status;
        switch(_buttonStatus) 
        {
            case "Normal":
                _buttonImage.sprite = _buttonSprite;
                break;
            case "Selected":
                _buttonImage.sprite = _selectedSprite;
                break;
            case "Neighbor":
                _buttonImage.sprite = _neighborSprite;
                break;
            case "Not Neighbor":
                _buttonImage.sprite = _notNeighborSprite;
                break;
        }
    }

    public int GetButtonValue()
    { 
        return _buttonValue; 
    }

    public void SetButtonValue(int value)
    {
        _buttonValue = value;
        this.gameObject.GetComponentInChildren<TMP_Text>().text = value.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_buttonStatus != "Not Neighbor")
        {
            _atManager.TilePressed(_buttonIndex);
        }
    }
}
