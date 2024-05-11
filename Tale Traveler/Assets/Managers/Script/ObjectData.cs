using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public Rigidbody2D Rb {  get; private set; }
    public Collider2D col { get; private set; }

    [field:SerializeField]public MovementData Data { get; private set; }

    //public Vector2 groundCheckPos {  get; private set; }
    public Vector2 groundCheckSize { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    
    void Start()
    {
        //groundCheckPos = new Vector2(col.bounds.center.x, col.bounds.min.y);
        groundCheckSize = new Vector2(col.bounds.size.x * Data.groundCheckSize.x, Data.groundCheckSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
