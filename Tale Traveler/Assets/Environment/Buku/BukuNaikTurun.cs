using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BukuNaikTurun : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;

    private float moveSpeed;
    private float moveRange;
    private float lerp;

    private Vector3 originalPos;
    private Vector3 targetPos;

    private void Start()
    {
        moveSpeed = transform.localScale.x * speed;
        moveRange = transform.localScale.x * range;

        originalPos = transform.position;
        targetPos = new Vector3(originalPos.x, originalPos.y + moveRange);
    }

    private void Update()
    {
        lerp = Mathf.PingPong(Time.time * moveSpeed, 1);
        transform.position = Vector2.Lerp(originalPos, targetPos, lerp);
    }
}
