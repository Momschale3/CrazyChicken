
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CickenMovement : MonoBehaviour
{
    /*/--------------------------------------------------------------------------------------------------------------/*/

    //NavMeshAgent
    private NavMeshAgent agent;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    //Gameobjects
    private GameObject[] FurnitureArray;

    // NEW!! EYPERIMENTAL!
    public GameObject furniTemp;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    //Ints
    private int number1;
    private float timer;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    //Bools
    public bool isAtFurniture = false;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    [Header("Chicken Stats")]
    [Space(10f)]
    public float wanderTimer;
    public int wanderRadius;

    [Tooltip("Die maximale Distanz zwischen dem Huhn und dem M�belst�ck")]
    public int furnitureNoticeDistance;

    [Tooltip("Hier kannst du die Geschwindigkeit des H�hnchens ver�ndern.")]
    public int chickenSpeed;

    public LayerMask furniture_LM;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    private void Awake()
    {
        #region Assignements 

        agent = GetComponent<NavMeshAgent>();
        FurnitureArray = GameObject.FindGameObjectsWithTag("Furniture");

        #endregion Assignements

        #region Chicken Stats

        agent.speed = chickenSpeed;

        #endregion Chicken Stats
    }

    /*/--------------------------------------------------------------------------------------------------------------/*/

    public void Update()
    {

        #region Chicken Furniture or random Walk
        timer += Time.deltaTime;

        if (timer >= wanderTimer && isAtFurniture == false) //Abklingzeit vom Laufpunkten
        {
            if (Physics.CheckSphere(this.gameObject.transform.position, wanderRadius, furniture_LM)) //Falls M�bel in Radius -> Got To M�bel
            {
                isAtFurniture = true;
                ChickenToFurniture();
                

            }
            else //Falls nicht --> such weiterer Punkt
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        #endregion #region Chicken Furniture or random Walk

    }

    /*/--------------------------------------------------------------------------------------------------------------/*/

    private void ChickenToFurniture()
    {

        Debug.Log("Furni gefunden!");

        # region NEW block of code
        // distance of the chicken to the first furniture object (reason: see l. 109)
        float distanceTemp = Vector3.Distance(FurnitureArray[0].gameObject.transform.position, this.gameObject.transform.position);
        furniTemp = FurnitureArray[0];
        // GameObject furniTemp = FurnitureArray[0];
        #endregion NEW block of code


        foreach (GameObject furni in FurnitureArray) //Geh durch alle M�bel druch und suche das was am n�chsten ist
        {
            
            #region OLD block of code:
            /*
             hat nicht das je näheste Möbelstück genommen, sondern das erste, das die Bedingung erfüllt (ergo sind immer alle zum gleichen Möbelstück gelaufen)
            if(Vector3.Distance(furni.gameObject.transform.position, this.gameObject.transform.position) <= wanderRadius + 10)
            {
                agent.SetDestination(furni.transform.position);               
            }
            */
            #endregion OLD block of code:

            #region NEW block of code:
            // if the distance of chicken to the (temporary) furni is smaller than the last one...
            if (Vector3.Distance(furni.gameObject.transform.position, this.gameObject.transform.position) < distanceTemp)
            {
                // ... then set it as the (new) goal...
                furniTemp = furni;
                // ... and update the distance to which the next furni will be compared
                distanceTemp = Vector3.Distance(furni.gameObject.transform.position, this.gameObject.transform.position);
            }
                agent.SetDestination(furniTemp.transform.position);
            #endregion NEW block of code:
            
            
        }
       
    }

    /*/--------------------------------------------------------------------------------------------------------------/*/

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    /*/--------------------------------------------------------------------------------------------------------------/*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, wanderRadius);

    }

    /*/--------------------------------------------------------------------------------------------------------------/*/

}
