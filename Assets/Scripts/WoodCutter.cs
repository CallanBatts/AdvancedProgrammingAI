using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WoodCutter : Peon
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
    void Start ()
    {
        currentResource = 0;
        maxResource = 10;
        gatherRate = 4;
        hasTool = true;
        toolUses = 20;
	}
	
	// Update is called once per frame
	void Update ()
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
            nav.SetDestination(gameManager.treeLocation.transform.position);
            
        }
        else if (!hasTool /*&& isBeingAttacked == false*/)
        {
            //GetTool();
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
        
    }

    void OnTriggerEnter (Collider whatHit)
    {
        if (whatHit.tag == gameManager.townCentre.tag)
        {
            ReturnResource();
        }

        if (whatHit.tag == gameManager.treeLocation.tag)
        {
            GatherResource();
        }

        if (whatHit.tag == gameManager.smithLocation.tag)
        {
            GetTool();
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

    void ReturnResource()
    {
        gameManager.wood += currentResource;
        currentResource = 0;
    }

    void GetTool()
    {
        if (gameManager.tools > 0 && !hasTool)
        {
            gameManager.tools--;
            toolUses = 20;
            hasTool = true;
        }
    }
}
