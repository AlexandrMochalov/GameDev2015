using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardGame : MonoBehaviour {
    public List<PlaySlot> PlaySlots = new List<PlaySlot>();
    public List<FinalSlot> FinalSlots = new List<FinalSlot>();
    public Transform CardsParent;
    public List<Card> Cards = new List<Card>();

    void Start() {
        foreach (var card in CardsParent.GetComponentsInChildren<Card>()) {
            Cards.Add(card);
        }
        Cards.Sort((a, b) => Random.RandomRange(0, 100) < 50 ? -1 : 1);

        int index = 0;

        for (int i = 0; i < PlaySlots.Count; i++) {
            var slot = PlaySlots[i];
            for (int c = 0; c <= i; c++) {
                slot.AddCard(Cards[index], 0.125f * (float)c);
                index++;
            }
        }
    }
}
