using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Attacking : MonoBehaviour
{
    public int destroySpeed = 1;
    private bool atFurni;
    private GameObject attackedFurni;
    private bool furniFlag = false;
    private CickenMovement CM;
    private I_Destructable ID;

    // Start is called before the first frame update
    void Start()
    {
        CM = GetComponent<CickenMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        atFurni = CM.isAtFurniture;
        if (atFurni && !furniFlag)
        {
            attackedFurni = CM.furniTemp;
            ID = attackedFurni.GetComponent<I_Destructable>();
            furniFlag = true;
        }
        if (atFurni)
        {
            ID.health--;
        }
    }
}
