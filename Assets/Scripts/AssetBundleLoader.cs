using System;
using System.IO;
using UnityEngine;


namespace WeatherForeCasting
{
    public class AssetBundleLoader : MonoBehaviour
    {
        [Header("Asset Bundle Settings")]
        //public string assetBundlePath;
        public string assetName;

        private void Start()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Windows/forecastcanvas"));
                    Debug.Log("Running on Windows.");
                    break;
                case RuntimePlatform.WindowsEditor:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Windows/forecastcanvas"));
                    Debug.Log("Running on Windows.");
                    break;

                case RuntimePlatform.OSXEditor:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Mac/forecastcanvas"));
                    Debug.Log("Running on Mac.");
                    break;
                case RuntimePlatform.OSXPlayer:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Mac/forecastcanvas"));
                    Debug.Log("Running on Mac.");
                    break;

                case RuntimePlatform.Android:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Android/forecastcanvas"));
                    Debug.Log("Running on Android.");
                    break;

                case RuntimePlatform.IPhonePlayer:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/IOS/forecastcanvas"));
                    Debug.Log("Running on iOS.");
                    break;

                default:
                    LoadAssetBundle(Path.Combine(Application.streamingAssetsPath, "SceneAssets/Mac/forecastcanvas"));
                    Debug.Log("Running on other platform." + Application.platform);
                    break;
            }
        }

        private void LoadAssetBundle(string assetBundlePath)
        {
            AssetBundle assetBundle = null;

            assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

            if (assetBundle == null)
            {
                Debug.LogError("Failed to load asset bundle from " + assetBundlePath);
                return;
            }

            GameObject loadedAsset = assetBundle.LoadAsset<GameObject>(assetName);

            if (loadedAsset != null)
            {
                Instantiate(loadedAsset, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError($"Failed to load asset: {assetName}");
            }

            assetBundle.Unload(false); // Unload the asset bundle when done, set to 'true' to unload all loaded objects as well
        }
    }
}
