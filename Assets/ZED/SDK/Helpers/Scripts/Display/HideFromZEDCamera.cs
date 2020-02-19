//======= Copyright (c) Stereolabs Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Displays the point cloud of the real world in front of the camera.
/// Can be attached to any GameObject in a scene, but requires a ZEDManager component to exist somewhere. 
/// </summary>
public class HideFromZEDCamera : MonoBehaviour
{
    /// <summary>
    /// Set to a camera if you do not want that camera to see the point cloud. 
    /// </summary>
    [Tooltip("Set to a camera if you do not want that camera to see the point cloud. ")]

    public List<Camera> hiddenObjectFromCamera;


    /// <summary>
    /// Whether the point cloud should be visible or not. 
    /// </summary>
    [Tooltip("Whether the point cloud should be visible or not. ")]
    public bool display = true;



    void OnRenderObject()
    {

            foreach(Camera cam in hiddenObjectFromCamera)
                if (cam == Camera.current) return;
            if (!display) return; //Don't draw anything if the user doesn't want to. 


    }

}
