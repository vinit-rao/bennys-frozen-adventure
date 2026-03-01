using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    public float scoopHeight = 0.5f;
    public Rigidbody rb;
    public BennyIceCreamStacker benny;
    public bool landed = false;

    public BennyOrders order;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (landed) return;
        if (!other.transform.CompareTag("ScoopLanded")) return;

        benny = other.transform.GetComponentInParent<BennyIceCreamStacker>();
        if (benny == null) return;

        landed = true;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        benny.stack_y += scoopHeight;
        transform.position = new Vector3(benny.transform.position.x, benny.stack_y, benny.transform.position.z);

        transform.SetParent(benny.transform);
        transform.tag = "ScoopLanded";
    }
}