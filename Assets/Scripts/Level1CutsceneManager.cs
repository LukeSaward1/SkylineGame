using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CutsceneManager : MonoBehaviour
{
    public Animator animator;
    public GameObject Cam;
    public GameObject vCam;
    public GameObject Player;

    public void StartCutscene(){
        animator.SetTrigger("zoomin");
    }

    public void CutsceneStart(){
        Cam.SetActive(false);
        vCam.SetActive(true);
    }

    public void OnCutsceneComplete(){
        Cam.SetActive(true);
        vCam.SetActive(false);
    }

    public void Start(){
        PlayerMovement PM = Player.GetComponent<PlayerMovement>();
        PM.enabled = false;
    }

    public void Update(){

    }
}