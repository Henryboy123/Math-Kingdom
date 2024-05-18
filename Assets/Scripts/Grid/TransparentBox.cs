using UnityEngine;

public class TransparentBox : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    public BrickTypeEnum AssignedBrickType = BrickTypeEnum.None;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    public bool HasChangedSprite => AssignedBrickType != BrickTypeEnum.None;
}
