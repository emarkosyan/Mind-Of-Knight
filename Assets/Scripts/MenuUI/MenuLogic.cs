using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIController
{
    public class MenuLogic : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject optionMenu;
        public Slider MasterSound;
        public Slider MusicSound;
        public Slider SFXSound;
        MenuButtonController mbc;
        MenuButton mb;
        int index;
        
        void Start()
        {
            mbc = GetComponent<MenuButtonController>();
            mb = GetComponentInChildren<MenuButton>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(mbc.index);
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
            }
        }
        void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
        void SelecOptions()
        {
            mainMenu.gameObject.SetActive(false);
            optionMenu.SetActive(true);
        }
        void SaveSettings()
        {
            
        }
        void SetFullScreen(bool isFullscreen)
        {
            
        }
        void GameExit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }
}