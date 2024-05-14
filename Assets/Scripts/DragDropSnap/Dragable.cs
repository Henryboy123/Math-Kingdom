using UnityEngine;

public class Dragable : MonoBehaviour
{
    public bool IsDraging;
    public Vector3 LastPosition;
    private Collider2D _collider;
    private DragController _dragController;
    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestination;
    public string gridType;
    public BrickTypeEnum BrickType;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
    }

    void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if (IsDraging)
            {
                _movementDestination = null;
                return;
            }

            if (transform.position == _movementDestination.Value)
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestination.Value, _movementTime * Time.fixedDeltaTime);
            }
        }
    }
    void DeleteBrick()
    {
        Destroy(gameObject);
    }

    public void MoveGameObject(GameObject gameObj)
    {
        if (gameObj != null)
        {
            Transform TransformGameObj = gameObj.transform;
            TransformGameObj.position += new Vector3(0f, 450f, 0f);
        }
        else
        {
            Debug.LogError("GameObject is not assigned.");
        }
    }
}
