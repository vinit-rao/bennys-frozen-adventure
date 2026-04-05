using UnityEngine;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class IceCreamScript : MonoBehaviour
{
    public float scoopHeight = 0.5f;
    public Rigidbody rb;
    public bool landed = false;
    public BennyOrders bennyOrders;
    public int numOrder;
    public IceCreamSpawner spawner;

    private float timeRemaining = 5;
    GameObject collision = null;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;
    private ArduinoController arduino;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bennyOrders = GameObject.FindWithTag("Player").GetComponent<BennyOrders>();
        //freeze ice cream in every direction but falling down
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        arduino = FindObjectOfType<ArduinoController>();
    }

    private bool getScoopId(string scoopName, out int flavourId, out string flavourText)
    {
        flavourId = -1;
        flavourText = "";
        switch (scoopName)
        {
            case "ScoopStrawberry(Clone)":
                flavourId = 0;
                flavourText = "straw";
                return true;
            case "ScoopChoc(Clone)":
                flavourId = 1;
                flavourText = "choc";
                return true;
            case "ScoopVanilla(Clone)":
                flavourId = 2;
                flavourText = "van";
                return true;
            case "ScoopRockyRoad(Clone)":
                flavourId = 0;
                flavourText = "rocky road";
                return true;
            case "ScoopPistachio(Clone)":
                flavourId = 1;
                flavourText = "pista";
                return true;
            case "ScoopButterscotch(Clone)":
                flavourId = 2;
                flavourText = "butterscotch";
                return true;
            case "ScoopLavender(Clone)":
                flavourId = 0;
                flavourText = "lav";
                return true;
            case "ScoopBlueMoon(Clone)":
                flavourId = 1;
                flavourText = "blue moon";
                return true;
            case "ScoopBlackHole(Clone)":
                flavourId = 2;
                flavourText = "black hole";
                return true;
        }
        return false;
    }

    private void scoopLandOnHand(System.Collections.Generic.List<int> handOrder, int scoopCount, bool isLeft)
    {
        float stack_y = 2.5f + scoopHeight * scoopCount;
        transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);

        if (getScoopId(transform.name, out int flavourId, out string flavourText))
        {
            handOrder.Add(flavourId);
            numOrder = handOrder.Count;
        }

        if (arduino != null && arduino.useArduinoController)
        {
            if (isLeft) arduino.BlinkLeftLED();
            else arduino.BlinkRightLED();
        }
    }

    private void addScoop()
    {
        Transform parent = transform.parent;
        if (parent == null) return;

        Transform light = transform.Find("Spot Light(Clone)");
        if (light != null)
            Destroy(light.gameObject);

        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (parent.name == "ArmL")
        {
            scoopLandOnHand(bennyOrders.leftOrder, bennyOrders.leftOrder.Count, true);
        }
        else if (parent.name == "ArmR")
        {
            scoopLandOnHand(bennyOrders.rightOrder, bennyOrders.rightOrder.Count, false);
        }
    }

    private void Update()
    {
        rb.velocity = new Vector3(0, -DifficultyManager.fallSpeed, 0);
        //deletes the ice cream after 5 seconds if the collision is the floor
        if (collision != null)
        {
            if (collision.CompareTag("Floor") || collision.CompareTag("Fallen"))
            {
                gameObject.tag = "Fallen";
                timeRemaining -= Time.deltaTime;

                if (timeRemaining <= 4)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        collision = other.gameObject;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayCatchSound();

        if (other.transform.CompareTag("BennyArm") && !landed)
        {
            landed = true;
            gameObject.tag = "ScoopLanded";

            rb.isKinematic = true;
            transform.SetParent(other.transform);

            addScoop();
        }
        else if (other.transform.CompareTag("ScoopLanded") && !landed)
        {
            landed = true;
            gameObject.tag = "ScoopLanded";

            rb.isKinematic = true;
            transform.SetParent(other.transform.parent);

            addScoop();
        }
    }
}