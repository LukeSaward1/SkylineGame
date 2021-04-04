using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBox : MonoBehaviour
{
    public GameObject Cam;
    public GameObject Player;
    public Rigidbody rb;

    void Start()
    {
        GameObject theLevelManager = GameObject.Find("levelManager");
        LevelManager levelManag = theLevelManager.GetComponent<LevelManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement PM = Player.GetComponent<PlayerMovement>();
        GameObject.Find("Canvas").SendMessage("Started");
        GameObject.Find("Canvas").SendMessage("StartSpeedrun");
        GameObject.Find("Canvas").SendMessage("StartIGT");
        Cam.SetActive(true);
        PM.enabled = true;
        rb.velocity = Vector3.zero;
    }
}
