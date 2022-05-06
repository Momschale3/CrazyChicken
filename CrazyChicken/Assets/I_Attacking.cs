using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Attacking : MonoBehaviour
{
    // public variables
    public int destroySpeed = 1;

    // private variables

    private bool atFurni; // is the chicken currently at furniture
    private bool furniFlag = false;

    private GameObject attackedFurni;
    private CickenMovement CM;
    private I_Destructable ID;

    void Start()
    {
        CM = GetComponent<CickenMovement>();
    }

    void Update()
    {
        atFurni = CM.isAtFurniture;
        if (atFurni && !furniFlag)
        {
            attackedFurni = CM.furniTemp; // get the attacked GameObject
            ID = attackedFurni.GetComponent<I_Destructable>(); // get access to the script that holds the health of the furni
            furniFlag = true; // makes sure the code is just executed once
        }
        if (atFurni && ID.health > 0)
        {
            ID.health--; // reduce the health of the furniture
        }
        if (!atFurni)
        {
            furniFlag = false; // set the flag back to false, so the thrown chicken would be able to attack another furni again
        }
    }
}
