using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miner : Peon
{
    float timer = 0f;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        peon = this.gameObject;
        gameManager = FindObjectOfType<GameManager>();
        currentResource = 0;
        maxResource = 15;
        gatherRate = 3;
        hasTool = true;
        toolUses = 20;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (hasTool && currentResource >= maxResource /*&& isBeingAttacked == false*/)
        {
            //ReturnResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.townCentre.transform.position);
        }
        else if (hasTool && currentResource < maxResource /*&& isBeingAttacked == false*/)
        {
            //GetResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.mineLocation.transform.position);
        }
        else if (!hasTool /*&& isBeingAttacked == false*/)
        {
            //GetNewTool();
            nav.ResetPath();
            nav.SetDestination(gameManager.smithLocation.transform.position);
        }
        else if (false /*&& isBeingAttacked == true */)
        {
            //Panic();
        }

        if (toolUses <= 0)
        {
            hasTool = false;
        }

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

    void ReturnResource()
    {
        if (currentResource > 0)
        {
            gameManager.ore += currentResource;
            currentResource = 0;
        }
    }

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
