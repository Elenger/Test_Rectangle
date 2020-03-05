using UnityEngine;

public class JointController : MonoBehaviour
{
    public GameObject startRectangle;
    public GameObject finishRectangle;

    private void Start()
    {
        LineUpdate();
    }

    //Change Line position, scale and rotation depending on locations joined Rectangles.
    public void LineUpdate()
    {
        Transform transformStartRectangle = startRectangle.transform;
        Transform transformFinishRectangle = finishRectangle.transform;

        gameObject.transform.position = LinePosition(transformStartRectangle, transformFinishRectangle);
        float lenghtLine = (transformStartRectangle.position - transformFinishRectangle.position).magnitude;
        Vector3 scaleLine = gameObject.transform.localScale;
        scaleLine.x = lenghtLine;
        gameObject.transform.localScale = scaleLine;

        float angleRad = Mathf.Atan2(transformStartRectangle.position.y - transformFinishRectangle.position.y,
            transformStartRectangle.position.x - transformFinishRectangle.position.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        Quaternion angleRotation = new Quaternion();
        angleRotation.eulerAngles = new Vector3(0f, 0f, angleDeg);
        gameObject.transform.rotation = angleRotation;
    }

    private Vector3 LinePosition(Transform transformStartRectangle, Transform transformFinishRectangle)
    {
        float x = (transformStartRectangle.position.x + transformFinishRectangle.position.x) / 2;
        float y = (transformStartRectangle.position.y + transformFinishRectangle.position.y) / 2;
        return new Vector2(x, y);
    }
}
