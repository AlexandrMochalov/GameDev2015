using UnityEngine;
using System.Collections;

public class FinalSlot : Slot {
    public override bool CanReceiveDrop(Drag dragObject)
    {
        var newCard = dragObject.GetComponent<Card>();

        if (stack.Count == 0)
        {
            return newCard.Value == 1;
        }
        else
        {
            var lastCard = GetLastCard();
            return newCard.Type == lastCard.Type
                && newCard.Value == lastCard.Value + 1;
        }
    }

    public bool IsFull() {
        return stack.Count == 13;
    }
}
