using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIController
{
    public class MenuLogic : MonoBehaviour
    {

        [Header("Main Menu Components")]
        public GameObject mainMenu;
        public GameObject optionMenu;

        [Header("Option Mena Components")]
        public Slider MasterSound;
        public Slider MusicSound;
        public Slider SFXSound;

        [Header("Menu AudioSources")]
        public AudioSource myListen;
        public AudioClip switchAudio;
        public AudioClip selectAudio;
        
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            myListen = EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>();
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
        void SaveSettings()
        {
            
        }
        void SetFullScreen(bool isFullscreen)
        {
            
        }
        public void GameExit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }


        public void HoverPlay()
        {
            myListen.PlayOneShot(switchAudio);
        }
        public void SelectPlay()
        {
            myListen.PlayOneShot(selectAudio);
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
    }
}