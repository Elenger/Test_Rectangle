using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RectangleInfo : MonoBehaviour, IPointerClickHandler
{
    public static GameObject selectionStartRectangle;
    public List<GameObject> listLines;
    public GameObject linePrefab;
    public Transform canvasTransform;

    //If was left click on the Rectangle and then was right click on another Rectangle, then between these Rectangles makes the Line
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            selectionStartRectangle = gameObject;
        }
        else if (eventData.button == PointerEventData.InputButton.Right && selectionStartRectangle != null 
            && selectionStartRectangle!=gameObject && !_joinedLineExist)
        {
            GameObject newLine = Instantiate(linePrefab, new Vector3().normalized, Quaternion.identity, canvasTransform);
            JointController jointControllernewLine = newLine.GetComponent<JointController>();
            jointControllernewLine.startRectangle = selectionStartRectangle;
            jointControllernewLine.finishRectangle = gameObject;

            selectionStartRectangle.GetComponent<RectangleInfo>().listLines.Add(newLine);
            listLines.Add(newLine);

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
            foreach (GameObject line in selectionStartRectangle.GetComponent<RectangleInfo>().listLines)
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
