using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsController : MonoBehaviour
{
    [HeaderAttribute("Master")]
    public GameObject PosDisplay;
    public GameObject VelDisplay;
    public GameObject RotDisplay;
    public GameObject PlayerDisplay;
    public GameObject SpeedometerDisplay;
    public bool statsCanvasEnabled = false;

    [HeaderAttribute("Objects")]
    public GameObject player;
    public GameObject cam;
    public Transform camTransform;
    public GameObject statsCanvas;

    [HeaderAttribute("Velocity")]
    public TextMeshProUGUI XVelocityDisplay;
    public TextMeshProUGUI YVelocityDisplay;
    public TextMeshProUGUI ZVelocityDisplay;
    public float xVelocity;
    public float yVelocity;
    public float zVelocity;

    [HeaderAttribute("Position")]
    public TextMeshProUGUI XPositionDisplay;
    public TextMeshProUGUI YPositionDisplay;
    public TextMeshProUGUI ZPositionDisplay;
    public float xPosition;
    public float yPosition;
    public float zPosition;

    [HeaderAttribute("Rotation")]
    public TextMeshProUGUI XRotationDisplay;
    public TextMeshProUGUI YRotationDisplay;
    public TextMeshProUGUI ZRotationDisplay;
    public float XRotation;
    public float YRotation;
    public float ZRotation;

    [HeaderAttribute("Player")]
    public TextMeshProUGUI GroundedDisplay;
    public TextMeshProUGUI RollDisplay;
    public TextMeshProUGUI FOVDisplay;
    public TextMeshProUGUI WallrunStatusDisplay;
    public bool isPlayerGrounded;
    public string playerGroundedState;
    public bool isPlayerWallrunning;
    public string playerWallrunState;
    public float roll;
    public float FOV;

    [HeaderAttribute("Speedometer")]
    public TextMeshProUGUI Speedometer;
    public float Speed;
    public string SpeedometerReading;

    public void Update()
    {
        PlayerMovement playerMvmt = player.GetComponent<PlayerMovement>();
        Rigidbody rb = playerMvmt.GetComponent<Rigidbody>();

        LeanBehaviour leanB = cam.GetComponent<LeanBehaviour>();

        XVelocityDisplay.text = "X Velocity: " + rb.velocity.x.ToString();
        YVelocityDisplay.text = "Y Velocity: " + rb.velocity.y.ToString();
        ZVelocityDisplay.text = "Z Velocity: " + rb.velocity.z.ToString();

        xPosition = rb.position.x;
        yPosition = rb.position.y;
        zPosition = rb.position.z;

        xVelocity = rb.velocity.x;
        yVelocity = rb.velocity.y;
        zVelocity = rb.velocity.z;

        XRotation = camTransform.rotation.x;
        YRotation = camTransform.rotation.y;
        ZRotation = camTransform.rotation.z;

        XPositionDisplay.text = "X Position: " + rb.position.x.ToString();
        YPositionDisplay.text = "Y Position: " + rb.position.y.ToString();
        ZPositionDisplay.text = "Z Position: " + rb.position.z.ToString();

        XRotationDisplay.text = "X Rotation: " + camTransform.eulerAngles.x.ToString();
        YRotationDisplay.text = "Y Rotation: " + camTransform.eulerAngles.y.ToString();
        ZRotationDisplay.text = "Z Rotation: " + camTransform.eulerAngles.z.ToString();

        GroundedDisplay.text = "Grounded: " + playerMvmt.grounded.ToString();
        RollDisplay.text = "Roll: " + leanB.currentLeanAngle.ToString();
        FOVDisplay.text = "FOV: " + leanB.m_FieldOfView.ToString();
        WallrunStatusDisplay.text = "Wallrun State: " + isPlayerWallrunning.ToString();

        roll = leanB.currentLeanAngle;
        FOV = leanB.m_FieldOfView;
        isPlayerGrounded = playerMvmt.grounded;
        isPlayerWallrunning = playerMvmt.isWallRunning;
        playerGroundedState = playerMvmt.grounded.ToString();
        playerWallrunState = playerMvmt.isWallRunning.ToString();

        Speed = xVelocity + zVelocity / 2;
        Speedometer.text = Speed.ToString("f0") + " KM/H";
        SpeedometerReading = Speedometer.text;
    
        if(Input.GetKeyDown(KeyCode.O))
        {
            statsCanvasEnabled = !statsCanvasEnabled;
            Debug.Log("SCE Changed to " + statsCanvasEnabled);
            PosDisplay.SetActive(statsCanvasEnabled);
            VelDisplay.SetActive(statsCanvasEnabled);
            RotDisplay.SetActive(statsCanvasEnabled);
            PlayerDisplay.SetActive(statsCanvasEnabled);
            SpeedometerDisplay.SetActive(statsCanvasEnabled);
        }
    }
}