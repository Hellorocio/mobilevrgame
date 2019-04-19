using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material gazedAtMaterial;
    private Renderer myRenderer;
    private Material[] defaultMaterials;
    public bool highlight;

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
