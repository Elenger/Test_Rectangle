using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Transform _canvasTransform;
    private float _halfHeightRectangle = 0.5f;
    private float _halfWidthRectangle = 1f;
    private Image _image;

    void Start()
    {
        _image = _prefab.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 mouseGlob = Camera.main.ScreenToWorldPoint(mouse);
                                                                       
            Vector2 topLeftCorner = new Vector2(mouseGlob.x - _halfWidthRectangle,
                mouseGlob.y + _halfHeightRectangle);
            Vector2 bottomRightCorner = new Vector2(mouseGlob.x + _halfWidthRectangle,
                mouseGlob.y - _halfHeightRectangle);

            Collider2D obstructionCollider = Physics2D.OverlapArea(topLeftCorner, bottomRightCorner);
            if (obstructionCollider == null)
            {
                RandomImageColor();
                GameObject rectangle = Instantiate(_prefab, mouseGlob, Quaternion.identity, _canvasTransform);
                RectangleInfo rectangleInfo = rectangle.GetComponent<RectangleInfo>();
                rectangleInfo.linePrefab = _linePrefab;
                rectangleInfo.canvasTransform = _canvasTransform;
            }
        }
    }

    public void RandomImageColor()
    {
        _image.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}

