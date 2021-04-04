using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class LeanBehaviour : MonoBehaviour {
     
     [HeaderAttribute("Camera Settings")]
     public Transform _Pivot;
     public Camera Camera;

     public float currentLeanAngle = 0f;
     public float leanSpeed = 100f;
     public float maxLeanAngle = 20f;
 
     // Use this for initialization
     void Awake () {
         if (_Pivot == null && transform.parent != null) _Pivot = transform.parent;
     }

     [HeaderAttribute("FOV Settings")]
     public float m_FieldOfView;

     [HeaderAttribute("Look Settings")]
     public Transform player;
     
     void Start(){
         m_FieldOfView = 90.0f;
     }
     // Update is called once per frame
     void Update () {
         // lean left
         if (Input.GetKey(KeyCode.Q))
         {
             currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, maxLeanAngle, leanSpeed * Time.deltaTime);
         }
         // lean right
         else if (Input.GetKey(KeyCode.E))
         {
             currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, -maxLeanAngle, leanSpeed * Time.deltaTime);
         }
         // reset lean
         else
         {
             currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, 0f, leanSpeed * Time.deltaTime);
         }
 
         _Pivot.transform.localRotation = Quaternion.AngleAxis(currentLeanAngle, Vector3.forward);

        //Update the camera's field of view to be the variable returning from the Slider
        Camera.fieldOfView = m_FieldOfView;

        transform.position = player.transform.position;
     }
 }