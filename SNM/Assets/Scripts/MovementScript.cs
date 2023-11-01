using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementInputs : MonoBehaviour
{

    private float _xMove;

    private float _yMove;

    private Rigidbody2D _rb;

    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    void CheckInput()
    {
        _xMove = Input.GetAxis("Horizontal");
        _yMove = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector2(_xMove, _yMove) * speed);
    }
}
