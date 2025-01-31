using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OLDINVENTORY : MonoBehaviour
{
    [System.Serializable]
    public class Inventory
    {
        public GameObject item;
        public int amount;
        public Types type;

        public Inventory(GameObject _item, int _amount, Types _type)
        {
            item = _item;
            amount = _amount;
            type = _type;
        }
    }

    [Header("Inventory")]
    [SerializeField] private int inventorySlots;
    [SerializeField] private int slotSelected;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject slotHighlight;
    [SerializeField] private Image itemImage;

    [SerializeField] private List<Inventory> inventoryList = new List<Inventory>();
    [SerializeField] private GameObject testOBJ;

    private void Start()
    {
        InitInventory();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) AddItem(testOBJ, 10, Types.Stone);

        ChangeSlotSelected();
    }

    private void InitInventory()
    {
        for (int i = 1; i <= inventorySlots; i++)
        {
            GameObject _slot = Instantiate(inventorySlotPrefab);

            _slot.name = i.ToString();
            _slot.transform.SetParent(inventoryUI.transform, false);

            _slot.transform.Find("Image").gameObject.SetActive(false);
            _slot.transform.Find("Amount").gameObject.SetActive(false);
        }
    }

    private void ChangeSlotSelected()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 1; i <= inventorySlots; i++)
            {
                if (Input.GetKeyDown("" + i))
                {
                    slotHighlight.SetActive(true);

                    slotHighlight.transform.SetParent(inventoryUI.transform.Find(i.ToString()), false);
                    slotHighlight.transform.position = inventoryUI.transform.Find(i.ToString()).position;
                    slotHighlight.transform.SetAsFirstSibling();

                    slotSelected = i;
                }
            }
        }
    }

    private GameObject SelectedInventoryEmpty(GameObject _itemToVerify)
    {
        GameObject _slot = null;

        for (int i = 0; i < inventorySlots; i++)
        {
            GameObject _inventorySlot = inventoryUI.transform.GetChild(i).gameObject;
            var _onList = inventoryList.Find(_list => _list.item.name == _itemToVerify.name);

            if (_onList != null)
            {
                _slot = _inventorySlot;
                Debug.Log("No inventario");
                break;
            }
            else
            {
                _slot = _inventorySlot;
                Debug.Log("Nao esta no inventario");
            }
        }

        return _slot;
    }

    public void AddItem(GameObject _obj, int _amount, Types _type)
    {
        GameObject _slot = SelectedInventoryEmpty(_obj);

        if (_slot == null)
        {
            Debug.Log("Inventario cheio");
            return;
        }

        Image _slotImage = _slot.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI _slotText = _slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

        _slotImage.gameObject.SetActive(true);
        _slotText.gameObject.SetActive(true);

        var _onList = inventoryList.Find(_list => _list.item == _obj);
        if (_onList != null)
        {
            _onList.amount += _amount;
            _slotText.text = _onList.amount.ToString();

            Debug.Log("Item stackado");
        }
        else
        {
            _slot.GetComponent<InventorySlot>().haveItem = true;

            _slotText.text = _amount.ToString();
            _slotImage.sprite = _obj.GetComponent<SpriteRenderer>().sprite;

            inventoryList.Add(new Inventory(_obj, _amount, _type));

            Debug.Log("Item adicionado");
        }
    }














    private GameObject NAOPERDER()
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
}