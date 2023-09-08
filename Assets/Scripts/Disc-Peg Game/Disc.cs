using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing;

public class Disc : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private int _discSize;
    private int _currentPeg;
    private int _currentSlot;
    private Vector3 _originalLocation;

    private Image _image;
    private UnityEngine.Color _tempColor;

    private bool _canMove = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
        if(_image == null ) 
        {
            Debug.LogError("Image is Null!");
        }

        _originalLocation = this.transform.localPosition;
    }

    public void SetDiscLocation(int peg, int slot, Vector3 newLocation)
    {
        _currentPeg = peg;
        _currentSlot = slot;
        _originalLocation = newLocation;
    }

    public void ReturnToOriginalSlot()
    {
        this.transform.localPosition = _originalLocation;
    }

    public int GetDiscSize() 
    {
        return _discSize;
    }

    public int GetPeg()
    { 
        return _currentPeg; 
    }

    public int GetSlot() 
    {
        return _currentSlot;
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
        _image.raycastTarget = canMove;
    }

    public bool GetCanMove() 
    {
        return _canMove;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_canMove)
        {
            _tempColor = _image.color;
            _tempColor.a = 0.75f;
            _image.color = _tempColor;
            _image.raycastTarget = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_canMove)
        {
            _tempColor = _image.color;
            _tempColor.a = 1.0f;
            _image.color = _tempColor;
            _image.raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_canMove)
        {
            this.transform.position = eventData.position;
        }
    }
}
