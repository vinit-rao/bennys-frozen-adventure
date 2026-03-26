using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orders
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

    public GameObject floor;

    private List<string> names = new List<string>()
    {
        "Alex","Sam","Jamie","Taylor","Casey","Morgan","Riley","Jordan","Parker","Quinn",
        "Avery","Cameron","Blake","Charlie","Elliot","Kai","Leo","Noah","Liam","Logan",
        "Lucas","Ethan","Mason","Elijah","James","Benjamin","Henry","Jack","Daniel",
        "Matthew","Samuel","David","Joseph","Owen","Wyatt","Luke","Nathan","Connor",
        "Tyler","Hunter","Evan","Cole","Tristan","Victor","Oscar","Adrian","Marco",
        "Diego","Mateo","Carlos","Javier","Ricardo","Santiago",

        "Sarah","Emily","Grace","Lily","Anna","Claire","Sophie","Hannah","Julia","Leah",
        "Madeline","Elena","Bianca","Valeria","Camila","Daniela","Lucia","Marina",
        "Olivia","Emma","Ava","Sophia","Isabella","Mia","Charlotte","Amelia","Harper",
        "Evelyn","Abigail","Ella","Scarlett","Aria","Nova","Luna","Hazel","Willow",

        "Fabio","Rania","Rowan","Natalia","Naia","Rebecca","Skye","Silver","Mungus",
        "Vinit","Khoi","Hugh","Ronald","Craig","Aidan","Andy","Michelle","Jason",

        "Batman","Spider-Man","Venom","Superman","Joker","Harley Quinn",
        "Goku","Naruto","Levi","Mikasa","Eren",

        "Sonic","Shadow","Tails","Knuckles",
        "Mario","Luigi","Peach",
        "Link","Zelda",
        "Kratos","Atreus",
        "Kirby"
    };

    Orders Order1;
    Orders Order2;
    Orders Order3;

    public OrderUI uiTicket1;
    public OrderUI uiTicket2;
    public OrderUI uiTicket3;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    Orders createOrder(OrderUI ticketVisual, int size, int time)
    {
        Orders order = new Orders();
        int nameIndex = Random.Range(0, names.Count);
        order.name = names[nameIndex];

        for (int i = 0; i < size; i++)
        {
            int Rand = Random.Range(0, 3);
            order.iceCreams.Add(Rand);
        }

        ticketVisual.SetupOrderVisuals(order);

        return order;
    }


    //randomly generates a set of ice creams to complete
    void Start()
    {
        floor = GameObject.Find("Floor");

        Order1 = createOrder(uiTicket1, 5, 0);
        Order2 = createOrder(uiTicket2, 5, 15);
        Order3 = createOrder(uiTicket3, 5, 30);
    }

    bool checkComplete(Orders order, OrderUI ticketVisual)
    {
        Debug.Log($"Order needs: [{string.Join(", ", order.iceCreams)}]");
        Debug.Log($"Left has:    [{string.Join(", ", leftOrder)}]");
        Debug.Log($"Right has:   [{string.Join(", ", rightOrder)}]");
        if (order.complete) return false;

        int leftA = 0;
        int rightA = 0;

        for (int i = 0; i < order.iceCreams.Count; i++)
        {
            // Read leftOrder/rightOrder from the top of the stack (end of list)
            int leftIndex = leftOrder.Count - 1 - i;
            int rightIndex = rightOrder.Count - 1 - i;

            if (leftIndex >= 0 && leftOrder[leftIndex] == order.iceCreams[i])
                leftA++;

            if (rightIndex >= 0 && rightOrder[rightIndex] == order.iceCreams[i])
                rightA++;
        }

        leftCount = leftOrder.Count;
        rightCount = rightOrder.Count;

        if (leftA == order.iceCreams.Count)
        {
            ticketVisual.MarkAsComplete();
            order.complete = true;

            Transform armL = transform.Find("ArmL");  // fixed: use Find instead of GetChild(1)
            for (int j = leftOrder.Count - 1; j >= 0; j--)
            {
                if (j < armL.childCount)
                    Destroy(armL.GetChild(j).gameObject);
                leftOrder.RemoveAt(j);
            }

            leftCount = 0;
            leftText.text = "Left :";
            return true;
        }

        if (rightA == order.iceCreams.Count)
        {
            ticketVisual.MarkAsComplete();
            order.complete = true;

            Transform armR = transform.Find("ArmR");  // fixed: use Find instead of GetChild(0)
            for (int j = rightOrder.Count - 1; j >= 0; j--)
            {
                if (j < armR.childCount)
                    Destroy(armR.GetChild(j).gameObject);
                rightOrder.RemoveAt(j);
            }

            rightCount = 0;
            rightText.text = "Right :";
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        checkComplete(Order1, uiTicket1);
        checkComplete(Order2, uiTicket2);
        checkComplete(Order3, uiTicket3);

        // garbage bin at the top right corner (4, 0, 4) to destroy ice cream
        if (gameObject.transform.position.x == 4 && gameObject.transform.position.z == 4)
        {
            float rotation = gameObject.GetComponent<BennyScript>().benny_rotation;

            if (rotation == 90)
            {
                print("In the area");
                foreach (Transform iceCream in transform.Find("ArmL"))
                {
                    Destroy(iceCream.gameObject);
                }

                leftOrder.Clear();
                leftText.text = "Left: ";
                leftCount = leftOrder.Count;
            }
            else if (rotation == 270)
            {
                print("In the area");
                foreach (Transform iceCream in transform.Find("ArmR"))
                {
                    Destroy(iceCream.gameObject);
                }

                rightOrder.Clear();
                rightText.text = "Right: ";
                rightCount = rightOrder.Count;
            }
        }
    }
}
