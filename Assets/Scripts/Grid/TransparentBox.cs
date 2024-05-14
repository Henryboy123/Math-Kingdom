using UnityEngine;

public class TransparentBox : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    public bool HasChangedSprite()
    {
        // Compare the current sprite to the original sprite
        return spriteRenderer.sprite != originalSprite;
    }
}
