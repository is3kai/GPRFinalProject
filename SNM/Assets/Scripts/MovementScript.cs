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

    private CircleCollider2D _hb;

    [SerializeField]
    private float rotationSpeed;

    public float speed = 10f;

    [HideInInspector]
    public bool isFacingLeft;
    [HideInInspector]
    public bool isFacingDown;
    [HideInInspector]
    public bool isFacingUp;
    [HideInInspector]
    public bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    virtual protected void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hb = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure the cursor is at the same z-coordinate as the character

        // Calculate the direction from the character to the cursor
        Vector3 direction = (mousePos - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the character to face the cursor
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
