using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMain : MonoBehaviour {


    public void ClickButtonStart() {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }

    public void ClickButtonExit() {
        Application.Quit();
    }


}
