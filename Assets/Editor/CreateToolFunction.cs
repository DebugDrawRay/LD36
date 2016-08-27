using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class CreateToolFunction
{
    [MenuItem("Assets/Create/ENTOMB/ToolFunction")]
    public static ToolFunction Create()
    {
        ToolFunction asset = ScriptableObject.CreateInstance<ToolFunction>();

        AssetDatabase.CreateAsset(asset, "Assets/NewToolFunction.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
