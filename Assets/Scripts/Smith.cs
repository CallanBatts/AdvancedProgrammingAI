using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Smith : Peon
{
    float timer = 0f;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        peon = this.gameObject;
        gameManager = FindObjectOfType<GameManager>();
        currentResource = 0;
        maxResource = 5;
        gatherRate = 1;
        hasTool = true;
        toolUses = 5;
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
            //GatherResource();
            nav.ResetPath();
            nav.SetDestination(gameManager.smithLocation.transform.position);
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

        if (whatHit.tag == gameManager.smithLocation.tag && hasTool)
        {
            GatherResource();
        }
        else if (whatHit.tag == gameManager.smithLocation.tag && !hasTool)
        {
            GetNewTool();
            
        }
    }

    void ReturnResource()
    {
        if (currentResource > 0)
        {
            gameManager.tools++;
            gameManager.wood -= 5;
            gameManager.ore -= 10;
            currentResource = 0;
        }
    }

    void GatherResource()
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

    void GetNewTool()
    {
        if (gameManager.tools > 0)
        {
            gameManager.tools--;
            toolUses = 20;
            hasTool = true;
        }
    }
}
