using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _discs;
    [SerializeField]
    private float[] _pegsY;
    [SerializeField]
    private int[] _slotsX;
    [SerializeField]
    private GameObject[,] _pegs_Slots;

    private void Start()
    {
        _pegs_Slots = new GameObject[3, 5];

        // Start discs on Peg 0, in proper spots
        // Update location in Disc.SetDiscLocation();
        int discIndex = 4; // Largest disc in Slot 0
        for (int i = 0; i < _discs.Length; i++)
        {
            _pegs_Slots[0, i] = _discs[i];
            _discs[discIndex].GetComponent<Disc>().SetDiscLocation(0, i, new Vector3(_slotsX[i], _pegsY[0], 0));
            _discs[discIndex].transform.localPosition = new Vector3(_slotsX[i], _pegsY[0], 0);
            discIndex--;
        }
    }

    
    private void Update()
    {
        
    }

    public void SetPegYLocation(int peg, float y)
    {
        _pegsY[peg] = y;
    }

    public void UpdateDiscLocation(GameObject disc, int newPeg)
    {
        int discSize = disc.GetComponent<Disc>().GetDiscSize();
        int oldPeg = disc.GetComponent<Disc>().GetPeg();
        int oldSlot = disc.GetComponent<Disc>().GetSlot();
        bool isValidMove = true;
        int firstEmptySlot = 0;

        // Remove from old slot
        _pegs_Slots[oldPeg, oldSlot] = null;

        // Check to see if smaller than top disc on peg -
        for(int i = 0; i < _discs.Length; i++) 
        {
            if (_pegs_Slots[newPeg, i] != null)
            {
                firstEmptySlot = i + 1;
                if (_pegs_Slots[newPeg, i].GetComponent<Disc>().GetDiscSize() < discSize)
                {
                    isValidMove = false;
                }
            }
        }
        if (isValidMove)
        {
            _pegs_Slots[newPeg, firstEmptySlot] = disc;
            disc.GetComponent<Disc>().SetDiscLocation(newPeg, firstEmptySlot, new Vector3(_slotsX[firstEmptySlot], _pegsY[newPeg], 0));
            disc.transform.localPosition = new Vector3(_slotsX[firstEmptySlot], _pegsY[newPeg], 0);
        }
        else
        {
            disc.GetComponent<Disc>().ReturnToOriginalSlot();
        }
    }
}
