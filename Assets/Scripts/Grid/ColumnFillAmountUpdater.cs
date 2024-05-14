using UnityEngine;
using UnityEngine.UI;

public class ColumnFillAmountUpdater : MonoBehaviour
{
    [SerializeField] public Image tallImage;
    private float accumulatedFillAmount = 0f;

    // Update fill amount based on filled parts of columns
    public void UpdateFillAmount(float fillPercentage)
    {
        // Ensure the image reference is assigned
        if (tallImage == null)
        {
            Debug.LogWarning("Tall image reference is not assigned!");
            return;
        }

        // Clamp fill percentage between 0 and 1
        fillPercentage = Mathf.Clamp01(fillPercentage);

        // Update accumulated fill amount
        accumulatedFillAmount += fillPercentage;
        accumulatedFillAmount = Mathf.Clamp01(accumulatedFillAmount);

        // Update image fill amount
        tallImage.fillAmount = accumulatedFillAmount;
    }
}
