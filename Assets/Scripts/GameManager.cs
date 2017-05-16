using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject townCentre;
    public GameObject treeLocation;
    public GameObject smithLocation;
    public GameObject mineLocation;

    public GameObject EnemyPrefab;
    public GameObject GuardPrefab;

    public int wood;
    public int ore;
    public int tools;

    public Text woodText;
    public Text oreText;
    public Text toolsText;

    float enemySpawnTimer = 0;
    float guardSpawnTimer = 0;

    void Awake()
    {
        townCentre = GameObject.FindGameObjectWithTag("TownCentre");
        treeLocation = GameObject.FindGameObjectWithTag("Trees");
        smithLocation = GameObject.FindGameObjectWithTag("Smith");
        mineLocation = GameObject.FindGameObjectWithTag("Mine");
    }

    // Use this for initialization
    void Start ()
    {
        wood = 0;
        tools = 20;
	}
	
	// Update is called once per frame
	void Update ()
    {
        woodText.text = "Wood: " + wood.ToString();
        oreText.text = "Ore: " + ore.ToString();
        toolsText.text = "Tools: " + tools.ToString();

        enemySpawnTimer += Time.deltaTime;
        guardSpawnTimer += Time.deltaTime;

        if (enemySpawnTimer > 20f)
        {
            Instantiate(EnemyPrefab, new Vector3(14, 1, -14), Quaternion.identity);
            enemySpawnTimer = 0;
        }

        if (guardSpawnTimer > 30f)
        {
            Instantiate(GuardPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            guardSpawnTimer = 0;
        }
    }
}
