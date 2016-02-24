using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : Drag {
    public enum CardType
    {
        Heatrs,
        Diamonds,
        Clovers,
        Pikes
    }

    public enum CardColor
    {
        Red,
        Black
    }

    public CardType Type = CardType.Heatrs;
    public CardColor ColorType = CardColor.Red;
    public int Value;
    public Sprite BackSprite;

    private Sprite _originalSprite;
    private List<Card> cardList = new List<Card>();

    public override void OnDrop(Slot slot)
    {
        slot.AddCard(this);
    }



    public override bool IsCanStartDrag()
    {
        cardList.Clear();
        if (currentSlot != null) {
            var playSlot = currentSlot.GetComponent<PlaySlot>();

            if (playSlot != null)
            {
                cardList = playSlot.GetNextCardList(this);
                if (cardList.Count == 0) return true;

                currentSlot.ShowStack();
                var prew = this;
                foreach (var nextCard in cardList) {
                    if (prew.ColorType == nextCard.ColorType || prew.Value != nextCard.Value + 1)
                    {
                        return false;
                    }
                    prew = nextCard;
                }

                return true;    
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    public override void OnDragUpdate(Vector3 delta)
    {
        foreach (var card in cardList) {
            card.transform.position += delta;
        }
    }

    public override void Init()
    {
        base.Init();
        bool isRed = Type == CardType.Diamonds || Type == CardType.Heatrs;
        ColorType = isRed ? CardColor.Red : CardColor.Black;
    }
}
