using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RectangleInfo : MonoBehaviour, IPointerClickHandler
{
    private static RectangleInfo selectionStartRectangle;
    public List<JointController> listLines;
    public GameObject linePrefab;
    public Transform canvasTransform;

    //If was left click on the Rectangle and then was right click on another Rectangle, then between these Rectangles makes the Line
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            selectionStartRectangle = this;
        }
        else if (eventData.button == PointerEventData.InputButton.Right
            && selectionStartRectangle != null
            && selectionStartRectangle != gameObject
            && !_joinedLineExist)
        {
            GameObject newLine = Instantiate(linePrefab, new Vector3().normalized, Quaternion.identity, canvasTransform);
            JointController newJointController = newLine.GetComponent<JointController>();
            newJointController.startRectangle = selectionStartRectangle.transform;
            newJointController.finishRectangle = transform;

            selectionStartRectangle.listLines.Add(newJointController);
            listLines.Add(newJointController);

            selectionStartRectangle = null;
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            selectionStartRectangle = null;
        }
    }

    private bool _joinedLineExist
    {
        get
        {
            bool joinedLineExist = false;
            foreach (var line in selectionStartRectangle.listLines)
            {
                if (listLines.Contains(line))
                {
                    joinedLineExist = true;
                }
                else joinedLineExist = false;
            }
            return joinedLineExist;
        }
    }
}
