using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace WeatherForeCasting.Tests
{
    public class AppFlowTest
    {
        private const float SceneLoadDelay = 2.0f;
        private const float ActionDelay = 1.0f;
        private const float ResponseDelay = 5.0f;

        private CommHandler commHandler;
        private Button getWeatherDetailsButton;
        private Button closeButton;
        private Text latitude;
        private Text longitude;
        private Text timeZone;
        private List<Text> dateTempList = new List<Text>();
        private GameObject loadingBar;
        private GameObject popup;
        private ForcastCoreHandler forcastCoreHandler;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("MainScene");
            yield return new WaitForSeconds(SceneLoadDelay);
            InitializeReferences();
            Assert.IsNotNull(forcastCoreHandler.ConfigData, "ConfigData not found");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return null;
        }

        private void InitializeReferences()
        {
            GameObject forcastCanvas = GameObject.Find("ForeCastCanvas(Clone)");
            Assert.IsNotNull(forcastCanvas, "ForeCastCanvas not found");

            forcastCoreHandler = forcastCanvas.GetComponent<ForcastCoreHandler>();
            commHandler = forcastCanvas.GetComponent<CommHandler>();

            latitude = forcastCoreHandler.Latitude;
            longitude = forcastCoreHandler.Longitude;
            getWeatherDetailsButton = forcastCoreHandler.GetWeatherButton;
            timeZone = forcastCoreHandler.TimeZone;
            loadingBar = forcastCoreHandler.Loader.gameObject;
            popup = forcastCoreHandler.Toast.gameObject;
            closeButton = forcastCoreHandler.CloseButton;

            for(int i = 0; i < forcastCoreHandler.DateTempArray.Length; i++)
            {
                dateTempList.Add(forcastCoreHandler.DateTempArray[i]);
            }
        }

        [UnityTest]
        public IEnumerator GetWeatherStatusFlow()
        {
            // Arrange
            Assert.IsNotNull(getWeatherDetailsButton, "GetWeatherDetailsButton not found");

            // Act
            getWeatherDetailsButton.onClick.Invoke();
            yield return null;

            // Assert
            AssertObjectActivity(loadingBar, true, "Loading bar");
            yield return new WaitForSeconds(ResponseDelay);
            AssertDeserializationSuccessful();
            AssertObjectActivity(loadingBar, false, "Loading bar");
            AssertObjectActivity(popup, true, "Popup");
            CompareWeatherDataWithTextComponents(forcastCoreHandler.WeatherData);

            yield return null;
            Assert.IsNotNull(closeButton, "CloseButton not found");
            closeButton.onClick.Invoke();

        }

        private void CompareWeatherDataWithTextComponents(JsonModelClass.WeatherData data)
        {
            Assert.AreEqual(data.Latitude.ToString(), latitude.text, "Latitude value does not match.");
            Assert.AreEqual(data.Longitude.ToString(), longitude.text, "Longitude value does not match.");
            Assert.AreEqual(data.Timezone, timeZone.text, "Timezone value does not match.");

            for (int i = 0; i < dateTempList.Count; i++)
            {
                string expectedText = $"{data.Daily.Time[i]}   -   Max Temp: {data.Daily.temperature_2m_max[i]}{data.daily_units.temperature_2m_max}";
                Assert.AreEqual(expectedText, dateTempList[i].text, $"Text mismatch at index {i}.");
            }
        }


        private void AssertDeserializationSuccessful()
        {
            Assert.IsNotNull(commHandler, "commHandler is null");
            Assert.IsFalse(commHandler.DeserializationFailed, "Deserialization failed.");
        }

        private void AssertObjectActivity(GameObject gameObject, bool shouldBeActive, string objectName)
        {
            Assert.IsNotNull(gameObject, $"{objectName} is null");
            Assert.AreEqual(shouldBeActive, gameObject.activeSelf, $"{objectName} should be {(shouldBeActive ? "active" : "inactive")}.");
        }

    }
}
