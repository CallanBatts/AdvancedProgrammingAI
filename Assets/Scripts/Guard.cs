using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Peon   //Inherits from the Peon Class to save me writing down the same things over again
{
    bool outsideTown;                   //outsideTown keeps track of whether the guard is inside the town perimiter or not
    float timer = 0;                    //timer is a timer to keep track of the guards attack rate
    int damage = 5;                     //how much damage the guard does

    int peonState = 0;                  //peonState is used to control the Switch Statement. It determines which state the Finite State Machine is in.

    public List<GameObject> points;     //List of all of the Patrol points the Guard will patrol through
    int destPoint = 0;                  //which of the current patrol points the guard is currently moving towards

    //Awake() sets up all of the variable for this peon
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        gameManager = FindObjectOfType<GameManager>();

        outsideTown = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;        //Sets the timer to run off of deltaTime

        //Main logic loop of the Guard Peon
        switch (peonState)
        {
            case 0: //Patrol() State
                Patrol();
                if (CanSeeEnemy())
                {
                    peonState = 1;
                }
                break;

            case 1: //Attack() State
                Attack();
                break;

            case 2: //ReturnToTown() State
                ReturnToTown();
                if (!outsideTown)
                {
                    peonState = 0;
                }
                break;
        }

        //if statement check to destroy the guard if it's health is too low.
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // CanSeeEnemy checks whether or not there is an enemy Gameobject active
    bool CanSeeEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            return true;
        }
        return false;
    }

    //ReturnToTown sends the guard back to the town
    void ReturnToTown()
    {
        nav.ResetPath();
        nav.SetDestination(GameObject.FindGameObjectWithTag("Town").transform.position);
    }

    //Loops the Patrol of the guard
    void Patrol()
    {
        if (nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    //Keeps the guards looping the patrol rather than eventually stopping
    void GoToNextPoint()
    {
        if (points.Capacity == 0)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }

        nav.destination = points[destPoint].transform.position;
        destPoint = (destPoint + 1) % points.Capacity;
    }

    //attacks the enemy game object
    void Attack()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            peonState = 2;
        }

        nav.ResetPath();
        nav.SetDestination(enemy.transform.position);

        if (nav.remainingDistance < 0.5f && timer > 2f)
        {
            enemy.SendMessage("DamagePeon", damage);
            timer = 0;
        }
    }

    //onTriggerStay checks for triggers against the collider as well
    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Town")
        {
            outsideTown = false;
        }
    }
}
