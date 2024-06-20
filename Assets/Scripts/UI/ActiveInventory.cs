using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControl playerControl;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    private void Start()
    {
        playerControl.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHightLight(numValue - 1);
    }

    private void ToggleActiveHightLight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        // Nếu có vũ khí hiện tại, phá hủy nó
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        // Kiểm tra xem ô chứa vũ khí mới có hợp lệ không
        InventorySlot slot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>();
        if (slot.GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        // Lấy prefab của vũ khí mới và tạo nó trong cảnh
        GameObject weaponToSpawn = slot.GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        // Đặt lại vị trí và gán vũ khí mới vào đối tượng ActiveWeapon
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
