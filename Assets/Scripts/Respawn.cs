 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class Respawn : MonoBehaviour {

    public Vector3 SpawnPoint;
    public Transform target;
    public GameObject Player;
    public GameObject timerController;
    public Rigidbody rb;
    public TimerController tC;
    public LevelManager LM;
    public int Restarts;

    void Start(){
        rb = Player.GetComponent<Rigidbody>();
        tC = timerController.GetComponent<TimerController>();
    }
     
    void OnTriggerEnter (Collider other)
    {
        Player.transform.position = SpawnPoint;
        rb.velocity = Vector3.zero;
        target.transform.LookAt(Vector3.zero);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            Player.transform.position = SpawnPoint;
            target.transform.LookAt(Vector3.zero);
            rb.velocity = Vector3.zero;
            tC.RespawnResetTimer();
            Restarts += 1;
        }
    }
 }