using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraX : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform mainCam;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            mainCam.transform.Rotate(0.0f, 0f, -20.0f);
        }

        if(Input.GetKeyUp(KeyCode.Q))
        {
            mainCam.transform.Rotate(0.0f, 0f, 0.0f);
        }
    }


}
