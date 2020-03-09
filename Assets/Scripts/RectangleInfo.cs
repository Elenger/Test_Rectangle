using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RectangleInfo : MonoBehaviour, IPointerClickHandler
{
    private static RectangleInfo startRectangle;
    public List<JointController> listLines;
    public GameObject linePrefab;
    public Transform canvasTransform;

    //If was left click on the Rectangle and then was right click on another Rectangle, then between these Rectangles makes the Line
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            startRectangle = this;
        }
        else if (eventData.button == PointerEventData.InputButton.Right
            && startRectangle != null
            && startRectangle != gameObject
            && !_joinedLineExist)
        {
            GameObject newLine = Instantiate(linePrefab, new Vector3().normalized, Quaternion.identity, canvasTransform);
            JointController newJointController = newLine.GetComponent<JointController>();
            newJointController.startRectangle = startRectangle.transform;
            newJointController.finishRectangle = transform;
            newJointController.startRectangleInfo = startRectangle;
            newJointController.finishRectangleInfo = this;


            startRectangle.listLines.Add(newJointController);
            listLines.Add(newJointController);
        }
    }

    private bool _joinedLineExist
    {
        get
        {
            bool joinedLineExist = false;
            foreach (var line in startRectangle.listLines)
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
