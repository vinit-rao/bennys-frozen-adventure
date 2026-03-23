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

    //freeze ice cream in every direction but down
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

            //stacks it to y = 2.25, adds the average scoop height multiplied by how many scoops there are in that hand
            stack_y += scoopHeight * (leftCount);
            print(stack_y);

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
            }
        }
        else if (transform.parent.name == "ArmR")

        //same thing but for right hand
        {
            print("Added right");

            stack_y += scoopHeight * (rightCount);
            print(stack_y);

            numOrder = bennyOrders.rightOrder.Count;

            transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);
            switch (transform.name)
            {
                case "ScoopStrawberry(Clone)":
                    bennyOrders.rightOrder.Add(0);
                    rightOrderText.text += "straw, ";
                    break;

                case "ScoopVanilla(Clone)":
                    bennyOrders.rightOrder.Add(1);
                    rightOrderText.text += "van, ";
                    break;

                case "ScoopChoc(Clone)":
                    bennyOrders.rightOrder.Add(2);
                    rightOrderText.text += "choc, ";
                    break;
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
                print("Landed on ground");
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
