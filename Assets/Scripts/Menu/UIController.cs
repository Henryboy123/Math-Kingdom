using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button stoneBrickButton;
    [SerializeField] GameObject stoneBrickGameObject;
    [SerializeField] Button goldBrickButton;
    [SerializeField] GameObject goldBrickGameObject;

    private void Awake()
    {
        stoneBrickButton.onClick.AddListener(() => createBrickGameObject(stoneBrickButton.GetComponent<RectTransform>(), stoneBrickGameObject));
        goldBrickButton.onClick.AddListener(() => createBrickGameObject(goldBrickButton.GetComponent<RectTransform>(), goldBrickGameObject));

    }

    public void createBrickGameObject(Transform buttonTransform, GameObject brickPrefab)
    {
        Instantiate(brickPrefab, buttonTransform.position + Vector3.right, Quaternion.identity);
    }
}
