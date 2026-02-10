using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static List<itemData> items = new();

    public bool _isOpen = false;

    public GameObject inventory;
 
    public void OpenInventory()
    {
      
        inventory.SetActive(true);

        Time.timeScale = 0;

    }
    public void CloseInventory()
    {
        _isOpen &= false;

        Time.timeScale = 1;

        inventory.SetActive(false);
    }
    public void ToggleInventory(InputAction.CallbackContext ctx)
    {
       
        if (ctx.canceled) return;

        _isOpen = !_isOpen;

        inventory.SetActive(_isOpen);

        Time.timeScale = _isOpen ? 0 : 1;
    }
}
