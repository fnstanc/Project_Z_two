using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class CreateAssetBundle : MonoBehaviour {

    [MenuItem("Tool/SetBundleName")]
    static void SetBundleName()
    {
        Object[] selects = Selection.objects;

        foreach (var select in selects)
        {
            string path = AssetDatabase.GetAssetPath(select);
            AssetImporter importer = AssetImporter.GetAtPath(path);
            importer.assetBundleName = select.name;
            importer.assetBundleVariant = "assetbundle";
            importer.SaveAndReimport();
        }

        AssetDatabase.Refresh();
    }


    [MenuItem("Tool/Build AssetBundle")]
    static void BuildAssetBundles()
    {
        string path = Application.dataPath + "/StreamingAssets";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        AssetDatabase.Refresh();
    }


}
