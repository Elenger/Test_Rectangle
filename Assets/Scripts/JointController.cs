using UnityEngine;

public class JointController : MonoBehaviour
{
    public Transform startRectangle;
    public Transform finishRectangle;
    public RectangleInfo startRectangleInfo;
    public RectangleInfo finishRectangleInfo;

    private void Start()
    {
        LineUpdate();
    }

    //Change Line position, scale and rotation depending on locations joined Rectangles.
    public void LineUpdate()
    {
        Vector3 positionStart = startRectangle.position;
        Vector3 positionFinish = finishRectangle.position;

        transform.position = (positionStart + positionFinish) * 0.5f;

        Vector3 difference = positionStart - positionFinish;
        float lenghtLine = difference.magnitude;

        Vector3 scaleLine = transform.localScale;
        scaleLine.x = lenghtLine;
        transform.localScale = scaleLine;

        float angleRad = Mathf.Atan2(difference.y, difference.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        Quaternion angleRotation = new Quaternion();
        angleRotation.eulerAngles = new Vector3(0f, 0f, angleDeg);
        transform.rotation = angleRotation;
    }
}
