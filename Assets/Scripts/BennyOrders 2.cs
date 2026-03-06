using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class orders
{
    //description for the order
    public List<int> iceCreams = new List<int>();
    public string name;
    public bool complete;

    //how many of the ice creams are correct
    public int leftA = 0;
    public int rightA = 0;
}

public class BennyOrders : MonoBehaviour
{
    //lists flavors for each of Benny's arms
    public List<int> leftOrder = new List<int>();
    public List<int> rightOrder = new List<int>();

    //how many ice creams benny is holding in each hand
    private int leftCount = 0;
    private int rightCount = 0;

    orders Order1;
    public TextMeshProUGUI text;

    //randomly generates a set of ice creams to complete
    void Start()
    {
        Order1 = new orders();
        text.text = "";
        for (int i = 0; i < 3; i++)
        {
            int Rand = Random.Range(0, 3);
            Order1.iceCreams.Add(Rand);

            switch (Rand)
            {
                case 0:
                    text.text += "straw, ";
                    break;
                case 1:
                    text.text += "van, ";
                    break;
                case 2:
                    text.text += "choc, ";
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        int leftA = 0;
        int rightA = 0;

        if (leftCount < leftOrder.Count || rightCount < rightOrder.Count)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!Order1.complete)
                {
                    if (leftOrder[i] == Order1.iceCreams[i])
                    {
                        leftA++;

                    }
                    else if (rightOrder[i] == Order1.iceCreams[i])
                    {
                        rightA++;
                    }
                }
            }
            if (leftA == 3 || rightA == 3)
            {
                text.text = "Complete";
                Order1.complete = true;
            }

            leftCount = leftOrder.Count;
            rightCount = rightOrder.Count;
        }

    }
}
