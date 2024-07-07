using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private int arrowCount = 10;
    private bool facingLeft = false;
    public Text arrowCountText;
    public static Player Instance;
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    private PlayerControl control;
    private Vector2 movement;
    private Rigidbody2D body;
    private Knockback knockback;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;
        control = new PlayerControl();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        UpdateArrowCountUI();
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
        if(knockback.getKnockBack) { return; }
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
     private void UpdateArrowCountUI()
    {
        if (arrowCountText != null)
        {
            arrowCountText.text = arrowCount.ToString();
        }
    }

    public int GetArrowCount()
    {
        return arrowCount;
    }

    public void DecreaseArrowCount()
    {
        arrowCount--;
        UpdateArrowCountUI();
    }
    public void IncreaseArrowCount(int amount)
    {
        arrowCount += amount;
        UpdateArrowCountUI();
    }
}
