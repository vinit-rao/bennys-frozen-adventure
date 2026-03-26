using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TextMeshProUGUI customerNameText;
    public Image[] scoopSlots;

    public Sprite strawberrySprite;
    public Sprite vanillaSprite;
    public Sprite chocolateSprite;

    public GameObject completeStamp;
    public GameObject incompleteStamp;

    public void SetupOrderVisuals(Orders orderData)
    {
        customerNameText.text = orderData.name;
        if (completeStamp != null) completeStamp.SetActive(false);
        if (incompleteStamp != null) incompleteStamp.SetActive(false);
        foreach (Image slot in scoopSlots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < orderData.iceCreams.Count; i++)
        {
            if (i >= scoopSlots.Length) break;

            scoopSlots[i].gameObject.SetActive(true);

            if (orderData.iceCreams[i] == 0)
                scoopSlots[i].sprite = strawberrySprite;
            else if (orderData.iceCreams[i] == 1)
                scoopSlots[i].sprite = chocolateSprite;
            else if (orderData.iceCreams[i] == 2)
                scoopSlots[i].sprite = vanillaSprite;
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