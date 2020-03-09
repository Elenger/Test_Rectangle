using UnityEngine;

public class JointController : MonoBehaviour
{
    public Transform startRectangle;
    public Transform finishRectangle;

    private void Start()
    {
        LineUpdate();
    }

    //Change Line position, scale and rotation depending on locations joined Rectangles.
    public void LineUpdate()
    {
        Transform transformStart = startRectangle.transform;
        Transform transformFinish = finishRectangle.transform;

        transform.position = LinePosition(transformStart, transformFinish);
        float lenghtLine = (transformStart.position - transformFinish.position).magnitude;
        Vector3 scaleLine = transform.localScale;
        scaleLine.x = lenghtLine;
        transform.localScale = scaleLine;

        float angleRad = Mathf.Atan2(transformStart.position.y - transformFinish.position.y,
            transformStart.position.x - transformFinish.position.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        Quaternion angleRotation = new Quaternion();
        angleRotation.eulerAngles = new Vector3(0f, 0f, angleDeg);
        transform.rotation = angleRotation;
    }

    private Vector3 LinePosition(Transform transformStartRectangle, Transform transformFinishRectangle)
    {
        float x = (transformStartRectangle.position.x + transformFinishRectangle.position.x) / 2;
        float y = (transformStartRectangle.position.y + transformFinishRectangle.position.y) / 2;
        return new Vector2(x, y);
    }
}
