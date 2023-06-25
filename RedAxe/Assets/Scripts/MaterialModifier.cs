using System.Collections.Generic;
using UnityEngine;
public static class MaterialModifier
{
    public static void ChangeNormalMap(float value, List<Renderer> objectRenderers, Material targetMaterial, List<int> materialIndex)
    {
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            // Get the materials array from the Renderer
            Material[] materials = objectRenderers[i].materials;

            // Ensure the materialIndex is within bounds
            if (materialIndex[i] >= 0 && materialIndex[i] < materials.Length)
            {
                // Create a new instance of the material
                Material newMaterial = new Material(materials[materialIndex[i]]);

                // Modify the normal map value (assuming "_BumpScale" is the name of the normal map property)
                newMaterial.SetFloat("_BumpScale", value*2);

                // Assign the new material to the specific material index
                materials[materialIndex[i]] = newMaterial;

                // Update the materials array in the Renderer
                objectRenderers[i].materials = materials;
            }
            else
            {
                Debug.LogWarning("Material index out of range for Renderer: " + objectRenderers[i].name);
            }
        }
    }
    public static void ChangeMaterialColor(List<Renderer> objectRenderers, Material targetMaterial, List<int> materialIndex)
    {
        const float paintPercentage = 0.15f;
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            // Get the materials array from the Renderer
            Material[] materials = objectRenderers[i].materials;

            // Ensure the materialIndex is within bounds
            if (materialIndex[i] >= 0 && materialIndex[i] < materials.Length)
            {
                // Create a new instance of the material
                Material newMaterial = new Material(materials[materialIndex[i]])
                {
                    // Modify the color
                    color = new Color(materials[materialIndex[i]].color.r*(1-paintPercentage), materials[materialIndex[i]].color.g*(1-paintPercentage), materials[materialIndex[i]].color.b*(1-paintPercentage), 1)
                };

                // Assign the new material to the specific material index
                materials[materialIndex[i]] = newMaterial;

                // Update the materials array in the Renderer
                objectRenderers[i].materials = materials;
            }
            else
            {
                Debug.LogWarning("Material index out of range for Renderer: " + objectRenderers[i].name);
            }
        }
    }
}
