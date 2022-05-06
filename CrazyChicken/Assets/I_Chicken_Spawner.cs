using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class I_Chicken_Spawner : MonoBehaviour
{
    // the prefabs for the objects this script is attached to
    public GameObject chicken;
    public GameObject box;

/*/--------------------------------------------------------------------------------------------------------------/*/

    [Header("Adjustable Values")] [Tooltip("the number of chicken that are spawned at the first wave.")]
    [Space(10f)] 
    public int numChickenFirstWave = 3;

    [Tooltip("the number by which the number of chicken increases every value.")]
    public int chickenAddedPerWave = 1;

    [Tooltip("the time until the first wave starts")]
    public float timeFirstWave = 5f;

    [Tooltip("the time that is added each wave (if set to 0, the time would always stay the same)")]
    public float timeAddedPerWave = 1.5f;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    private float _timeSinceLastWave;

    // dynamically changing values
    private float numChickenPerWave;
    private float timePerWave;

    /*/--------------------------------------------------------------------------------------------------------------/*/
    void Start()
    {
        // setting the values to the user-defined starting values
        numChickenPerWave = numChickenFirstWave;
        timePerWave = timeFirstWave;
    }

/*/--------------------------------------------------------------------------------------------------------------/*/
    // Update is called once per frame
    void Update()
    {
        // counting the time since the last wave
        _timeSinceLastWave += Time.deltaTime;

        if (_timeSinceLastWave > timePerWave)
        {
            Debug.Log("Time for a new wave!");
            for (int i = 0; i < numChickenPerWave; i++)
            {
                // instantiate new chicken at the center of the box with random rotation
                Instantiate(chicken, box.transform.position, Random.rotation);
            }

            Debug.Log(numChickenPerWave + " chicken spawned.");

            // resetting / updating the values
            _timeSinceLastWave = 0;
            numChickenPerWave += chickenAddedPerWave;
            timePerWave += timeAddedPerWave;
        }

    }
}
