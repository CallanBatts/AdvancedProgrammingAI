using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peon : MonoBehaviour
{
    protected GameManager gameManager;      //Reference to the GameManager Script that all Peons will be able to access. This save me from having to type this line more than once.
    protected NavMeshAgent nav;             //Reference to the NavMeshAgent each peon is equiped with. Also saved me from having to type this line more than once.

    public int health;                      //The Health of the Peon.
    protected int currentResource;          //How much the Peon is currently carrying of it's resource
    protected int maxResource;              //The maximum amount of resource that this peon can carry
    protected int gatherRate;               //how fast it gathers its particular resource
    protected bool hasTool;                 //Bool to check whether or not it currently has a tool
    protected int toolUses;                 //The uses of the tool before it breaks
    protected bool isBeingAttacked;         //Checks whether or not the Peon is being attacked.

    //  The DamagePeon method does a certain amount of damage to the peon.
    //  This method is used only by the Guard and Enemy Peons as they are the only ones that attack each other.
    //  I decided to have this method here as it saves space in both the Guard and Enemy scripts as it is something that they both have in common.
    protected void DamagePeon(int damage)
    {
        health -= damage;
    }

    //  The Panic Method is what causes the worker peons to run back to the town centre when the enemies are attacking the town.
    //  I decided to have the method here as well as the damage peon as it was simple enough to implement without being dependent upon each specific peon's code.
    protected void Panic()
    {
        nav.destination = gameManager.townCentre.transform.position;
    }
}
