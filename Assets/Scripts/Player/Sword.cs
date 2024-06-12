using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject Slash_Prefab;
    [SerializeField] private Transform Slash_Animation;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float attackCooldown = 1.0f; // Thời gian hồi chiêu tính bằng giây

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
        playerControl.Combat.Attack.started += _ => TryAttack();
    }

    private void Update()
    {
        MouseFollow();
    }

    private void TryAttack()
    {
        if (canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

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

            if (player.FacingLeft)
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void MouseFollow()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            weapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
