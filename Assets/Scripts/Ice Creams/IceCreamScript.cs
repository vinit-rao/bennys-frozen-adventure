using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class IceCreamScript : MonoBehaviour
{
    public float scoopHeight = 0.5f;
    public Rigidbody rb;
    public bool landed = false;
    public BennyOrders bennyOrders;
    public int numOrder;
    bool holdingSpace;
    public IceCreamSpawner spawner;

    private float timeRemaining = 5;
    GameObject collision = null;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;
    private ArduinoController arduino;

    //freeze ice cream in every direction but down
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bennyOrders = GameObject.FindWithTag("Player").GetComponent<BennyOrders>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        arduino = FindObjectOfType<ArduinoController>();
    }

    private void addScoop()
    {
        //counts the amount of ice creams benny is holding in each hand
        int leftCount = bennyOrders.leftOrder.Count;
        int rightCount = bennyOrders.rightOrder.Count;

        //how high the first ice cream should be
        float stack_y = 2.5f;

        Destroy(gameObject.transform.Find("Spot Light(Clone)").gameObject);

        transform.eulerAngles = new Vector3(0, 0, 0);

        if (transform.parent.name == "ArmL")
        {
            print("Added left");

            if (arduino != null && arduino.useArduinoController) //arduino left led blink
            {
                arduino.BlinkLeftLED();
            }

            //stacks it to y = 2.25, adds the average scoop height multiplied by how many scoops there are in that hand
            stack_y += scoopHeight * (leftCount);

            //changes position
            transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);

            numOrder = bennyOrders.leftOrder.Count;

            //adds the ice cream to the hand's list depending on its name
            switch (transform.name)
            {
                case "ScoopStrawberry(Clone)":
                    bennyOrders.leftOrder.Add(0);
                    leftOrderText.text += "straw, ";
                    return;

                case "ScoopVanilla(Clone)":
                    bennyOrders.leftOrder.Add(1);
                    leftOrderText.text += "van, ";
                    return;

                case "ScoopChoc(Clone)":
                    bennyOrders.leftOrder.Add(2);
                    leftOrderText.text += "choc, ";
                    return;

                case "ScoopRockyRoad(Clone)":
                    bennyOrders.leftOrder.Add(3);
                    leftOrderText.text += "rocky road, ";
                    return;

                case "ScoopPistachio(Clone)":
                    bennyOrders.leftOrder.Add(4);
                    leftOrderText.text += "pista, ";
                    return;

                case "ScoopButterscotch(Clone)":
                    bennyOrders.leftOrder.Add(5);
                    leftOrderText.text += "butterscotch, ";
                    return;
                case "ScoopLavender(Clone)":
                    bennyOrders.leftOrder.Add(6);
                    leftOrderText.text += "lav, ";
                    return;
                case "ScoopBlueMoon(Clone)":
                    bennyOrders.leftOrder.Add(7);
                    leftOrderText.text += "blue moon, ";
                    return;
                case "ScoopBlackHole(Clone)":
                    bennyOrders.leftOrder.Add(8);
                    leftOrderText.text += "black hole, ";
                    return;
            }
        }
        else if (transform.parent.name == "ArmR")

        //same thing but for right hand
        {
            print("Added right");

            if (arduino != null && arduino.useArduinoController) //arduino right led blink
            {
                arduino.BlinkRightLED();
            }

            stack_y += scoopHeight * (rightCount);

            numOrder = bennyOrders.rightOrder.Count;

            transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);
            switch (transform.name)
            {
                case "ScoopStrawberry(Clone)":
                    bennyOrders.rightOrder.Add(0);
                    rightOrderText.text += "straw, ";
                    return;

                case "ScoopVanilla(Clone)":
                    bennyOrders.rightOrder.Add(1);
                    rightOrderText.text += "van, ";
                    return;

                case "ScoopChoc(Clone)":
                    bennyOrders.rightOrder.Add(2);
                    rightOrderText.text += "choc, ";
                    return;

                case "ScoopRockyRoad(Clone)":
                    bennyOrders.rightOrder.Add(3);
                    rightOrderText.text += "rocky road, ";
                    return;

                case "ScoopPistachio(Clone)":
                    bennyOrders.rightOrder.Add(4);
                    rightOrderText.text += "pista, ";
                    return;

                case "ScoopButterscotch(Clone)":
                    bennyOrders.rightOrder.Add(5);
                    rightOrderText.text += "butterscotch, ";
                    return;
                case "ScoopLavender(Clone)":
                    bennyOrders.rightOrder.Add(6);
                    rightOrderText.text += "lav, ";
                    return;
                case "ScoopBlueMoon(Clone)":
                    bennyOrders.rightOrder.Add(7);
                    rightOrderText.text += "blue moon, ";
                    return;
                case "ScoopBlackHole(Clone)":
                    bennyOrders.rightOrder.Add(8);
                    rightOrderText.text += "black hole, ";
                    return;
            }
        }
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, -3.44f, 0);
            spawner.timeBetween = 1;
        }
        else
        {
            rb.velocity = new Vector3(0, -1.77f, 0);
            spawner.timeBetween = 2;
        }

        //deletes the ice cream after 5 seconds if the collision is the floor
        if (collision != null)
        {
            if (collision.CompareTag("Floor") || collision.CompareTag("Fallen")) { 
                // print("Landed on ground");
                gameObject.tag = "Fallen";
                timeRemaining -= Time.deltaTime;

                //you can change the time if you want it was just for debugging
                if (timeRemaining <= 4)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //allows outside functions to see the collision
        collision = other.gameObject;

        //checks if the object collided with one of benny's arms
        if (other.transform.CompareTag("BennyArm") && !landed)
        {
            //makes it so that it can't land on anything else
            landed = true;

            //stops all movement
            rb.isKinematic = true;

            transform.SetParent(other.transform);

            //depending on the scoop add it to the list of benny scoops
            addScoop();

        //if it lands on another ice cream scoop
        } else if (other.transform.CompareTag("Scoop") && !landed)
        {
            landed = true;

            rb.isKinematic = true;
            transform.SetParent(other.transform.parent);

            addScoop();
        }
    }   
}
