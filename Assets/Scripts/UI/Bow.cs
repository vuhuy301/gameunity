using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Transform weaponCollider;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;
    private PlayerControl playerControl;
    private Player player;
    private ActiveWeapon weapon;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        weapon = GetComponentInParent<ActiveWeapon>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void Start()
    {
        weaponCollider = Player.Instance.GetWeaponCollider();

        playerControl.Combat.Attack.started += _ => Attack();


    }

    private void Update()
    {
        MouseFollow();
    }
    public void Attack()
    {
        Debug.Log("abc");
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);

    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollow()
    {
        // Lấy vị trí chuột trong không gian thế giới
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;  // Đảm bảo vị trí z là 0

        // Lấy vị trí của người chơi trong không gian thế giới
        Vector3 playerPosition = Player.Instance.transform.position;

        // Tính toán góc giữa vị trí người chơi và vị trí chuột
        Vector2 direction = mouseWorldPosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Xác định hướng và cập nhật góc quay của vũ khí
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



}
