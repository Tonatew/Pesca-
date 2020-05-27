using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IIntroControllerUI : MonoBehaviour
{
    bool reduceVolume;
    [SerializeField]
    AudioClip bellSound;
    [SerializeField]
    AudioSource musicAudio;
    [SerializeField]
    Image cover;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(reduceVolume){
            musicAudio.volume -= 0.01666f;
        }
    }

    void coverAnimation(){
        cover.fillAmount -= 0.001f;
    }

    public void GoToMenu(){
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.PlayOneShot(bellSound);
        Invoke("LoadMenu", 1);
        reduceVolume = true;
    }

    void LoadMenu(){
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
