using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerZoom : MonoBehaviour
{



    public bool ZoomActive;
    public Vector3[] Target;
    public Camera Cam;
    public float Speed;
    public float speed1;
    public float speed2;
    public float Speed3;
    // Start is called before the first frame update
    void Start()
    {


        Cam = Camera.main;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (ZoomActive)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize,speed1 + Time.deltaTime, Speed3 + Time.deltaTime);
        }else{
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, speed2 + Time.deltaTime, Speed + Time.deltaTime);

        }
    }
}
