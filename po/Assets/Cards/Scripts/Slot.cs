using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    protected List<Drag> stack = new List<Drag>();
    
    public void ShowStack ()
    {
        string s = "";
        foreach (var card in stack)
            s = s + " " + card.GetComponent<Card>().Value;

        Debug.LogError(s);
    }

    public virtual bool CanReceiveDrop(Drag dragObject) {
        return false;
    }

    protected Card GetLastCard() {
        if (stack.Count == 0) return null;

        return stack[stack.Count - 1].GetComponent<Card>();
    }

    public virtual void Init() {
        var coll = GetComponent<Collider2D>();
        if (coll == null)
            coll = gameObject.AddComponent<BoxCollider2D>();

        coll.isTrigger = true;
    }

    public virtual void AddCard (Card card, float delay = 0f) {}

    public void OnDrop(Drag dragObject) {
        var pos = dragObject.transform.position;
        
        dragObject.GetComponent<SpriteRenderer>().sortingOrder = stack.Count;
    }

    public void RemoveCard(Drag dragObject)
    {
        stack.Remove(dragObject);
    }

    public List<Card> GetNextCardList(Card card) {
        List<Card> list = new List<Card>();
        if (stack.Contains(card))
        {
            int index = stack.IndexOf(card);
            for (int i = index + 1; i < stack.Count; i++)
                list.Add(stack[i].GetComponent<Card>());
        }
        return list;
    }

    void Start() {
        Init();
    }
}
