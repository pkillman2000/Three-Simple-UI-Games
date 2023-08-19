using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Peg : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int _pegIndex;

    private int _pegYLocation;
    private DPManager _dpManager;


    private void Start()
    {
        _dpManager = FindObjectOfType<DPManager>();
        if(_dpManager == null)
        {
            Debug.LogError("DP Manager is Null!");
        }

        // Tell DP Manager Y location of this peg
        _dpManager.SetPegYLocation(_pegIndex, this.transform.localPosition.y);
    }

    
    private void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        _dpManager.UpdateDiscLocation(eventData.pointerDrag.gameObject, _pegIndex); 
    }
}
