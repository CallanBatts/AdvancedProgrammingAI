using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peon : MonoBehaviour
{
    protected GameManager gameManager;
    protected GameObject peon;
    protected NavMeshAgent nav;
    public int health;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void DamagePeon(int damage)
    {
        health -= damage;
    }
}
