using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TextMeshProUGUI customerNameText;
    public TextMeshProUGUI timerText;

    public Image[] scoopSlots;
    public BennyOrders BennyOrders;

    public Sprite strawberrySprite;
    public Sprite vanillaSprite;
    public Sprite chocolateSprite;

    public GameObject completeStamp;
    public GameObject incompleteStamp;

    public void SetupOrderVisuals(Orders orderData, int index)
    {
        setPosition(orderData, index);

        if (orderData.IsActive)
        {
            customerNameText.text = orderData.name;

            if (completeStamp != null) completeStamp.SetActive(false);
            if (incompleteStamp != null) incompleteStamp.SetActive(false);
            foreach (Image slot in scoopSlots)
            {
                slot.gameObject.SetActive(false);
            }

            timerText.text = "Time: " + orderData.timer;

            for (int i = 0; i < orderData.iceCreams.Count; i++)
            {
                if (i >= scoopSlots.Length) break;

                scoopSlots[i].gameObject.SetActive(true);

                if (orderData.iceCreams[i] == 0)
                    scoopSlots[i].sprite = strawberrySprite;
                else if (orderData.iceCreams[i] == 1)
                    scoopSlots[i].sprite = vanillaSprite;
                else if (orderData.iceCreams[i] == 2)
                    scoopSlots[i].sprite = chocolateSprite;
            }
        }
        
    }

    public void setPosition(Orders order, int index)
    {
        int orderCount = BennyOrders.levelOrders.Count;
        RectTransform rect = GetComponent<RectTransform>();
        int ypos = 350;

        switch (index)
        {
            case 0:
                rect.anchoredPosition = new Vector2(-802, 280);
                break;
            case 1:
                rect.anchoredPosition = new Vector2(-802, 280 - ypos);
                break;
            case 2:
                rect.anchoredPosition = new Vector2(-802, 280 - (ypos * 2));
                break;
            case 3:
                rect.anchoredPosition = new Vector2(-552, 280);
                break;
            case 4:
                rect.anchoredPosition = new Vector2(-552, 280 - ypos);
                break;
            case 5:
                rect.anchoredPosition = new Vector2(-552, 280 - (ypos * 2));
                break;
        }
    }

    public void MarkAsComplete()
    {
        if (completeStamp != null) completeStamp.SetActive(true);
        if (incompleteStamp != null) incompleteStamp.SetActive(false);
    }

    public void MarkAsIncomplete()
    {
        if (incompleteStamp != null) incompleteStamp.SetActive(true);
        if (completeStamp != null) completeStamp.SetActive(false);
    }
}