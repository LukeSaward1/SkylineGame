using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowPassManager : MonoBehaviour
{
    public AudioLowPassFilter alpF;

    private bool finished = false;

    public void Start()
    {
        alpF.enabled = false;
    }

    public void Update()
    {
        if(finished){
            return;
        }
    }

    public void Finish(){
        alpF.enabled = true;
    }
}    
