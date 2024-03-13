using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    
    public string ItemName;
    public string GetItemName()
    {
        bool isDoorOpen = SelectionManager.Instance.DoorInteractionOpen();
        bool isDoorClose = SelectionManager.Instance.DoorInteractionClose();
        
        if (ItemName == "open" && isDoorOpen)
        {
            ItemName = "locked";
        }
        else if(ItemName == "locked" && !isDoorClose)
        {
            ItemName = "open";
        }
        return ItemName;
    }
}
