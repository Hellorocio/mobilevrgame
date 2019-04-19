using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material gazedAtMaterial;
    private Renderer myRenderer;
    private Material[] defaultMaterials;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        defaultMaterials = myRenderer.materials;
    }

    
    public void SetGazedAt(bool gazedAt)
    {
        if (gazedAtMaterial != null)
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                myRenderer.materials[i] = gazedAt ? gazedAtMaterial : defaultMaterials[i];
            }
        }
    }
}
