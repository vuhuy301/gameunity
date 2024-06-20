using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    public PlayerControl PlayerControl { get; private set; }
    private float timeBetweenAttacks;

    public bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        PlayerControl = new PlayerControl();    

    }

    private void OnEnable()
    {
        PlayerControl.Enable();
    }

    private void Start()
    {
        PlayerControl.Combat.Attack.started += _ => StartAttacking();
        PlayerControl.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        if (newWeapon is Sword sword)
        {
            timeBetweenAttacks = sword.GetAttackCooldown();
        }
    }


    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
        // Thêm các xử lý cần thiết khi không có vũ khí
    }


    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            if (attackButtonDown && !isAttacking && CurrentActiveWeapon != null)
            {
                StartCoroutine(AttackCooldownRoutine());
                if (CurrentActiveWeapon is Sword sword)
                {
                    sword.TryAttack();
                }
                
            }
        }
    }
    private IEnumerator AttackCooldownRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

}
