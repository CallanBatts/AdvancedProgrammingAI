using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject townCentre;       //The location of the town centre
    public GameObject treeLocation;     //The location of the trees
    public GameObject smithLocation;    //The location of the smith
    public GameObject mineLocation;     //The location of the mine

    public GameObject EnemyPrefab;      //The Enemy Prefab
    public GameObject GuardPrefab;      //The Guard Prefab

    public int wood;                    //The total amount of wood the town has
    public int ore;                     //The total amount of ore the town has
    public int tools;                   //The total amount of tools the town has

    public Text woodText;               //The text that displays the wood on screen
    public Text oreText;                //The text that displays the ore on screen
    public Text toolsText;              //The text that displays the tools on screen

    float enemySpawnTimer = 0;          //The timer determining when the enemies spawn
    float guardSpawnTimer = 0;          //The timer determining when the guards spawn

    GameObject[] guardCount;

    void Awake()
    {
        //Awake set the locations and initial resources
        townCentre = GameObject.FindGameObjectWithTag("TownCentre");
        treeLocation = GameObject.FindGameObjectWithTag("Trees");
        smithLocation = GameObject.FindGameObjectWithTag("Smith");
        mineLocation = GameObject.FindGameObjectWithTag("Mine");
        wood = 0;
        ore = 0;
        tools = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        guardCount = GameObject.FindGameObjectsWithTag("Guard");        //Update() loop continues to update the guardCount so that it stays accurate

        //The following lines display the text to the screen
        woodText.text = "Wood: " + wood.ToString();
        oreText.text = "Ore: " + ore.ToString();
        toolsText.text = "Tools: " + tools.ToString();

        //runs the spawn timers off of the deltaTime
        enemySpawnTimer += Time.deltaTime;
        guardSpawnTimer += Time.deltaTime;

        //Small if statement to spawn the enemies when the timer has reached a certain limit
        if (enemySpawnTimer > 20f)
        {
            Instantiate(EnemyPrefab, new Vector3(14, 1, -14), Quaternion.identity);
            enemySpawnTimer = 0;
        }

        //Small if statement to spawn the guards when the timer has reached a certain limit
        if (guardSpawnTimer > 30f && guardCount.Length < 3)
        {
            Instantiate(GuardPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            guardSpawnTimer = 0;
        }
    }
}
