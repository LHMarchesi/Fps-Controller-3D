using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryUiParent;
    [SerializeField] private Sprite emptySlotSprite;
    private Color selectedColor = Color.yellow; // Highlight color
    private Color normalColor = Color.white;    // Normal color
    private List<Item> items = new List<Item>();
    private List<Image> uiSlots = new List<Image>();
    private int maxItems = 3;
    private int selectedIndex = 0; // Track the selected item index

    private void Start()
    {
        // Initialize inventory UI
        for (int i = 0; i < maxItems; i++)
        {
            GameObject slotObj = new GameObject("Slot" + i);
            slotObj.transform.parent = inventoryUiParent;

            Image slotImage = slotObj.AddComponent<Image>();
            slotImage.sprite = emptySlotSprite;

            uiSlots.Add(slotImage);
        }

        // Update UI to show initial state
        UpdateInventoryUi();
    }

    private void Update()
    {
        // Handle scrolling input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f)
        {
            // Scroll up (next item)
            SelectNextItem();
        }
        else if (scrollInput < 0f)
        {
            // Scroll down (previous item)
            SelectPreviousItem();
        }
    }

    public void AddItem(Item item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            displayItems();
            UpdateInventoryUi();
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryUi();
        }
    }

    private void UpdateInventoryUi()
    {
        for (int i = 0; i < uiSlots.Count; i++)
        {
            if (i < items.Count)
            {
                uiSlots[i].sprite = items[i].ItemIcon;
            }
            else
            {
                uiSlots[i].sprite = emptySlotSprite;
            }

            // Update the color to highlight the selected item
            uiSlots[i].color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }

    private void SelectNextItem()
    {
        // Increase the selected index, wrap around if necessary
        selectedIndex = (selectedIndex + 1) % maxItems;
        displayItems();
        UpdateInventoryUi();
    }

    private void SelectPreviousItem()
    {
        // Decrease the selected index, wrap around if necessary
        selectedIndex = (selectedIndex - 1 + maxItems) % maxItems;
        displayItems();
        UpdateInventoryUi();
    }

    private void displayItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i != selectedIndex)
            {
                items[i].hide(items[i].gameObject);
            }
            else
            {
                items[i].showItem(items[i].gameObject);
            }
        }
    }

    // Optionally, a method to get the currently selected item
    public Item GetSelectedItem()
    {
        if (items.Count > 0 && selectedIndex < items.Count)
        {
            return items[selectedIndex];
        }
        return null;
    }
}
