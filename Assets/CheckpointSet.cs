using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSet : MonoBehaviour
{
    public GameObject RespawnObject;
    public bool checkpointReached;

    void OnTriggerEnter(Collider other)
    {
        checkpointReached = true;
        Debug.Log("Checkpoint reached.");
    }

    void Update()
    {
        
    }

    void Start()
    {

    }
}
