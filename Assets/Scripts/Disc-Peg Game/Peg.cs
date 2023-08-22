using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Peg : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int _pegIndex;
    [SerializeField]
    private GameObject _peg;

    private DPManager _dpManager;


    private void Awake()
    {
        _dpManager = FindObjectOfType<DPManager>();
        if(_dpManager == null)
        {
            Debug.LogError("DP Manager is Null!");
        }

        // Tell DP Manager Y location of this peg
        _dpManager.SetPegYLocation(_pegIndex, _peg.gameObject.transform.localPosition.y);
    }

    public void OnDrop(PointerEventData eventData)
    {
        _dpManager.UpdateDiscLocation(eventData.pointerDrag.gameObject, _pegIndex);
    }
}
