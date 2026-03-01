using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class orders
{
    public List<int> iceCreams = new List<int>();
}

public class BennyOrders : MonoBehaviour
{
    public List<int> leftOrder = new List<int>();
    public List<int> rightOrder = new List<int>();

    orders Order1;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Order1 = new orders();
        text.text = "";
        for (int i = 0; i < 3; i++)
        {
            int Rand = Random.Range(0, 3);
            Order1.iceCreams.Add(Rand);
            print(Order1.iceCreams[i]);

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

        bool complete = false;

        for (int i = 0; i < 3; i++)
        {
            if (!complete)
            {
                if (leftOrder[i] == Order1.iceCreams[i])
                {
                    leftA++;
                }
                else if (rightOrder[i] == Order1.iceCreams[i])
                {
                    rightA++;
                }
            } else if (leftA <= 3 || rightA <= 3)
            {
                complete = true;
                text.text = "Complete";
            }
   
        }
    }
}
