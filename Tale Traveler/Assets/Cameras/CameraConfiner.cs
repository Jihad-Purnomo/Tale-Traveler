using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{
    private Collider2D coll;
    private CameraLogic camLogic;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        camLogic = GetComponentInParent<CameraLogic>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        confiner = GetComponentInChildren<CinemachineConfiner2D>();
    }

    private void Start()
    {
        confiner.m_BoundingShape2D = coll;
    }

    private void Update()
    {
        virtualCamera.enabled = camLogic.movingObject.Object.col.IsTouching(coll);

        if (virtualCamera.enabled)
        {
            virtualCamera.Follow = camLogic.toFollow;
        }
    }
}
