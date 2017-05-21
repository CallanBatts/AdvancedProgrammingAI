using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miner : Peon   //Inherits from the Peon Class to save me writing down the same things over again
{
    float timer = 0f;   //Timer used to control the gatherRate
    int peonState;      //peonState is used to control the Switch Statement. It determines which state the Finite State Machine is in.

    //Awake() sets up all of the variable for this peon
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        gameManager = FindObjectOfType<GameManager>();
        currentResource = 0;
        maxResource = 15;
        gatherRate = 3;
        hasTool = true;
        toolUses = 20;
        isBeingAttacked = false;
    }

    void Update()
    {
        timer += Time.deltaTime;    //Sets the timer to run off of deltaTime

        //Main logic of the Peon
        switch (peonState)
        {
            case 0: //ReturnResource() State
                nav.ResetPath();
                nav.SetDestination(gameManager.townCentre.transform.position);
                if (hasTool && currentResource < maxResource && isBeingAttacked == false)
                {
                    peonState = 1;
                }
                else if (!hasTool && !isBeingAttacked)
                {
                    peonState = 2;
                }
                break;

            case 1: //GatherResource() State
                nav.ResetPath();
                nav.SetDestination(gameManager.mineLocation.transform.position);
                if (hasTool && currentResource >= maxResource && isBeingAttacked == false)
                {
                    peonState = 0;
                }
                else if (!hasTool && !isBeingAttacked)
                {
                    peonState = 2;
                }
                else if (isBeingAttacked)
                {
                    peonState = 3;
                }
                break;

            case 2: //GetNewTool State
                nav.ResetPath();
                nav.SetDestination(gameManager.smithLocation.transform.position);
                if (hasTool && currentResource >= maxResource && isBeingAttacked == false)
                {
                    peonState = 0;
                }
                else if (hasTool && currentResource < maxResource && isBeingAttacked == false)
                {
                    peonState = 1;
                }
                break;

            case 3: //Panic State
                Panic();
                if (!isBeingAttacked)
                {
                    peonState = 0;
                }
                break;
        }

        //if statement to check whether or not an enemy has spawned
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            isBeingAttacked = true;
        }
        else
        {
            isBeingAttacked = false;
        }

        //if statement to check whether or not the tool still has uses
        if (toolUses <= 0)
        {
            hasTool = false;
        }

        //if statement to check if the peon has health or not
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // OnTriggerEnter checks whether the collider has hit a trigger or not.
    // I use this method to determine where the peon is and which building/resource location it is currently at.
    void OnTriggerEnter(Collider whatHit)
    {
        //Debug.Log("Hit Something");
        if (whatHit.tag == gameManager.townCentre.tag && peonState == 0)
        {
            ReturnResource();
        }

        if (whatHit.tag == gameManager.mineLocation.tag)
        {
            GatherResource();
        }

        if (whatHit.tag == gameManager.smithLocation.tag)
        {
            GetNewTool();
        }
    }

    // ReturnResource() is how the peon returns their current resource back to the town centre's supply    
    void ReturnResource()
    {
        if (currentResource > 0)
        {
            gameManager.ore += currentResource;
            currentResource = 0;
        }
    }

    // GatherResource() is how the peon gathers it's specific resource and is only run at it's specific resource location
    void GatherResource()
    {
        if (currentResource < maxResource)
        {
            if (timer >= 2f)
            {
                currentResource += gatherRate;
                toolUses--;
                timer = 0;
            }
        }
    }

    // GetTool() allows the peon to get itself another tool
    void GetNewTool()
    {
        if (gameManager.tools > 0 && !hasTool)
        {
            gameManager.tools--;
            toolUses = 20;
            hasTool = true;
        }
    }
}
