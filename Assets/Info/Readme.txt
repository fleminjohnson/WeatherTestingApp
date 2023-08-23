# Weather Forecasting SDK

Welcome to the Weather Forecasting SDK! This SDK allows you to effortlessly integrate weather forecasting capabilities into your Unity projects. This README provides user-friendly guidance on getting started with the SDK and highlights its key features.

## Getting Started

1. **Installation**: Incorporate the Weather Forecasting SDK into your Unity project seamlessly:
   - Download the Weather Forecasting SDK Unity package (`WeatherForecastingSDK.unitypackage`).
   - Open your Unity project.
   - Navigate to `Assets` > `Import Package` > `Custom Package`.
   - Choose the downloaded SDK package file and import it.

2. **Initialising the SDK**: Set up the SDK in your scene with ease:
   - Add an empty GameObject to your scene.
   - Attach the `AssetBundleLoader` script to the GameObject.
   - Customise the `Asset Name` field in the inspector to match the asset you want to instantiate from the asset bundle.

3. **Platform-Specific Asset Bundles**: The SDK automatically loads the appropriate asset bundle based on your current platform. No manual handling of different platform bundles is needed.

## Usage

1. **Forecasting Scene**: After configuring the `AssetBundleLoader` script, run your scene to experience the Weather Forecasting UI in action. The pre-designed UI displays weather data in an intuitive manner.

2. **Customisation**: While the SDK offers a user-friendly UI, you can seamlessly customise it to align with your project's design. Modify the assets within the provided asset bundle or design your own assets to replace them.

## Asset Bundles and Configuration

The Weather Forecasting SDK package includes the essential asset bundles to ensure a seamless integration experience. These asset bundles are conveniently located within the SDK package under the `StreamingAssets` folder:

- `StreamingAssets/SceneAssets/Windows/forecastcanvas` (for Windows platforms)
- `StreamingAssets/SceneAssets/Mac/forecastcanvas` (for macOS platforms)
- `StreamingAssets/SceneAssets/Android/forecastcanvas` (for Android platforms)
- `StreamingAssets/SceneAssets/IOS/forecastcanvas` (for iOS platforms)

Additionally, the SDK configuration is located at:
- `StreamingAssets/Config/appConfig.json`

You don't need to download or manage these asset bundles separately. Simply ensure they are in the right paths within your project.

## Configuration

1. **Config Data**: To configure settings such as request timeouts, the SDK provides a straightforward method using a JSON configuration file:
   - Open the `Config` folder in the Streaming Assets directory.
   - Locate the `appConfig.json` file and adjust the `RequestTimeout` field to your desired timeout duration in seconds.


## Version History

- **Version X.Y.Z** (Release Date):
