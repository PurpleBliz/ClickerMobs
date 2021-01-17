using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public void GoTo(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}