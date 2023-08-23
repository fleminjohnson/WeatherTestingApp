using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherForeCasting
{
    public class ForcastCoreHandler : SingletonBehaviour<ForcastCoreHandler>
    {
        private CommHandler commHandler;
        private JsonModelClass.WeatherData weatherData;
        private bool responsePending = false;
        private float requestTimeout = 10;
        private JsonModelClass.ConfigData configData;

        [SerializeField]
        private Text[] dateTempArray;

        [SerializeField]
        private Image toast;
        [SerializeField]
        private Image loader;

        [SerializeField]
        private Text latitude;
        [SerializeField]
        private Text longitude;
        [SerializeField]
        private Text timeZone;

        [SerializeField]
        private Button getWeatherButton;
        [SerializeField]
        private Button closeButton;

        public Text[] DateTempArray { get => dateTempArray; }
        public Image Toast { get => toast; }
        public Image Loader { get => loader; }
        public Text Latitude { get => latitude; }
        public Text Longitude { get => longitude; }
        public Text TimeZone { get => timeZone; }
        public Button GetWeatherButton { get => getWeatherButton; }
        public JsonModelClass.WeatherData WeatherData { get => weatherData; }
        public Button CloseButton { get => closeButton; }
        public JsonModelClass.ConfigData ConfigData { get => configData;}


        [SerializeField]
        private float rotationSpeed;

        // API endpoint for weather forecast
        private string weatherApiUrl = "https://api.open-meteo.com/v1/forecast?latitude=19.07&longitude=72.87&timezone=IST&daily=temperature_2m_max";

        protected override void Awake()
        {
            Debug.Log("Inside awake method");
            base.Awake();
            commHandler = GetComponent<CommHandler>();
        }

        private void Start()
        {
            // Load JSON data from the StreamingAssets folder
            string filePath = Path.Combine(Application.streamingAssetsPath, "Config/appConfig.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                configData = JsonUtility.FromJson<JsonModelClass.ConfigData>(jsonContent);
                requestTimeout = ConfigData != null ? ConfigData.RequestTimeout : requestTimeout;
            }
            else
            {
                Debug.LogError("Timeout configuration file not found!");
            }
        }

        bool isAnimation = false;

        private void Update()
        {
            if (isAnimation)
                loader.rectTransform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }

        private IEnumerator GetData()
        {
            bool hasTimedOut = false;
            float elapsedTime = 0f;

            // Send the API request
            yield return StartCoroutine(commHandler.GetJsonData(weatherApiUrl, (result) =>
            {
                StopLoaderAnimation();
                Debug.Log("result is " + result);
                // Deserialize the received JSON data
                toast.gameObject.SetActive(true);
                weatherData = commHandler.Deserialize<JsonModelClass.WeatherData>(result);

                if (weatherData != null)
                {
                    Debug.Log("Weather data fetched successfully!");
                    Debug.Log($"Latitude: {weatherData.Latitude}");
                    Debug.Log($"Longitude: {weatherData.Longitude}");

                    latitude.text = weatherData.Latitude.ToString();
                    longitude.text = weatherData.Longitude.ToString();
                    timeZone.text = weatherData.Timezone.ToString();

                    for (int i = 0; i < dateTempArray.Length; i++)
                    {
                        dateTempArray[i].text = $"{weatherData.Daily.Time[i]}   -   Max Temp: { weatherData.Daily.temperature_2m_max[i]}{weatherData.daily_units.temperature_2m_max}";
                    }
                    hasTimedOut = false;
                }
                else
                {
                    Debug.LogError("Weather data deserialization failed!");
                }
                responsePending = false;
            }));

            // Start the timeout timer
            while (!hasTimedOut && elapsedTime < requestTimeout)
            {
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            if (hasTimedOut)
            {
                Debug.LogError("API request timed out!");
                // Handle the timeout scenario, such as showing an error message to the user
            }
        }

        public void CloseApplication()
        {
            Debug.Log("Application is closing...");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void GetWeatherData()
        {
            if (!responsePending)
            {
                responsePending = true;
                StartCoroutine(GetData());
                StartLoaderAnimation();
            }
        }

        public void StartLoaderAnimation()
        {
            loader.gameObject.SetActive(true);
            isAnimation = true;
        }

        public void StopLoaderAnimation()
        {
            loader.gameObject.SetActive(false);
            isAnimation = false;
        }
    }
}
