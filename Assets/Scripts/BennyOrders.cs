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
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Order1 = new orders();

        for (int i = 0; i < 4; i++)
        {
            int Rand = Random.Range(0, 3);
            string flavor;
            Order1.iceCreams.Add(Rand);
            print(Order1.iceCreams[i]);

            switch (Rand)
            {
                case 0:
                    flavor = "strawberry";
                    break;
                case 1:
                    flavor = "vanilla";
                    break;
                case 2:
                    flavor = "chocolate";
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
