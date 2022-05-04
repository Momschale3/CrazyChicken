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

    /*/--------------------------------------------------------------------------------------------------------------/*/

    //Ints
    private int number1;
    private float timer;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    //Bools
    private bool isAtFurniture = false;

    /*/--------------------------------------------------------------------------------------------------------------/*/

    [Header("Chicken Stats")]
    [Space(10f)]
    public float wanderTimer;
    public int wanderRadius;

    [Tooltip("Die maximale Distanz zwischen dem Huhn und dem Möbelstück")]
    public int furnitureNoticeDistance;

    [Tooltip("Hier kannst du die Geschwindigkeit des Hühnchens verändern.")]
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
            if (Physics.CheckSphere(this.gameObject.transform.position, wanderRadius, furniture_LM)) //Falls Möbel in Radius -> Got To Möbel
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

        foreach(GameObject furni in FurnitureArray) //Geh durch alle Möbel druch und suche das was am nächsten ist
        {
            if(Vector3.Distance(furni.gameObject.transform.position, this.gameObject.transform.position) <= wanderRadius + 10)
            {
                agent.SetDestination(furni.transform.position);               
            }
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
