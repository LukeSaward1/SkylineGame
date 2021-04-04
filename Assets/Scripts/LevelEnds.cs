using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnds : MonoBehaviour
{
    public Vector3 Level1SpawnPoint;
    public Vector3 Level2SpawnPoint;
    public Vector3 Level3SpawnPoint;
    public Vector3 Level4SpawnPoint;
    public Vector3 Level5SpawnPoint;

    public AudioLowPassFilter alpF;
    public GameObject leanController;
    public GameObject Player;
    public TimerController tC;
    public Rigidbody rb;
    public DiscordController DC;
    public GameObject MainMenuCanvas;
    public GameObject BlackFadeCanvas;
    public GameObject SpeedrunCanvas;
    public LevelManager LM;

    public GameObject endCanvas;

    public void Level1 ()
    {
        LM.levelID += 1;
        Debug.Log("LevelID set to " + LM.levelID);
        BlackFadeCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
        SpeedrunCanvas.SetActive(true);
        Player.transform.position = Level1SpawnPoint;
        endCanvas.SetActive(false);
        Player.SetActive(true);
        alpF.enabled = false;
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = true;
        tC.timeCounter.enabled = true;
        tC.IGTCounter.enabled = true;
        rb.velocity = Vector3.zero;
    }

    public void Level2 ()
    {
        Player.transform.position = Level2SpawnPoint;
        endCanvas.SetActive(false);
        Player.SetActive(true);
        alpF.enabled = false;
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = true;
        tC.timeCounter.enabled = true;
        tC.IGTCounter.enabled = true;
        tC.Started();
        rb.velocity = Vector3.zero;
    }

    public void mainMenu ()
    {
        SceneManager.LoadScene("Main Menu");
    }
}