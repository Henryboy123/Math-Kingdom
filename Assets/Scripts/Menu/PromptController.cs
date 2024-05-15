using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptController : MonoBehaviour
{
    [SerializeField]
    public GameObject castleGrid;
    public Canvas UiCanvas;
    public SpriteRenderer constructionTower;
    public Button startButton;
    private Animator animator;
    public Canvas promptCanvas;

    private void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }
    public void onStart()
    {
        EventsManager.Instance.OnGameStart.Invoke();
        castleGrid.SetActive(true);
        UiCanvas.gameObject.SetActive(true);
        constructionTower.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        animator.SetTrigger("PlayMoveToTopRight");
    }
}
