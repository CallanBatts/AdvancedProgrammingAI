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

    public int wood;
    public int ore;
    public int tools;

    public Text woodText;
    public Text oreText;
    public Text toolsText;

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
    }
}
