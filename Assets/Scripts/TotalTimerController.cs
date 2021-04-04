using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TotalTimerController : MonoBehaviour
{
    public static TotalTimerController instance;

    [HeaderAttribute("Objects")]
    public TextMeshProUGUI totalTimeCounter;
    public TextMeshProUGUI endTotalTimeCounter;

    public GameObject startTrigger;
    public GameObject endCanvas;
    public GameObject Player;
    public AudioLowPassFilter alpF;
    public GameObject leanController;
    public TimeSpan TotalTimePlaying;

    [Header("Timer Info")]
    public bool TotalTimerGoing;
    public bool finished = false;
    public bool started = false;
    public bool speedrunStarted;
    public bool speedrunEnded;
    public float TotalElapsedTime;
    public string TotalTimePlayingString;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = true;

        totalTimeCounter.text = "00:00.000";
        TotalTimerGoing = false;
    }

    public void BeginTimer()
    {
        TotalTimerGoing = true;
        startTrigger.SetActive(true);

        StartCoroutine(UpdateTimer());
    }

    public void RespawnResetTimer()
    {
        TotalTimerGoing = false;
        TotalTimerGoing = true;
    }

    public void EndTimer()
    {
        TotalTimerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (TotalTimerGoing)
        {
            TotalElapsedTime += Time.deltaTime;
            TotalTimePlaying = TimeSpan.FromSeconds(TotalElapsedTime);
            TotalTimePlayingString = TotalTimePlaying.ToString("mm':'ss'.'fff");
            totalTimeCounter.text = TotalTimePlayingString;
            endTotalTimeCounter.text = TotalTimePlayingString;
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
        TotalTimerGoing = false;
        totalTimeCounter.color = Color.red;
        totalTimeCounter.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endCanvas.SetActive(true);
        Player.SetActive(false);
        alpF.enabled = true;
        GameObject.Find("Cam").GetComponent<LeanBehaviour>().enabled = false;
    }

    public void Started()
    {
        started = true;
        TotalTimerGoing = true;
        StartCoroutine(UpdateTimer());
        totalTimeCounter.color = Color.black;
        startTrigger.SetActive(false);
        endCanvas.SetActive(false);
        Player.SetActive(true);
        alpF.enabled = false;
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

    void DestroyGameObject()
    {
        Destroy(startTrigger);
    }
}
