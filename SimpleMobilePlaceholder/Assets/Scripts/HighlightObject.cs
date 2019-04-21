using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material gazedAtMaterial;
    private Renderer myRenderer;
    private Material[] defaultMaterials;
    public bool highlight;
    public ClothingObject clothingScript;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        defaultMaterials = myRenderer.materials;
    }

    private void Update()
    {
        if (highlight)
        {
            SetGazedAt(true);
            highlight = !highlight;
        }
    }


    public void SetGazedAt(bool gazedAt)
    {
        //stop early if clothing isn't highlightable
        if (gazedAt && clothingScript != null && !clothingScript.AllowHighlight())
        {
            return;
        }

        Material[] tempMaterials = myRenderer.materials;
        if (gazedAtMaterial != null)
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                tempMaterials[i] = gazedAt ? gazedAtMaterial : defaultMaterials[i];
            }
        }
        myRenderer.materials = tempMaterials;
    }
}
