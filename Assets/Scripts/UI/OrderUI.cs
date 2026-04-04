using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TextMeshProUGUI customerNameText;
    public TextMeshProUGUI timerText;

    public Image[] scoopSlots;
    public BennyOrders BennyOrders;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;

    public RenderTexture[] rtCustomers;

    public GameObject completeStamp;
    public GameObject incompleteStamp;

    void Start()
    {
        // shuffle render texture list
        for (int i = 0; i < rtCustomers.Length; i++)
        {
            int randomIndex = Random.Range(i, rtCustomers.Length);
            RenderTexture temp = rtCustomers[i];
            rtCustomers[i] = rtCustomers[randomIndex];
            rtCustomers[randomIndex] = temp;
        }
    }
    public void SetupOrderVisuals(Orders orderData, int index)
    {
        if (orderData.IsActive)
        {
            setPosition(orderData, index);
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
                    scoopSlots[i].sprite = Sprite1;
                else if (orderData.iceCreams[i] == 1)
                    scoopSlots[i].sprite = Sprite2;
                else if (orderData.iceCreams[i] == 2)
                    scoopSlots[i].sprite = Sprite3;
            }

            RawImage thumbnailImage = GameObject.Find("customerThumbnail").GetComponent<RawImage>();
            if (index < rtCustomers.Length)
                thumbnailImage.texture = rtCustomers[index];

        }
        else
        {
            //if you want to create the grayed out version
        }
    }

    public void setPosition(Orders order, int index)
    {
        RectTransform rect = GetComponent<RectTransform>();
        int ypos = 500;

        switch (index)
        {
            case 0:
                rect.anchoredPosition = new Vector2(-740, 180);
                break;
            case 1:
                rect.anchoredPosition = new Vector2(-740, 180 - ypos);
                break;
        }
    }

    public void MarkAsComplete()
    {
        if (completeStamp != null) completeStamp.SetActive(true);
        if (incompleteStamp != null) incompleteStamp.SetActive(false);
        AudioManager.Instance.PlayOrderCompleteSound();
    }

    public void MarkAsIncomplete()
    {
        if (incompleteStamp != null) incompleteStamp.SetActive(true);
        if (completeStamp != null) completeStamp.SetActive(false);
    }
}