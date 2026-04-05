using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerThumbnail : MonoBehaviour
{
    private List<Color> skinColors = new List<Color> { new Color(1f, 0.8f, 0.6f),
                                                       new Color(0.8f, 0.6f, 0.4f),
                                                       new Color(0.6f, 0.4f, 0.2f) };
    private List<Color> hairColors = new List<Color> { new Color(0.1f, 0, 0),
                                                       new Color(0.5f, 0.4f, 0.2f),
                                                       new Color(0.2f, 0.1f, 0.1f) };
    private List<Color> eyeColors = new List<Color> { new Color(0.1f, 0.2f, 0.3f),
                                                      new Color(0.1f, 0.2f, 0.1f),
                                                      new Color(0.3f, 0.1f, 0),
                                                      Color.black };
    private List<Color> shirtColors = new List<Color> { new Color(0.8f, 0.4f, 0.4f),
                                                        new Color(0.6f, 0.5f, 0.2f),
                                                        new Color(0.3f, 0.4f, 0.3f),
                                                        new Color(0.2f, 0.4f, 0.6f) };
    // Start is called before the first frame update
    void Start()
    {
        // grabs the prefab's skin, hair, shirt and randomize from a list of swatches containing Color()
        Transform skinTransform = transform.Find("customerMesh/skin");
        MeshRenderer skinRenderer = skinTransform != null ? skinTransform.GetComponent<MeshRenderer>() : null;
        MeshRenderer hairRenderer = transform
            .Find("customerMesh/hairBrows").GetComponent<MeshRenderer>();
        MeshRenderer eyeRenderer = transform
            .Find("customerMesh/eyes").GetComponent<MeshRenderer>();
        MeshRenderer shirtRenderer = transform
            .Find("customerMesh/shirt").GetComponent<MeshRenderer>();

        if (skinRenderer != null)
        {
            skinRenderer.material.color = skinColors[Random.Range(0, skinColors.Count)];
        }
        hairRenderer.material.color = hairColors[Random.Range(0, hairColors.Count)];
        eyeRenderer.material.color = eyeColors[Random.Range(0, eyeColors.Count)];
        shirtRenderer.material.color = shirtColors[Random.Range(0, shirtColors.Count)];
    }
}
