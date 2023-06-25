using System.Collections.Generic;
using UnityEngine;

public static class MaterialModifier
{
    public static void ChangeNormalMap(float value, List<Renderer> objectRenderers, List<int> materialIndex)
    {
        const float damageMultiplier = 1.5f;
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            Material[] materials = objectRenderers[i].materials;
            Material newMaterial = new Material(materials[materialIndex[i]]);
            
            // Modify the normal map value
            newMaterial.SetFloat("_BumpScale", value * damageMultiplier);

            materials[materialIndex[i]] = newMaterial;
            objectRenderers[i].materials = materials;
        }
    }

    public static void ChangeMaterialColor(List<Renderer> objectRenderers, List<int> materialIndex)
    {
        const float paintPercentage = 0.20f;
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            Material[] materials = objectRenderers[i].materials;
            Material newMaterial = new Material(materials[materialIndex[i]])
            {
                color = new Color(materials[materialIndex[i]].color.r * (1 - paintPercentage),
                    materials[materialIndex[i]].color.g * (1 - paintPercentage),
                    materials[materialIndex[i]].color.b * (1 - paintPercentage), 1)
            };
            materials[materialIndex[i]] = newMaterial;
            objectRenderers[i].materials = materials;
        }
    }
}