using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class CameraSwitch : MonoBehaviour
{
    public List<Camera> Cameras;

    private bool showDepth = true;
    bool headIsMoving = true;
    private float noMovementThreshold = .03f;
    private float noRotationThreshold = 2.0f;
    private const int noMovementFrames = 60;
    private const int ovrnoMovementFrames = 3;
    Vector3[] previousTranslation = new Vector3[noMovementFrames];
    Quaternion[] previouRotation = new Quaternion[noMovementFrames];
    GameObject ovr;
    bool ismoving;
    bool pass = false;


    private void Start()
    {



        //initial camera setup (realsense by default)
        EnableCamera(0, 1);

    }

    private void Update()
    {
        ismoving = CamMoving();
        if (Input.GetKeyDown(KeyCode.Alpha1) || ismoving == false && pass == false)
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Stereo");
            EnableCamera(0, 1);
            pass = true;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha2))       
        if (Input.GetKeyDown(KeyCode.Alpha2) || (ismoving == true && pass == true) )
        {
            print("Depth");
            UnityEngine.XR.InputTracking.Recenter();
            EnableCamera(2, 3);
            pass = false;

        }


    }


    private bool CamMoving()
    {
        bool headIsMoving = false;
        ovr = GameObject.FindWithTag("ZEDCam");

        var pos = ovr.transform.position;
        var angles = ovr.transform.eulerAngles;
        var rotation = Quaternion.Euler(-angles.x, -angles.y, angles.z);



        for (int i = 0; i < previousTranslation.Length - 1; i++)
        {
            previousTranslation[i] = previousTranslation[i + 1];
        }
        previousTranslation[previousTranslation.Length - 1] = pos;



        for (int i = 0; i < previouRotation.Length - 1; i++)
        {
            previouRotation[i] = previouRotation[i + 1];
        }
        previouRotation[previouRotation.Length - 1] = rotation;


        for (int i = 0; i < previousTranslation.Length - 1; i++)
        {
            if (Vector3.Distance(previousTranslation[i], previousTranslation[i + 1]) >= noMovementThreshold || Quaternion.Angle(previouRotation[i], previouRotation[i + 1]) >= noRotationThreshold)
            {
                //The minimum movement has been detected between frames
                headIsMoving = true;
                //print( "Head moved");
                break;
            }
            else
            {
                headIsMoving = false;
            }
        }

        return headIsMoving;

    }

    private void EnableCamera(int n, int m)
    {
        Cameras.ForEach(cam => cam.enabled = false);
        Cameras[n].enabled = true;
        if (m >= 0)
            Cameras[m].enabled = true;
    }

}

