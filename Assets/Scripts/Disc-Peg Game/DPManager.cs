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

    private DPUIManager _dpUIManager;

    private void Start()
    {
        _dpUIManager = FindObjectOfType<DPUIManager>();
        if(_dpUIManager == null ) 
        {
            Debug.LogError("DP UI Manager is Null!");
        }

        _pegs_Slots = new GameObject[3, 5];

        // Start discs on Peg 0, in proper spots
        // Update location in Disc.SetDiscLocation();
        int discIndex = 4; // Largest disc in Slot 0
        for (int i = 0; i < _discs.Length; i++)
        {
            _pegs_Slots[0, i] = _discs[discIndex];
            _discs[discIndex].GetComponent<Disc>().SetDiscLocation(0, i, new Vector3(_slotsX[i], _pegsY[0], 0));
            _discs[discIndex].transform.localPosition = new Vector3(_slotsX[i], _pegsY[0], 0);
            discIndex--;
        }

        CalculateCanMove();
    }

    public void SetPegYLocation(int peg, float y)
    {
        _pegsY[peg] = y;
    }

    public void UpdateDiscLocation(GameObject disc, int newPeg)
    {
        Disc discScript = disc.GetComponent<Disc>();
        if(discScript == null)
        {
            Debug.Log("Disc Script is Null!");
        }

        int discSize = discScript.GetDiscSize();
        int oldPeg = discScript.GetPeg();
        int oldSlot = discScript.GetSlot();
        bool isValidMove = true;
        int firstEmptySlot = 0;


        // Check to see if smaller than top disc on peg -
        for(int i = 0; i < _discs.Length; i++) 
        {
            if (_pegs_Slots[newPeg, i] != null)
            {
                firstEmptySlot = i + 1;
                int slotDiscSize = _pegs_Slots[newPeg, i].GetComponent<Disc>().GetDiscSize();
                //Debug.Log("New Peg: " + newPeg + " Slot: " + i);
                //Debug.Log("Slot Disc Size: " + slotDiscSize + " Moving Disc Size: " + discSize);
                if (slotDiscSize <= discSize)
                {
                    isValidMove = false;
                }
            }
        }

        if (isValidMove)
        {
            // Remove from old slot
            _pegs_Slots[oldPeg, oldSlot] = null;
            // Add to new slot
            _pegs_Slots[newPeg, firstEmptySlot] = disc;
            discScript.SetDiscLocation(newPeg, firstEmptySlot, new Vector3(_slotsX[firstEmptySlot], _pegsY[newPeg], 0));
            disc.transform.localPosition = new Vector3(_slotsX[firstEmptySlot], _pegsY[newPeg], 0);
            // Housekeeping
            _dpUIManager.IncrementScore();
            CalculateCanMove();
            CheckForVictory();
        }
        else
        {
            discScript.ReturnToOriginalSlot();
        }
    }

    private void CalculateCanMove()
    {
        for(int peg = 0;  peg < _pegsY.Length; peg++) // Cycle through pegs
        {
            for(int slot = 0; slot < _slotsX.Length-1; slot++) // Cycle through all but top slot
            {
                if (_pegs_Slots[peg, slot] != null)
                {
                    if (_pegs_Slots[peg, slot +1] != null)
                    {
                        _pegs_Slots[peg, slot].GetComponent<Disc>().SetCanMove(false);
                    }
                    else
                    {
                        _pegs_Slots[peg, slot].GetComponent<Disc>().SetCanMove(true);
                    }
                }
            }
        }
    }

    private void CheckForVictory()
    {
        if (_pegs_Slots[_pegsY.Length-1, _slotsX.Length-1] != null) 
        {
            Debug.Log("Victory!");
            // stop all disc movement
            // Fireworks - Text
            // Write score to playerpref if necessary
        }
    }
}
