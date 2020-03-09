using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveController : MonoBehaviour, IPointerClickHandler
{
	private int _click;
    private JointController _destroyingLine;
    [SerializeField] private Type _type;

    public enum Type {Rectangle, Line};

    //Destroy the gameObject and all links on its in joined gameObjects
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _click = eventData.clickCount;

        if (_click != 2)
            return;

        Remove();
    }

    private void Remove()
    {           
        if (_type == Type.Rectangle)
        {
            var listLines = GetComponent<RectangleInfo>().listLines;
            while (listLines.Count > 0)
            {
                _destroyingLine = listLines[0];
                LineDestroy(_destroyingLine.startRectangle.GetComponent<RectangleInfo>().listLines, _destroyingLine);
                LineDestroy(_destroyingLine.finishRectangle.GetComponent<RectangleInfo>().listLines, _destroyingLine);
            }
            Destroy(gameObject);
        }
        else
        {
               _destroyingLine = GetComponent<JointController>();
               LineDestroy(_destroyingLine.startRectangle.GetComponent<RectangleInfo>().listLines, _destroyingLine);
               LineDestroy(_destroyingLine.finishRectangle.GetComponent<RectangleInfo>().listLines, _destroyingLine);
               Destroy(gameObject);
        }
    }

    private void LineDestroy(List<JointController> currentListLines, JointController destroyingLine)
    {
        currentListLines.Remove(destroyingLine);
        Destroy(destroyingLine.gameObject);
    }
}