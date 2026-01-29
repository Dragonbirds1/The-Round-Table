using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static List<itemData> items = new();
    private bool _isOpen;
    public GameObject inventory;

    public void OpenInventory()
    {
        _isOpen = true;

        inventory.SetActive(true);
        Time.timeScale = 0;
        
    }
    public void CloseInventory()
    {
        _isOpen &= false;
       Time.timeScale  =  1;
        inventory.SetActive(false);
    }
    public void ToggleInventory(InputAction.CallbackContext ctx)
    {
      _isOpen = !_isOpen;

        inventory.SetActive(!_isOpen);
        Time.timeScale = _isOpen ? 0 : 1;
    }
}
