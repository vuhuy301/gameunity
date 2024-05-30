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

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        control = new PlayerControl();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        PlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = control.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }

    private void Move() { 
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);    
    }

    private void PlayerFacingDirection() {
        Vector3 mouse = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mouse.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
