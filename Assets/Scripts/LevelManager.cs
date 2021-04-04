using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool speedrunStarted;
    public bool RichPresenceEnabled;
    public int levelID;

    public void AddLevel()
    {
        levelID += 1;
    }

    public void ResetLevelCount()
    {
        levelID += 0;
    }
}
