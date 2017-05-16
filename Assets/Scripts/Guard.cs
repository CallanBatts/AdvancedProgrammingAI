using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Peon
{
    bool outsideTown;
    GameObject target;
    float timer = 0;
    int damage = 5;

    public GameObject[] points;
    int destPoint = 0;

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
        outsideTown = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (CanSeeEnemy())
        {
            Attack();
        }
        else if (outsideTown && !CanSeeEnemy())
        {
            ReturnToTown();
        }
        else if (!outsideTown && !CanSeeEnemy())
        {
            Patrol();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    bool CanSeeEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            return true;
        }
        return false;
    }

    void ReturnToTown()
    {
        nav.ResetPath();
        nav.SetDestination(GameObject.FindGameObjectWithTag("Town").transform.position);
    }

    void Patrol()
    {
        if (nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }

        nav.destination = points[destPoint].transform.position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void Attack()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        nav.ResetPath();
        nav.SetDestination(enemy.transform.position);

        if (nav.remainingDistance < 0.5f && timer > 2f)
        {
            enemy.SendMessage("DamagePeon", damage);
            timer = 0;
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Town")
        {
            outsideTown = false;
        }
    }
}
