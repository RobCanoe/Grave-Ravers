﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidbody;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
