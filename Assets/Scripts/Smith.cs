using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Smith : Peon
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
        maxResource = 5;
        gatherRate = 1;
        hasTool = true;
        toolUses = 5;        
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
                //gameManager.tools += 1;
                //gameManager.wood -= 5;
                //gameManager.ore -= 10;
                //currentResource = 0;
            }

        }
        else if (hasTool && currentResource < maxResource /*&& isBeingAttacked == false*/)
        {
            //GatherResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.smithLocation.transform.position);

            if (peon.transform.position == gameManager.smithLocation.transform.position)
            {
                //if (currentResource < maxResource)
                //{
                //    if (timer >= 3f)
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

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider whatHit)
    {
        //Debug.Log("Hit Something");
        if (whatHit.tag == gameManager.townCentre.tag)
        {
            if (currentResource > 0)
            {
                gameManager.tools ++;
                gameManager.wood -= 5;
                gameManager.ore -= 10;
                currentResource = 0;
            }
        }

        if (whatHit.tag == gameManager.smithLocation.tag && hasTool)
        {
            if (currentResource < maxResource)
            {
                if (timer >= 3f)
                {
                    currentResource += gatherRate;
                    toolUses--;
                    timer = 0;
                }
            }
        }
        else if (whatHit.tag == gameManager.smithLocation.tag && !hasTool)
        {
            if (gameManager.tools > 0)
            {
                gameManager.tools--;
                toolUses = 20;
                hasTool = true;
            }
        }
    }
}
