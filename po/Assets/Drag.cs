using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drag : MonoBehaviour {
    public enum CardType
    {
        Heatrs,
        Diamonds,
        Clovers,
        Pikes
    }

    public enum CardColor {
        Red,
        Black
    }
    
    public Vector3 Offset = Vector3.zero;
    public CardType Type = CardType.Heatrs;
    public int Value;
    public CardColor ColorType = CardColor.Red;

    bool isMouseDown = false;
    bool isDrag = false;
    Vector3 startPosition;
    string sortindLayerName;

    List<Slot> slots = new List<Slot> ();
    Slot currentSlot;

    void Start() {
        startPosition = transform.position;
        ColorType = (Type == CardType.Diamonds || Type == CardType.Heatrs) ? CardColor.Red : CardColor.Black;
        sortindLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
    }

    void OnMouseDown() {
        isMouseDown = true;
        GetComponent<SpriteRenderer>().sortingLayerName = "Drag";
    }

    void OnMouseUp() {
       
        if (!isDrag) return;
        isMouseDown = false;
        isDrag = false;

        if (slots.Count == 0)
        {
            iTween.MoveTo(gameObject
                , iTween.Hash("position", startPosition, "time", 0.5f));

            Invoke("OnCancalDragMoveEnd", 0.5f);
        }
        else
        {
            Slot nearest = slots[0];
            float distance = GetDistanceForSlot(nearest);
            foreach (var slot in slots)
            {
                var newDistance = GetDistanceForSlot(slot);
                if (newDistance < distance) {
                    nearest = slot;
                    distance = newDistance;
                }
            }
            nearest.OnDrop(this);
            currentSlot = nearest;

            var pos = nearest.transform.position;
            iTween.MoveTo(gameObject
                , iTween.Hash("position", new Vector3 (pos.x, pos.y, transform.position.z)));
            GetComponent<SpriteRenderer>().sortingLayerName = sortindLayerName;
        }
    }

    void OnCancalDragMoveEnd() {
        GetComponent<SpriteRenderer>().sortingLayerName = sortindLayerName;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    float GetDistanceForSlot (Slot slot)
    {
        var ourCenter = GetComponent<Collider2D>().bounds.center;
        var otherCenter = slot.GetComponent<Collider2D>().bounds.center;
        return Mathf.Abs((ourCenter - otherCenter).magnitude);
    }

    void OnMouseDrag() {
        if (!isMouseDown) return;
        if (!isDrag) {
            if (currentSlot != null)
                currentSlot.RemoveCard(this);
            currentSlot = null;
            isDrag = true;
        }

        var mousePos = Input.mousePosition;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        
        transform.position = Offset + new Vector3 (worldPosition.x
            , worldPosition.y
            , transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var slot = other.GetComponent<Slot>();
        if (slot != null && slot.CanReceiveDrop(this))
            slots.Add(slot);
    }

    void OnTriggerExit2D(Collider2D other) {
        var slot = other.GetComponent<Slot>();
        if (slot != null && slots.Contains(slot))
            slots.Remove(slot);
    }
}
