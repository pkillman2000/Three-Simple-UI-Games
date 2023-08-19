using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Disc : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private int _discSize;
    public int _currentPeg;
    public int _currentSlot;
    private Vector3 _originalLocation;

    private Image _image;
    private Color _tempColor;

    private void Start()
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

    public void OnPointerDown(PointerEventData eventData)
    {
        _tempColor = _image.color;
        _tempColor.a = 0.5f;
        _image.color = _tempColor;
        _image.raycastTarget = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tempColor = _image.color;
        _tempColor.a = 1.0f;
        _image.color = _tempColor;
        _image.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
}
