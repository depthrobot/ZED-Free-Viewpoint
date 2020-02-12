using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class CameraSwitch : MonoBehaviour
{
    public List<Camera> Cameras;



    private void Start()
    {



        //initial camera setup (realsense by default)
        EnableCamera(0, 1);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Stereo");
            EnableCamera(0, 1);
        }
               
        if (Input.GetKeyDown(KeyCode.Alpha2) )
        {
            print("Depth");
            UnityEngine.XR.InputTracking.Recenter();
            EnableCamera(2, 3);

        }


    }

    private void EnableCamera(int n, int m)
    {
        Cameras.ForEach(cam => cam.enabled = false);
        Cameras[n].enabled = true;
        if (m >= 0)
            Cameras[m].enabled = true;
    }

}

