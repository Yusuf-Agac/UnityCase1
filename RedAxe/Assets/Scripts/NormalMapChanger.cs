
using UnityEngine;

public class NormalMapChanger : MonoBehaviour
{
    public Renderer objectRenderer;
    public Material targetMaterial;
    private void ChangeNormalMap(float value)
    {
        // Create a new instance of the material
        Material newMaterial = new Material(targetMaterial);
        
        // Modify the normal map value (assuming "_BumpMap" is the name of the normal map property)
        newMaterial.SetFloat("_BumpScale", value);
        
        // Assign the modified material to the renderer
        objectRenderer.material = newMaterial;
        
    }
}
