using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject Slash_Prefab;
    [SerializeField] private Transform Slash_Animation;
    [SerializeField] private float attackCooldown = 1.0f; // Thời gian hồi chiêu tính bằng giây

    private Transform weaponCollider;
    private PlayerControl playerControl;
    private Animator animator;
    private Player player;
    private ActiveWeapon weapon;
    private GameObject slashAnim;
    private bool canAttack = true;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        weapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void Start()
    {
        weaponCollider = Player.Instance.GetWeaponCollider();

        playerControl.Combat.Attack.started += _ => TryAttack();

        Slash_Animation = GameObject.Find("Slash").transform;

    }

    private void Update()
    {
        MouseFollow();
    }

    public void TryAttack()
    {
        if (canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
        }
        

        slashAnim = Instantiate(Slash_Prefab, Slash_Animation.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void DoneAttackAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingDownAnim()
    {
        if (slashAnim != null)
        {
            slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Player.Instance.FacingLeft)
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void MouseFollow()
    {
        
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    public float GetAttackCooldown()
    {
        return attackCooldown;
    }
}