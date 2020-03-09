using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour, IDragHandler
{
    private float _halfHeightRectangle = 0.5f;
    private float _halfWidthRectangle = 1f;
    private LayerMask _maskImmobilizedRectangle;
    private int _layerImmobilizedRectangle;
    private int _layerDraggingRectangle;
    private List<JointController> _listLines;

    private void Start()
    {
        _maskImmobilizedRectangle = LayerMask.GetMask("ImmobilizedRectangle");
        _layerImmobilizedRectangle = LayerMask.NameToLayer("ImmobilizedRectangle");
        _layerDraggingRectangle = LayerMask.NameToLayer("DraggingRectangle");
        _listLines = GetComponent<RectangleInfo>().listLines;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging && eventData.button == PointerEventData.InputButton.Left)
        {
            gameObject.layer = _layerDraggingRectangle;

            Vector2 mouse = Input.mousePosition;
            Vector2 mouseGlob = Camera.main.ScreenToWorldPoint(mouse);

            Vector2 topLeftCorner = new Vector2(mouseGlob.x - _halfWidthRectangle,
                mouseGlob.y + _halfHeightRectangle);
            Vector2 bottomRightCorner = new Vector2(mouseGlob.x + _halfWidthRectangle,
                mouseGlob.y - _halfHeightRectangle);

            Collider2D obstructionCollider = Physics2D.OverlapArea(topLeftCorner, bottomRightCorner, _maskImmobilizedRectangle);
            if (obstructionCollider == null)
            {
                transform.position = mouseGlob;
            }

            //If selected Rectangle has joined line, then changing the line when Rectangle moves
            if (_listLines != null)
            {
                foreach (var line in _listLines)
                {
                    line.LineUpdate();
                }
            }
        }
    }

    private void OnMouseUp()
    {
        gameObject.layer = _layerImmobilizedRectangle;
    }
}
