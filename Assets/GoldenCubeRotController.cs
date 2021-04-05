using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenCubeRotController : MonoBehaviour
{
    public Transform cube;
    public GameObject cubego;
    public CollectableController CC;

    public float speed;

    void Update ()
    {
        cube.Rotate (0.5f,0.6f,0.5f*Time.deltaTime); //rotates 50 degrees per second around z axis
        float y = Mathf.PingPong(Time.time * speed, 1) * 7.72f + 6.72f;
        cubego.transform.position = new Vector3(-2717.4f, y, -851.5646f);
    }

    void OnTriggerEnter (Collider other)
    {
        CC.collectableCount += 1;
        cubego.SetActive(false);
        Debug.Log("Golden Cube collected.");
    }
}
