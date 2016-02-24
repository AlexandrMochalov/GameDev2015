using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drag : MonoBehaviour {
    public Vector3 Offset = Vector3.zero;
    
    bool isMouseDown = false;
    bool isDrag = false;
    Vector3 startPosition;
    string sortindLayerName;
    public Slot currentSlot;

    List<Slot> slots = new List<Slot> ();

    public virtual void Init()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            var rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
        startPosition = transform.position;
        sortindLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
    }

    void Start() {
        Init ();
    }

    public virtual bool IsCanStartDrag() {
        return true; 
    }

    void OnMouseDown() {
        if (!IsCanStartDrag()) return;
        isMouseDown = true;
        GetComponent<SpriteRenderer>().sortingLayerName = "Drag";
        startPosition = transform.position;
    }

    public virtual void OnDrop (Slot slot) {
    }

    public virtual void OnDragUpdate(Vector3 delta) {
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
            OnDrop(nearest);
    }
    }

    void OnCancalDragMoveEnd() {
        
        GetComponent<SpriteRenderer>().sortingLayerName = sortindLayerName;
        //GetComponent<SpriteRenderer>().sortingOrder = 1;
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
        var newPosition = Offset + new Vector3(worldPosition.x
            , worldPosition.y
            , transform.position.z);

        var delta = newPosition - transform.position;
        transform.position = newPosition;
        
        OnDragUpdate(delta);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!isDrag) return;
        var slot = other.GetComponent<Slot>();
        if (slot != null && slot.CanReceiveDrop(this))
            slots.Add(slot);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!isDrag) return;
        var slot = other.GetComponent<Slot>();
        if (slot != null && slots.Contains(slot))
            slots.Remove(slot);
    }
}
