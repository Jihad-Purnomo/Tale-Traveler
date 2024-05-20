using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Camera;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner2D cameraConfiner;

    private void Awake()
    {
        virtualCamera = Camera.GetComponent<CinemachineVirtualCamera>();
        cameraConfiner = Camera.GetComponent<CinemachineConfiner2D>();
    }

    public void SetFollow(Transform cameraFollow)
    {
        virtualCamera.Follow = cameraFollow;
    }

    public void ResetFollow()
    {
        if(virtualCamera.Follow == null)
        {
            virtualCamera.Follow = transform;
        }    
    }

    public void SetConfiner(CompositeCollider2D area)
    {
        cameraConfiner.m_BoundingShape2D = area;
    }
}
