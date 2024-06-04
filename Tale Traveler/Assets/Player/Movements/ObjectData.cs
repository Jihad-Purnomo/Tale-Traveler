using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public Rigidbody2D Rb {  get; private set; }
    public Collider2D col { get; private set; }

    [field:SerializeField] public ObjectSound runningSound { get; private set; }
    [field:SerializeField] public ObjectSound jumpingSound { get; private set; }
    [field: SerializeField] public ObjectSound landingSound { get; private set; }

    [field:SerializeField]public MovementData Data { get; private set; }
    public ContactFilter2D groundFilter { get; private set; }
    public Vector2 groundCheckSize { get; private set; }

    //tambahan kode dari tutorial youtube
    private float forceAmount;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        groundCheckSize = new Vector2(col.bounds.size.x * Data.groundCheckSize.x, Data.groundCheckSize.y);
        //tambah kode dibawah buat stiker bisa ngambang di air
        //Rb.velocity = Vector3.down * forceAmount;
    }
}
