using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class Inventory
    {
        public GameObject slot;
        public GameObject item;
        public int amount;

        public Inventory(GameObject _slot, GameObject _item, int _amount)
        {
            slot = _slot;
            item = _item;
            amount = _amount;
        }
    }

    [Header("Inventory")]
    [SerializeField] private List<Inventory> inventory = new List<Inventory>();
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject highLight;
    [SerializeField] private int selectedSlot;
    [SerializeField] private int inventorySize;

    [Header("PopUp")]
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject uiObject;

    public GameObject testOBJ;

    public void Start()
    {
        InitInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) AddItem(testOBJ, 1);

        if (Input.anyKeyDown) HighLightSlot();
    }

    private void InitInventory()
    {
        for (int i = 1; i <= inventorySize; i++)
        {
            GameObject _slot = Instantiate(slotPrefab);

            _slot.name = i.ToString();
            _slot.transform.Find("Image").gameObject.SetActive(false);
            _slot.transform.Find("Amount").gameObject.SetActive(false);
            _slot.transform.SetParent(inventoryUI.transform, false);

            inventory.Add(new Inventory(_slot, null, 0));
        }
    }

    private void HighLightSlot()
    {
        for (int i = 1; i <= inventorySize; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                highLight.SetActive(true);

                highLight.transform.SetParent(inventoryUI.transform.Find(i.ToString()), false);
                highLight.transform.SetAsFirstSibling();
                highLight.transform.position = inventoryUI.transform.Find(i.ToString()).position;

                selectedSlot = i;
            }
        }
    }

    private void CreatePopUp(GameObject _item , int _amount)
    {
        GameObject _popUp = Instantiate(popUpPrefab);

        _popUp.transform.SetParent(uiObject.transform, false);
        _popUp.GetComponent<TextMeshProUGUI>().text = $"+{_amount} {_item.name}";
        Destroy(_popUp, 1.5f);
    }

    private int SelectSlot(GameObject _item)
    {
        int _slot = -1;

        foreach (var _element in inventory)
        {
            // Verify if slot dont is null and dont have item
            if (_element.slot != null && _element.item == null)
            {
                // If is true, return the slot index
                _slot = int.Parse(_element.slot.name) - 1;
                break;
            }
            // If slot have item
            else if (_element.slot != null && _element.item != null)
            {
                // Verify if the item in slot is the same item who want to add
                if (_item == _element.item)
                {
                    // If is true, return the slot index
                    _slot = int.Parse(_element.slot.name) - 1;

                    break;
                }
            }
        }

        return _slot;
    }

    public void AddItem(GameObject _item, int _amount)
    {
        int _slotIndex = SelectSlot(_item);
        if (_slotIndex > -1)
        {
            CreatePopUp(_item, _amount);

            GameObject _slot = inventory[_slotIndex].slot;
            Image _slotImage = _slot.transform.Find("Image").GetComponent<Image>();
            TextMeshProUGUI _slotAmount = _slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

            inventory[_slotIndex].item = _item;
            inventory[_slotIndex].amount += _amount;

            _slotImage.gameObject.SetActive(true);
            _slotImage.sprite = inventory[_slotIndex].item.GetComponent<SpriteRenderer>().sprite;
            _slotImage.color = inventory[_slotIndex].item.GetComponent<SpriteRenderer>().color;

            _slotAmount.gameObject.SetActive(true);
            _slotAmount.text = inventory[_slotIndex].amount.ToString();
        }
    }
}