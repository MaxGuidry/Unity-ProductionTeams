 
using UnityEditor; 

public class DontImportMaterials : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        var modelImporter = (ModelImporter)assetImporter;
        modelImporter.importMaterials = false;
    }
}
