using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1f;

    private PlayerControl control;
    private Vector2 movement;
    private Rigidbody2D body;

    private void Awake()
    {
        control = new PlayerControl();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput()
    {
        movement = control.Movement.Move.ReadValue<Vector2>();
    }

    private void Move() { 
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);    
    }
}
