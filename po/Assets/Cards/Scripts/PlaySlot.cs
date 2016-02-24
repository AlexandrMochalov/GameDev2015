using UnityEngine;
using System.Collections;

public class PlaySlot : Slot {
    public override bool CanReceiveDrop(Drag dragObject)
    {
        var newCard = dragObject.GetComponent<Card>();
        if (stack.Count == 0)
        {
            return newCard.Value == 13;
        }
        else
        {
            var lastCard = GetLastCard();
            return lastCard.ColorType != newCard.ColorType
                && lastCard.Value == newCard.Value + 1;
        }
    }

    public override void AddCard(Card card, float delay = 0f) {
        var y = (float)stack.Count * -0.2f;
        var z = (float)stack.Count * -0.1f;
        Vector3 pos = transform.position + new Vector3(0f, y, z);
        iTween.MoveTo(card.gameObject, iTween.Hash("position", pos, "time", 0.125f, "delay", delay));
        stack.Add(card);
        card.GetComponent<SpriteRenderer>().sortingOrder = stack.Count;
        card.currentSlot = this;
    }
}
