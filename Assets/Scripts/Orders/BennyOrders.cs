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

    orders Order1;
    orders Order2;
    orders Order3;

    public OrderUI uiTicket1;
    public OrderUI uiTicket2;
    public OrderUI uiTicket3;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    orders createOrder(OrderUI ticketVisual, int size, int time)
    {
        orders order = new orders();
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

    //orders createOrder(TextMeshProUGUI text, int size, int time)
    //{
    //    orders order = new orders();

    //    int name = Random.Range(0, names.Count);
    //    order.name = names[name];
    //    text.text = order.name + ": ";
        
    //    for (int i = 0; i < size; i++)
    //    {
    //        int Rand = Random.Range(0, 3);  
    //        order.iceCreams.Add(Rand);


    //        switch (Rand)
    //        {
    //            case 0:
    //                text.text += "Strawberry, ";
    //                break;
    //            case 1:
    //                text.text += "Vanilla, ";
    //                break;
    //            case 2:
    //                text.text += "Chocolate, ";
    //                break;
    //        }
    //    }

    //    return order;
    //}

    //randomly generates a set of ice creams to complete
    void Start()
    {
        Order1 = createOrder(uiTicket1, 5, 0);
        Order2 = createOrder(uiTicket2, 5, 15);
        Order3 = createOrder(uiTicket3, 5, 30);
        floor = GameObject.FindWithTag("Floor");
    }

    void checkComplete(orders order, OrderUI ticketVisual)
    {
        //resets how many ice creams are correct every frame
        int leftA = 0;
        int rightA = 0;

        //checks if the flavor is correct on either side for however many scoops there are in the order
        for (int i = 0; i < order.iceCreams.Count; i++)
        {

            //if the order isn't already complete then do it
            if (!order.complete)
            {

                //if the ice creams on either hand is equal to the same position on order, then add to "leftA" (however many are correct)
                //if the amount of scoops in the left and right order is less than the value of i, it won't check it
                if (i < leftOrder.Count && leftOrder[i] == order.iceCreams[i])
                {
                    leftA++;
                }

                if (i < rightOrder.Count && rightOrder[i] == order.iceCreams[i])
                {
                    rightA++;
                }
            }

            //if leftA or rightA (however many ice creams are correct) is equal to the amount of ice creams in the order, mark the order as complete
            if (leftA == order.iceCreams.Count)
            {
                ticketVisual.MarkAsComplete();
                order.complete = true;

                for (int j = 0; j < order.iceCreams.Count; j++)
                {
                    Destroy(transform.GetChild(1).GetChild(leftOrder.Count - 1).gameObject);
                    leftOrder.RemoveAt(leftOrder.Count - 1);
                    leftCount -= 1;

                    leftText.text = "Left :";
                }

            }
            else if (rightA == order.iceCreams.Count)
            {
                ticketVisual.MarkAsComplete();
                order.complete = true;

                for (int j = 0; j < order.iceCreams.Count; j++)
                {
                    Destroy(transform.GetChild(0).GetChild(rightOrder.Count - 1).gameObject);
                    rightOrder.RemoveAt(rightOrder.Count - 1);
                    rightCount -= 1;
                    rightText.text = "Right :";
                }

            }


            //update left and right count
            leftCount = leftOrder.Count;
            rightCount = rightOrder.Count;

        }
    }

    // Update is called once per frame
    void Update()
    {

        //checks if any ice creams have been added to the left or right hand
        if (leftCount < leftOrder.Count || rightCount < rightOrder.Count)
        {
            checkComplete(Order1, uiTicket1);
            checkComplete(Order2, uiTicket2);
            checkComplete(Order3, uiTicket3);
        }
        // garbage bin at the top right corner (4, 0, 4) to destroy ice cream
        if (gameObject.transform.position.x == 4 && gameObject.transform.position.z == 4)
        {
            
            if (gameObject.GetComponent<BennyScript>().benny_rotation == 270)
            {
                for (int i = 0; i < leftOrder.Count; i++)
                {
                    Destroy(transform.GetChild(1).GetChild(i).gameObject);
                    leftOrder.RemoveAt(0);
                    leftCount = 0;

                    leftText.text = "Left: ";
                }
            } else if (gameObject.GetComponent<BennyScript>().benny_rotation == 90)
            {
                for (int i = 0; i < rightOrder.Count; i++)
                {
                    Destroy(transform.GetChild(0).GetChild(i).gameObject);
                    rightOrder.RemoveAt(0);
                    rightCount = 0;

                    rightText.text = "Right: ";
                }
            }
        }
    }
}
