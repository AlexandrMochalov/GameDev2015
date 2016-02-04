using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    List<Drag> stack = new List<Drag>();

    bool IsTypeCopability(Drag.CardColor first, Drag.CardColor second)
    {
        return first != second;
    }

    public bool CanReceiveDrop(Drag dragObject)
    {
        return (stack.Count == 0)
            || (IsTypeCopability(stack[stack.Count-1].ColorType, dragObject.ColorType));
    }
    
    public void OnDrop(Drag dragObject) {
        var pos = dragObject.transform.position;
        if (stack.Count > 0)
            stack[stack.Count - 1].GetComponent<Collider2D>().enabled = false;

        stack.Add(dragObject);
        
        dragObject.GetComponent<SpriteRenderer>().sortingOrder = stack.Count;
        Debug.LogError("Add " + stack.Count);
    }

    public void RemoveCard(Drag dragObject)
    {
        stack.Remove(dragObject);
        if (stack.Count > 0)
            stack[stack.Count - 1].GetComponent<Collider2D>().enabled = true;
        Debug.LogError("Remove " + stack.Count);
    }
}
