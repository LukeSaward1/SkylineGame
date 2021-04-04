using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    [HeaderAttribute("Objects")]
    public TextMeshProUGUI timeCounter;
    public TextMeshProUGUI IGTCounter;
    public TextMeshProUGUI EndTimeCounterIGT;
    public TextMeshProUGUI endTimeCounter;

    public GameObject startTrigger;
    public GameObject endCanvas;
    public GameObject Player;
    public AudioLowPassFilter alpF;
    public GameObject leanController;
    public TimeSpan timePlaying;
    public TimeSpan igtTS;

    public GameObject LevelManager;

    [Header("Timer Info")]
    public bool timerGoing;
    public bool finished = false;
    public bool started = false;
    public bool speedrunStarted;
    public bool speedrunEnded;
    public float elapsedTime;
    public float igtElapsedTime;
    public string timePlayingString;
    public string igtString;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = true;

        timeCounter.text = "00:00.000";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void RespawnResetTimer()
    {
        timerGoing = false;
        elapsedTime = 0f;
        timerGoing = true;
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            igtElapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            igtTS = TimeSpan.FromSeconds(igtElapsedTime);
            timePlayingString = timePlaying.ToString("mm':'ss'.'fff");
            igtString = igtTS.ToString("mm':'ss'.'fff");

            timeCounter.text = timePlayingString;
            IGTCounter.text = igtString;
            EndTimeCounterIGT.text = igtString;
            endTimeCounter.text = timePlayingString;
            yield return null;
        }
    }

    private void Update()
    {
         if (finished){
            return;
        }

        if (started){
            return;
        }
    }

    public void Finish()
    {
        finished = true;
        started = false;
        timerGoing = false;
        timeCounter.color = Color.red;
        timeCounter.enabled = false;
        IGTCounter.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endCanvas.SetActive(true);
        Player.SetActive(false);
        alpF.enabled = true;
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = false;
    }

    public void Started()
    {
        startTrigger.SetActive(true);
        elapsedTime = 0f;
        started = true;
        timerGoing = true;
        StartCoroutine(UpdateTimer());
        startTrigger.SetActive(false);
        timeCounter.color = Color.black;
        endCanvas.SetActive(false);
        Player.SetActive(true);
        alpF.enabled = false;
    }

    public void StartIGT()
    {

    }

    public void EndSpeedrun()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Level 2"){
            speedrunEnded = true;
        }
    }

    public void StartSpeedrun()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Level 1")
        {
            speedrunStarted = true;
        }
    }
}
