using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using ProceduralToolkit.Samples.UI;

namespace UIController
{
    public class MenuLogic : MonoBehaviour
    {

        [Header("Main Menu Components")]
        public GameObject mainMenu;
        public GameObject optionMenu;
        public Button optionButton;

        
        [Header("Option |Video| Components")]
        [SerializeField]
        private TMP_Text resolutionText;

        public Toggle vsync_check;
        public Toggle fullscreenToggle;

        public float GammaCorrection;
        public Slider SliderBright;

        #region OptionsConsts
        private const string RESOLUTION_PREF_KEY = "resolution";
        private const string FULLSCREEN_PREF_KEY = "fullscreen";
        private const string VSYNC_PREF_KEY = "vsync";
        private const string GAMMA_PREF_KEY = "gamma";
        #endregion
        private Resolution[] resolutions;

        private int currentResolutionIndex = 0;

        private int VSYNCcount = 0;




        void Start()
        {
            //bool fullscreentoogle = (PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY) ==1 ) ? true : false;

            resolutions = Screen.resolutions;

            //checkToggle.isOn = fullscreentoogle;

            currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);

            SetResolutionText(resolutions[currentResolutionIndex]);

            LoadSetting();
        }
        // Update is called once per frame
        void Update()
        {
            if (optionMenu.activeSelf == true || SceneManager.GetActiveScene().name == "main")
                CloseOptions();
            OpenMenu();

            


            /*Debug.Log(mbc.index);
            if (Input.GetAxis("Submit") == 1)
            {
                index = mbc.index;
                switch (index)
                {
                    case 0:
                        PlayGame();
                        break;
                    case 3:
                        GameExit();
                        break;
                    default:
                        break;
                }
            }*/
        }
        public void PlayGame()
        {
            StartCoroutine("WaitForLoad");
        }
        public void SelecOptions()
        {
            StartCoroutine("WaitForOptions");
        }
        
        public void GameExit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }

        public IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(.15f);
            SceneManager.LoadScene(1);
        }
        public IEnumerator WaitForOptions()
        {
            yield return new WaitForSeconds(.15f);
            mainMenu.gameObject.SetActive(false);
            optionMenu.SetActive(true);
        }
        public void CloseOptions()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionMenu.SetActive(false);
                mainMenu.SetActive(true);
            }
        }
        public void OpenMenu()
        {
            if(SceneManager.GetActiveScene().name == "main" && Input.GetKeyDown(KeyCode.Escape))
            {
                optionMenu.SetActive(true);
            }
        }

        #region Resolution Functions
        private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
        {
            if (collection.Count < 1) return 0;
            return (currentIndex + 1) % collection.Count;
        }
        private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
        {
            if (collection.Count < 1) return 0;
            if ((currentIndex - 1) < 0) return collection.Count - 1;
            return (currentIndex - 1) % collection.Count;
        }

        private void SetResolutionText(Resolution resolution)
        {
            resolutionText.SetText(resolution.width + "x" + resolution.height);
        }

        public void SetNextResolution()
        {
            currentResolutionIndex = GetNextWrappedIndex(resolutions, currentResolutionIndex);
            SetResolutionText(resolutions[currentResolutionIndex]);
        }
        public void SetPreviousResolution()
        {
            currentResolutionIndex = GetPreviousWrappedIndex(resolutions, currentResolutionIndex);
            SetResolutionText(resolutions[currentResolutionIndex]);
        }

        private void SetAndApplyResolution(int newResolutionIndex)
        {
            currentResolutionIndex = newResolutionIndex;
            ApplyCurrentResolution();
        }

        private void ApplyCurrentResolution()
        {
            ApplyResolution(resolutions[currentResolutionIndex]);
        }
        private void ApplyResolution(Resolution resolution)
        {
            SetResolutionText(resolution);

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            
        }

        public void ApplyChanges()
        {
            SetAndApplyResolution(currentResolutionIndex);
            PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
            PlayerPrefs.SetInt(FULLSCREEN_PREF_KEY, Convert.ToInt32(Screen.fullScreen));
            PlayerPrefs.SetInt(VSYNC_PREF_KEY, VSYNCcount);
        }
        public void ToggleFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void ToggleVsycn()
        {
            QualitySettings.vSyncCount = VSYNCcount;
            if (vsync_check.isOn == true)
            {
                VSYNCcount = 2;
            }
            else
            {
                VSYNCcount = 0;
            }
            QualitySettings.vSyncCount = VSYNCcount;

            Debug.Log(QualitySettings.vSyncCount);

        }

        private void OnGUI()
        {
            GammaCorrection = SliderBright.value;
            RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1);
            Debug.Log(RenderSettings.ambientLight);
        }

        public void LoadSetting()
        {
            if (PlayerPrefs.HasKey(FULLSCREEN_PREF_KEY))
            {
                Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY));
                if (Screen.fullScreen == false)
                    fullscreenToggle.isOn = false;
                else
                    fullscreenToggle.isOn = true;
            }
            else
                Screen.fullScreen = true;
            if (PlayerPrefs.HasKey(VSYNC_PREF_KEY))
            {
                VSYNCcount = PlayerPrefs.GetInt(VSYNC_PREF_KEY);
                if (VSYNCcount == 2)
                    vsync_check.isOn = true;
                else
                    vsync_check.isOn = false;
            }
            else
                VSYNCcount = 0;
        }
        #endregion
    }
}