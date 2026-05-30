using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BlacksmithManager : MonoBehaviour
{
    [Header("Panel")]
    public Transform itemPanel;
    public GameObject itemCardPrefab;

    [Header("Items")]
    public ItemData[] allItems;

    private List<GameObject> itemCards = new List<GameObject>();

    void Start()
    {
        GenerateItems();
    }

    void GenerateItems()
    {
        List<ItemData> available = new List<ItemData>(allItems);
        int count = Mathf.Min(3, available.Count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            ItemData item = available[randomIndex];
            available.RemoveAt(randomIndex);

            GameObject card = Instantiate(itemCardPrefab, itemPanel);
            itemCards.Add(card);

            card.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            card.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            card.transform.Find("Stats").GetComponent<TMP_Text>().text = BuildStatsText(item);

            ItemData captured = item;
            card.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(captured, card));
        }
    }

    string BuildStatsText(ItemData item)
    {
        string text = "";
        foreach (StatModifier mod in item.modifiers)
        {
            string sign = mod.value >= 0 ? "+" : "";
            text += mod.stat + ": " + sign + mod.value + "\n";
        }
        return text.TrimEnd();
    }

    void BuyItem(ItemData item, GameObject card)
    {
        GameManager.instance.inventory.Add(item.itemName);
        card.transform.localScale = Vector3.zero;
    }
}