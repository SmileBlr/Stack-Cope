using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    private static GameObject _camera;
    private static Vector3 _up;
    private static GameObject _cm;

    void Awake()
    {
        _camera = GameObject.Find("Main Camera");
        _up  = new Vector3(0, 0.2f, 0);
        _cm = GameObject.Find("CM");
    }
    internal void UpCamera(Transform Look)
    {
        _camera.transform.position += _up;
        _cm.GetComponent<CinemachineVirtualCamera>().Follow = Look;
        _cm.GetComponent<CinemachineVirtualCamera>().LookAt = Look;
    }
}
