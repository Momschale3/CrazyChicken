using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Thrown_In_Box : MonoBehaviour
{
void OnCollisionEnter (Collision coll) {
Debug.Log("Chicken fell into box.");
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
