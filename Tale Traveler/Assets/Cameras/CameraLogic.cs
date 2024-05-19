using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private CinemachineVirtualCamera[] CameraList;
    public Movement movingObject { get; private set; }
    public Transform toFollow { get; private set; }

    private void Awake()
    {
        CameraList = GetComponentsInChildren<CinemachineVirtualCamera>();
        movingObject = FindObjectOfType<Movement>();
    }

    private void Start()
    {
        for (int i = 0;  i < CameraList.Length; i++)
        {
            CameraList[i].Priority = i;
        }

        SetFollow(movingObject.transform);
    }

    public void SetFollow(Transform cameraFollow)
    {
        toFollow = cameraFollow;
    }
}
