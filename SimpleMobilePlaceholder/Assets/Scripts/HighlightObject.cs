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
    private Material transparentMat;

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

        SetHighlightMaterial(gazedAt);
    }

    public void SetHighlightMaterial (bool highlight)
    {
        Material[] tempMaterials = myRenderer.materials;
        if (gazedAtMaterial != null)
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                if (!tempMaterials[i].name.Contains("Transparent"))
                {
                    tempMaterials[i] = highlight ? gazedAtMaterial : defaultMaterials[i];
                }
            }
        }
        myRenderer.materials = tempMaterials;
    }

    public void SetMaterial (Material newMat)
    {
        Material[] tempMaterials = myRenderer.materials;
        if (gazedAtMaterial != null)
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                if (tempMaterials[i].name.Contains("primary"))
                {
                    tempMaterials[i] = newMat;
                    defaultMaterials[i] = newMat;
                }
                
            }
        }
        myRenderer.materials = tempMaterials;
    }

    public void SetBearSkin (Material bearSkinMat)
    {
        Material[] tempMaterials = myRenderer.materials;
        if (gazedAtMaterial != null)
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                if (tempMaterials[i].name.Contains("Transparent"))
                {
                    transparentMat = tempMaterials[i];
                    tempMaterials[i] = bearSkinMat;
                    defaultMaterials[i] = bearSkinMat;
                }

            }
        }
        myRenderer.materials = tempMaterials;
    }

    public void ResetTransparentMat ()
    {
        if (transparentMat != null)
        {
            Material[] tempMaterials = myRenderer.materials;
            if (gazedAtMaterial != null)
            {
                for (int i = 0; i < defaultMaterials.Length; i++)
                {
                    if (tempMaterials[i].name.Contains("bear"))
                    {
                        tempMaterials[i] = transparentMat;
                        defaultMaterials[i] = transparentMat;
                    }

                }
            }
            myRenderer.materials = tempMaterials;
        }
       
    }
    
}
