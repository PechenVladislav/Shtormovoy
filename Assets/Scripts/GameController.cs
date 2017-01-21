using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController instance;

	// Use this for initialization
	void Awake ()
    {
        instance = this;
	}
	
	public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static GameController Instance
    {
        get { return instance; }
    }
}
