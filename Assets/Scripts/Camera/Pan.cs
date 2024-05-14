using UnityEngine;

public class Pan : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 10;
    private bool canDrag = true;
    private float screenWidth;
    private float screenHeight;
    private float backgroundWidth;
    private float backgroundHeight;

    [SerializeField] private SpriteRenderer background;

    private new Camera camera;

    private void Awake()
    {
        EventsManager.Instance.OnStartDrag.AddListener(() => canDrag = false);
        EventsManager.Instance.OnEndDrag.AddListener(() => canDrag = true);

        camera = Camera.main;
        camera.orthographicSize = 10;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        backgroundWidth = background.bounds.size.x;
        backgroundHeight = background.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canDrag)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 unclampedPosition = camera.transform.position + direction;

            // Calculate the maximum and minimum x positions for the camera
            float cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);
            float backgroundLeftEdge = background.transform.position.x - backgroundWidth / 2 + cameraHalfWidth;
            float backgroundRightEdge = background.transform.position.x + backgroundWidth / 2 - cameraHalfWidth;
            unclampedPosition.x = Mathf.Clamp(unclampedPosition.x, backgroundLeftEdge, backgroundRightEdge);

            // Calculate the maximum and minimum y positions for the camera
            float cameraHalfHeight = camera.orthographicSize * ((float)Screen.width / Screen.height);
            float backgroundBottomEdge = background.transform.position.y - backgroundHeight / 2 + cameraHalfHeight;
            float backgroundTopEdge = background.transform.position.y + backgroundHeight / 2 - cameraHalfHeight;
            unclampedPosition.y = Mathf.Clamp(unclampedPosition.y, backgroundBottomEdge, backgroundTopEdge);

            // Apply the clamped position to the camera
            camera.transform.position = unclampedPosition;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}