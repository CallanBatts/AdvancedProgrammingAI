using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Peon   //Inherits from the Peon Class to save me writing down the same things over again. Which is ironic because i've written down this note like seven times
{
    float timer = 0;        //Timer is used to keep track of the enemies attack rate
    int damage = 3;         //The amount of damage the enemy does

    int peonState = 0;      //peonState is used to control the Switch Statement. It determines which state the Finite State Machine is in.

    //Awake() sets up all of the variable for this peon
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        gameManager = FindObjectOfType<GameManager>();
    }

	void Update ()
    {
        timer += Time.deltaTime;    //Sets the timer to run off of deltaTime
        GameObject guard = GameObject.FindGameObjectWithTag("Guard");       //Finds the Guard GameObject
        GameObject[] peons = GameObject.FindGameObjectsWithTag("Peon");     //Finds the Peons GameObjects

        //Enemy Logic loop
        switch (peonState)
        {
            case 0:     //Attacking Guards State
                AttackPeon(guard);
                if (guard == null)
                {
                    peonState = 1;
                }
                break;
            case 1:     //Attacking Peons State
                AttackPeon(peons[0]);
                if (peons == null)
                {
                    peonState = 2;
                }
                break;
            case 2:     //Victory State
                nav.SetDestination(gameManager.townCentre.transform.position);
                break;
        }

        //Health check to determine if the enemy is still alive or not
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //AttackPeon allows the enemy to target any Peon and damage them. It only requires a target.
    void AttackPeon(GameObject target)
    {
        Debug.Log(target);
        nav.ResetPath();
        nav.SetDestination(target.transform.position);

        if (nav.remainingDistance < 0.5f && timer > 2f)
        {
            target.SendMessage("DamagePeon", damage);
            timer = 0;
        }
    }
}
