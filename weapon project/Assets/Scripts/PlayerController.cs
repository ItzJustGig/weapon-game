using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed = 5f; 
    public float acceleration = 10f;
    public float deceleration = 5f;

    private Rigidbody2D rb;
    private Vector2 targetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY).normalized;
        targetVelocity = inputVector * movSpeed;
    }

    void FixedUpdate()
    {
        // Acelaração
        if (targetVelocity.magnitude > 0.1f) 
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else // Desacelaração Gradual
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }
}