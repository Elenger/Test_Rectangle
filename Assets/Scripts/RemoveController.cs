using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveController : MonoBehaviour, IPointerClickHandler
{
	private int _click;
    private JointController _jointController;

    //Destroy the gameObject and all links on its in joined gameObjects
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _click = eventData.clickCount;

            if (_click == 2)
            {
                if (gameObject.TryGetComponent(out RectangleInfo rectangleInfo))
                {
                    List<GameObject> listLines = gameObject.GetComponent<RectangleInfo>().listLines;
                    while (listLines.Count>0) 
                    {
                            GameObject destroyingLine = listLines[0];

                            _jointController = destroyingLine.GetComponent<JointController>();
                            RectangleInfo currentStartRectangleInfo = _jointController.startRectangle.GetComponent<RectangleInfo>();
                            RectangleInfo currentFinishRectangleInfo = _jointController.finishRectangle.GetComponent<RectangleInfo>();
                            List<GameObject> currentStartRectangleListLines = currentStartRectangleInfo.listLines;
                            List<GameObject> currentFinishRectangleListLines = currentFinishRectangleInfo.listLines;

                            LineDestroy(destroyingLine, currentStartRectangleListLines);
                            LineDestroy(destroyingLine, currentFinishRectangleListLines);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    _jointController = gameObject.GetComponent<JointController>();
                    LineDestroy(gameObject, _jointController.startRectangle.GetComponent<RectangleInfo>().listLines);
                    LineDestroy(gameObject, _jointController.finishRectangle.GetComponent<RectangleInfo>().listLines);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void LineDestroy(GameObject destroyingLine, List<GameObject> currentListLines)
    {
        currentListLines.Remove(destroyingLine);
        Destroy(destroyingLine);
    }
}