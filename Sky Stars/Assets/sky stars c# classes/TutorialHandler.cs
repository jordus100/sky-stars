using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour {

	public void continueButtonClick()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
