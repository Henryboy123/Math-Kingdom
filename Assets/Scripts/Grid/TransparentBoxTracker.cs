using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransparentBoxTracker : MonoBehaviour
{
    private Dictionary<string, List<TransparentBox>> boxDictionary = new Dictionary<string, List<TransparentBox>>();
    private Dictionary<string, bool> layerFilledState = new Dictionary<string, bool>();
    public ColumnFillAmountUpdater columnFillAmountUpdater;
    public int currentUnfilledRow = 1;
    [SerializeField]
    public int layerId = 0;
    void Start()
    {
        for (int layer = 1; layer <= 9; layer++)
        {
            string layerKey = $"Layer {layer}";
            layerFilledState[layerKey] = false;

            GameObject layerGameObject = GameObject.Find(layerKey);

            if (layerGameObject != null)
            {
                TransparentBox[] boxesArray = layerGameObject.GetComponentsInChildren<TransparentBox>();
                boxDictionary[layerKey] = new List<TransparentBox>(boxesArray);
            }
            else
            {
                Debug.LogError($"Layer GameObject '{layerKey}' not found.");
            }
        }
    }


    void Update()
    {
        CheckLayerFillState();
    }

    void CheckLayerFillState()
    {
        foreach (var keyValuePair in boxDictionary)
        {
            string layerKey = keyValuePair.Key;
            List<TransparentBox> boxes = keyValuePair.Value;
            int layerNumber = keyValuePair.Key[keyValuePair.Key.Length - 1] - '0';
            bool allBoxesChanged = true;

            if (layerNumber == currentUnfilledRow)
            {
                foreach (TransparentBox box in boxes)
                {
                    if (!box.HasChangedSprite())
                    {
                        allBoxesChanged = false;
                        return;
                    }
                }
                currentUnfilledRow++;
            }
            else
            {
                return;
            }


            // If all boxes in the layer have changed their sprites and the layer isn't already marked as filled
            if (allBoxesChanged && !layerFilledState[layerKey])
            {
                layerFilledState[layerKey] = true;
                float fillPercentage = 1f / 9f;

                columnFillAmountUpdater.UpdateFillAmount(fillPercentage);
                Debug.Log($"{layerKey} is fully filled!");
                EmptyBoxes(layerKey);
            }
        }
    }

    void EmptyBoxes(string layerKey)
    {
        if (boxDictionary.ContainsKey(layerKey))
        {
            List<TransparentBox> boxes = boxDictionary[layerKey];

            foreach (TransparentBox box in boxes)
            {
                box.gameObject.SetActive(false);
            }
        }
    }
}
