using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int selectedSlot;
    public int numberOfSlots;

    public AbstractItem[] slots;

    public Inventory(int numberOfSlots) 
    {
        this.numberOfSlots = numberOfSlots;
        this.slots = new AbstractItem[numberOfSlots];

        this.selectedSlot = 0;
    }

    public int getNumberOfItems() 
    {
        int count = 0;

        for (int i = 0; i < numberOfSlots; i++) 
        {
            if (slots[i] != null) { count++; }
        }

        return count;
    }

    public void useSelectedItem() 
    {
        slots[selectedSlot].Use();
    }

    public bool addItem(AbstractItem item) 
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (slots[i] == null) { slots[i] = item; return true; }
        }

        return false;
    }

    public void deleteItem(int id) 
    {
        slots[id] = null;
    }

    public void selectSlot(int id) 
    {
        selectedSlot = id;
    }
    
}
