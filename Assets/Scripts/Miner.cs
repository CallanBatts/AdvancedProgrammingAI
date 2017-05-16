using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miner : Peon
{
    int currentResource;
    int maxResource;
    int gatherRate;
    bool hasTool;
    int toolUses;

    float timer = 0f;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        peon = this.gameObject;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start()
    {
        currentResource = 0;
        maxResource = 15;
        gatherRate = 3;
        hasTool = true;
        toolUses = 20;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (hasTool && currentResource >= maxResource /*&& isBeingAttacked == false*/)
        {
            //ReturnResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.townCentre.transform.position);

            if (peon.transform.position == gameManager.townCentre.transform.position)
            {
                //gameManager.ore += currentResource;
                //currentResource = 0;
            }

        }
        else if (hasTool && currentResource < maxResource /*&& isBeingAttacked == false*/)
        {
            //GetResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.mineLocation.transform.position);

            if (peon.transform.position == gameManager.mineLocation.transform.position)
            {
                //if (currentResource < maxResource)
                //{
                //    if (timer >= 2f)
                //    {
                //        currentResource += gatherRate;
                //        toolUses--;
                //        timer = 0;
                //    }
                //}
            }
        }
        else if (!hasTool /*&& isBeingAttacked == false*/)
        {
            //GetNewTool();
            nav.ResetPath();
            nav.SetDestination(gameManager.smithLocation.transform.position);

            if (peon.transform.position == gameManager.smithLocation.transform.position)
            {
                //Debug.Log("Mr Anderson");
                //if (gameManager.tools > 0)
                //{
                //    gameManager.tools--;
                //    toolUses = 20;
                //    hasTool = true;
                //}
            }
        }
        else if (false /*&& isBeingAttacked == true */)
        {
            //Panic();
        }

        if (toolUses <= 0)
        {
            hasTool = false;
        }
        /*
        Debug.Log("CurrentResource: " + currentResource);
        Debug.Log("hasTool? " + hasTool);
        Debug.Log("Tool usage: " + toolUses);
        */
    }

    void OnTriggerEnter(Collider whatHit)
    {
        //Debug.Log("Hit Something");
        if (whatHit.tag == gameManager.townCentre.tag)
        {
            gameManager.ore += currentResource;
            currentResource = 0;
        }

        if (whatHit.tag == gameManager.mineLocation.tag)
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

        if (whatHit.tag == gameManager.smithLocation.tag)
        {
            if (gameManager.tools > 0 && !hasTool)
            {
                gameManager.tools--;
                toolUses = 20;
                hasTool = true;
            }
        }
    }
}
