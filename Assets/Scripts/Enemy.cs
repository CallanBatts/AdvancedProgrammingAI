using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Peon
{
    float timer = 0;
    int damage = 3;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        health = 20;
        peon = this.gameObject;
        gameManager = FindObjectOfType<GameManager>();
    }

	void Update ()
    {
        timer += Time.deltaTime;
        GameObject guard = GameObject.FindGameObjectWithTag("Guard");
        GameObject[] peons = GameObject.FindGameObjectsWithTag("Peon");

        if (guard != null)
        {
            AttackPeon(guard);
        }
        else if (peons != null)
        {
            AttackPeon(peons[0]);
        }
        else
        {
            nav.SetDestination(gameManager.townCentre.transform.position);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

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
