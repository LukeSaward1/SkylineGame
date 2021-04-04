using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2StartBox : MonoBehaviour
{
    void Start()
    {
        GameObject theLevelManager = GameObject.Find("levelManager");
        LevelManager levelManag = theLevelManager.GetComponent<LevelManager>();
        levelManag.levelID += 1;
        Debug.Log("LevelID set to " + levelManag.levelID);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Canvas").SendMessage("Started");
    }
}
