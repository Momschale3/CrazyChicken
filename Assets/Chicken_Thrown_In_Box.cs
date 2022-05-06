using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Thrown_In_Box : MonoBehaviour
{
    public GameManager GM;

    public int chickenValue;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Chicken")
        {
            GM.AddScore(chickenValue);
            Destroy(other.gameObject);
        }

    }

}
