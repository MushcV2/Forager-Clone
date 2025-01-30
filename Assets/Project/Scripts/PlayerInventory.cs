using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory
{
    public GameObject item;
    public int amount;

    public Inventory(GameObject _item, int _amout)
    {
        item = _item;
        amount = _amout;
    }
}

public class PlayerInventory : MonoBehaviour
{

    [Header("Inventory")]
    [SerializeField] private int inventorySlots;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Image itemImage;

    [SerializeField] private List<Inventory> inventoryList = new List<Inventory>();

    private void Start()
    {
        InitInventory();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) AddItem(inventoryUI, 10);
    }

    private void InitInventory()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            GameObject _slot = Instantiate(inventorySlotPrefab);

            _slot.name = $"Slot: {i}";
            _slot.transform.SetParent(inventoryUI.transform, false);

            _slot.transform.Find("Image").gameObject.SetActive(false);
            _slot.transform.Find("Amount").gameObject.SetActive(false);
        }
    }

    private GameObject SelectedInventoryEmpty()
    {
        GameObject _selectedSlot = null;

        for (int i = 0; i < inventoryUI.transform.childCount; i++)
        {
            GameObject _slot = inventoryUI.transform.GetChild(i).gameObject;
            if (_slot.GetComponent<InventorySlot>().haveItem) continue;
            else
            {
                _selectedSlot = _slot;
                break;
            }
        }

        return _selectedSlot;
    }

    public void AddItem(GameObject _obj, int _amount)
    {
        GameObject _slot = SelectedInventoryEmpty();

        if (_slot == null)
        {
            Debug.Log("Inventario lotado");
            return;
        }

        inventoryList.Add(new Inventory(_obj, _amount));
        _slot.GetComponent<InventorySlot>().haveItem = true;
    }
}