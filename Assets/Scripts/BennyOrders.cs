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
    public List<int> currentOrder = new List<int>();
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
    }
}
