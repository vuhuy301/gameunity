using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private float range = 10f;// Thời gian chờ giữa các lần bắn

    private Transform weaponCollider;
    private Animator myAnimator;
    private PlayerControl playerControl;
    private ActiveWeapon weapon;
    private bool canAttack = true;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
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
    }

    private void Update()
    {
        MouseFollow();
    }

    private void TryAttack()
    {
        if (this == null) return;

        if (canAttack && myAnimator != null)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        if(myAnimator != null)
        {
            myAnimator.SetTrigger(FIRE_HASH);
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateProjectTitleRange(range);
        }
        
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void MouseFollow()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 playerPosition = Player.Instance.transform.position;

        Vector2 direction = mouseWorldPosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mouseWorldPosition.x < playerPosition.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
