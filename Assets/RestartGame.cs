using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {
    void Start() {
        SceneManager.LoadScene(0);
    }
}
