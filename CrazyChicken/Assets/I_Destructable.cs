using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Destructable : MonoBehaviour
{
    public float health = 10000f;
    private float healthInverted;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthInverted = (health / 10000f);
        GetComponent<Renderer>().material.color = new Color(1 - healthInverted, healthInverted, 0);
    }
    
}
