using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIGame : MonoBehaviour {

    public static UIGame instance;

    public GameObject WindowEndGame;
    public GameObject GameInfoUI;
    public Text Score;

    public AudioSource AudioSource;
    public List<AudioClip> sounds = new List<AudioClip>();

    void Awake() {
        if (instance != null && instance != this) {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;       
    }

    public void ClickButtonMain() {
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
    public void ClickButtonRestart() {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }

    public void EndGame() {
        WindowEndGame.SetActive(true);
        GameInfoUI.SetActive(false);
        CharacterController.instance.enabled = false;
    }

    public void Collect(GameObject go) {
        Debug.Log("Collect");
        if (go.name == "Exit") {
            EndGame();
        }
        Destroy(go);
       
        int value = int.Parse(Score.text);
        value++;
        Score.text = value.ToString();

        AudioSource.PlayOneShot(sounds[0]);
    }

}
