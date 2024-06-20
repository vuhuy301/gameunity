using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform weaponCollider;
    private bool facingLeft = false;
    public static Player Instance;
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    private PlayerControl control;
    private Vector2 movement;
    private Rigidbody2D body;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {

        Instance = this;
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

    public Transform GetWeaponCollider() { return weaponCollider; } 

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
        Vector2 mouse = Input.mousePosition;
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mouse.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
