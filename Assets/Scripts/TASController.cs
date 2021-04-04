using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TASController : MonoBehaviour
{
    [HeaderAttribute("Assignables")]
    public AudioSource BGM;
    public PlayerMovement PM;
    public Respawn RS;
    public RandomSoundStart RSS;
    public TimerController TC;
    public LeanBehaviour LB;
    public AudioLowPassFilter ALPF;
    public Transform camTransform;
    public TextMeshProUGUI SaveLoadText;
    private float timeToAppear = 1f;
    private float timeWhenDisappear;
    public Vector3 pos;
    public Vector3 vel;

    [HeaderAttribute("Game Settings")]

    // Bools
    public bool isPaused;
    public bool isFrameAdvancing;
    public string gameSpeedValue;
    
    // Floats
    public float gameSpeed = 1f;
    public float gameSpeedPercentage;

    // Assignables
    public TextMeshProUGUI SpeedrunTimer;

    [HeaderAttribute("Savestate Settings")]
    [Header("Saving")]
    public bool State1Saved = false;
    public bool State2Saved = false;
    public bool State3Saved = false;
    public bool State4Saved = false;
    public bool State5Saved = false;
    public bool State6Saved = false;
    public bool State7Saved = false;
    public bool State8Saved = false;
    public bool State9Saved = false;
    public bool State10Saved = false;

    [Header("Loading")]
    public bool State1Loaded = false;
    public bool State2Loaded = false;
    public bool State3Loaded = false;
    public bool State4Loaded = false;
    public bool State5Loaded = false;
    public bool State6Loaded = false;
    public bool State7Loaded = false;
    public bool State8Loaded = false;
    public bool State9Loaded = false;
    public bool State10Loaded = false;

    [Header("Statistics")]
    public int rerecords;
    public bool hasSavestated;

    [Header("Values")]
    // TimerController
    public bool timerGoing;
    public bool finished;
    public bool started;
    public bool speedrunStarted;
    public bool speedrunEnded;
    public float elapsedTime;
    public float igtElapsedTime;
    public string timePlayingString;
    public string igtString;

    // StatsController
    public bool statsCanvasEnabled = false;
    public bool isPlayerGrounded;
    public string playerGroundedState;
    public bool isPlayerWallrunning;
    public string playerWallrunState;
    public float roll;
    public float FOV;
    public float Speed;
    public string SpeedometerReading;

    // LevelManager
    public int levelID;

    // Lean Controller
    public float currentLeanAngle;

    // Savestates Part 2
    public float BGMTime;

    void Start()
    {
        SaveLoadText.enabled = false;
    }

    void Update()
    {
        Rigidbody rb = PM.GetComponent<Rigidbody>();

        if(Input.GetKeyDown(KeyCode.F)){
            isFrameAdvancing = true;
            isPaused = true;
            Time.timeScale = 3f;
            Debug.Log("FrameAdvance Key Pressed.");
            Time.timeScale = 0f;
            Debug.Log("Game is paused.");
            BGM.volume = 0f;
            BGMTime = BGM.time;
        }
        else{
            isFrameAdvancing = false;
            if(isPaused == true)
            {
                Time.timeScale = 0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.Pause))
        {
            isPaused = true;
            Debug.Log("Pause Key Pressed.");
            Time.timeScale = 0f;
            BGM.pitch = 0f;
            Debug.Log("Game is paused.");
        }

        if(isPaused == true && Input.GetKeyDown(KeyCode.Pause)) 
        {
            isPaused = false;
            Time.timeScale = 1f;
            BGM.pitch = 1f;
            BGM.volume = 1f;
            BGM.time = BGMTime;
            Debug.Log("Resumed normal game speed.");
        }

        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            Time.timeScale += 0.1f;
            BGM.pitch += 0.1f;
        }

        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Time.timeScale -= 0.1f;
            BGM.pitch -= 0.1f;
        }

        gameSpeed = Time.timeScale;
        gameSpeedValue = gameSpeedPercentage.ToString() + "%";
        gameSpeedPercentage = gameSpeed * 100;

        if(Time.timeScale != 1.0f)
        {
            SpeedrunTimer.color = Color.blue;
        }
        else{
            SpeedrunTimer.color = Color.black;
        }

        // Okay, here we go... 500 lines later... we still going? Okay, more lines henceforth shall appear.
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State1Loaded = false;
            State1Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 1";
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State1Saved = false;
            State1Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 1";
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State2Loaded = false;
            State2Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 2";
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State2Saved = false;
            State2Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 2";
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State3Loaded = false;
            State3Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 3";
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State3Saved = false;
            State3Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 3";
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State4Loaded = false;
            State4Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 4";
        }

        if(Input.GetKeyDown(KeyCode.F4))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State4Saved = false;
            State4Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 4";
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State5Loaded = false;
            State5Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 5";
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State5Saved = false;
            State5Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 5";
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State6Loaded = false;
            State6Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 6";
        }

        if(Input.GetKeyDown(KeyCode.F6))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State6Saved = false;
            State6Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 6";
        }

        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State7Loaded = false;
            State7Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 7";
        }

        if(Input.GetKeyDown(KeyCode.F7))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State7Saved = false;
            State7Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 7";
        }

        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State8Loaded = false;
            State8Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 8";
        }

        if(Input.GetKeyDown(KeyCode.F8))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State8Saved = false;
            State8Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 8";
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State9Loaded = false;
            State9Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 9";
        }

        if(Input.GetKeyDown(KeyCode.F9))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State9Saved = false;
            State9Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 9";
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            vel = rb.velocity;
            pos = rb.position;
            currentLeanAngle = LB.currentLeanAngle;
            State10Loaded = false;
            State10Saved = true;
            BGMTime = BGM.time;
            hasSavestated = true;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            SaveLoadText.text = "Saved state 10";
        }

        if(Input.GetKeyDown(KeyCode.F10))
        {
            Rigidbody RB = PM.GetComponent<Rigidbody>();
            rb.velocity = vel;
            rb.position = pos;
            State10Saved = false;
            State10Loaded = true;
            LB.currentLeanAngle = currentLeanAngle;
            BGM.time = BGMTime;

            SaveLoadText.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

            if(hasSavestated == true)
            {
                rerecords += 1;
            }

            SaveLoadText.text = "Loaded state 10";
        }

        if(SaveLoadText.enabled && (Time.time >= timeWhenDisappear))
        {
            SaveLoadText.enabled = false;
        }

    }
}