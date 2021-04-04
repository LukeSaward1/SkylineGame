using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIDResetter : MonoBehaviour
{
    void Start()
    {
        GameObject theLevelManager = GameObject.Find("levelManager");
        LevelManager levelManag = theLevelManager.GetComponent<LevelManager>();
        levelManag.levelID = 0;
        Debug.Log("LevelID set to " + levelManag.levelID);
    }
}
