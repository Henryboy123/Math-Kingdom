using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
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
        EventsManager.Instance.OnEndDrag.AddListener(CheckLayerFillState);
        for (int layer = 1; layer <= 10; layer++)
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

    void CheckLayerFillState()
    {
        foreach (var keyValuePair in boxDictionary)
        {
            string layerKey = keyValuePair.Key;
            List<TransparentBox> boxes = keyValuePair.Value;
            int layerNumber = int.Parse(keyValuePair.Key.Split(' ').Last());
            bool allBoxesChanged = true;

            if (layerNumber != currentUnfilledRow)
            {
                continue;
            }

            print(layerNumber);
            print(currentUnfilledRow);

            foreach (TransparentBox box in boxes)
            {
                if (!box.HasChangedSprite())
                {
                    allBoxesChanged = false;
                    break;
                }
            }

            if (!allBoxesChanged)
            {
                break;
            }
            currentUnfilledRow++;


            // If all boxes in the layer have changed their sprites and the layer isn't already marked as filled
            if (allBoxesChanged && !layerFilledState[layerKey])
            {
                layerFilledState[layerKey] = true;
                float fillPercentage = 1f / 10f;

                columnFillAmountUpdater.UpdateFillAmount(fillPercentage);
                Debug.Log($"{layerKey} is fully filled!");
                EmptyBoxes(layerKey);
                if (currentUnfilledRow - 1 == boxDictionary.Keys.Count)
                {
                    EventsManager.Instance.OnWin.Invoke();
                }
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
