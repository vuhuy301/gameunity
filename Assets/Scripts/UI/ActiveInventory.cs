using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        playerControl.Inventory.Keyboard.performed += ctx => OnKeyboardInputPerformed(ctx);
        ToggleActiveSlot(0);
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void OnKeyboardInputPerformed(InputAction.CallbackContext context)
    {
        int slotIndex = (int)context.ReadValue<float>() - 1; 
        ToggleActiveSlot(slotIndex);
    }

    private void ToggleActiveSlot(int indexNum)
    {
        if (indexNum < 0 || indexNum >= transform.childCount)
        {
            Debug.LogWarning("Invalid slot index.");
            return;
        }

        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance == null)
        {
            Debug.LogWarning("ActiveWeapon.Instance is null.");
            return;
        }


        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }


        InventorySlot slot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>();
        if (slot == null || slot.GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }


        GameObject weaponToSpawn = slot.GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);


        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
