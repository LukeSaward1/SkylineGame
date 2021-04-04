using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeManager : MonoBehaviour
{
    public Animator animator;
    public int levelID;
    public bool speedrunStarted;
    public LevelEnds LE;

    public void FadeToLevel (int levelID) {
        animator.SetTrigger("fadeOut");
    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete ()
    {
        LE.Level1();
        AddLevel();
        speedrunStarted = true;
    }

    public void AddLevel()
    {
        levelID += 1;
    }

    public void ResetLevelCount()
    {
        levelID += 0;
    }
}
