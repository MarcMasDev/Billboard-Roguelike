using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    private MenuGroup transition;
    private string toLoad = "";
    private void Awake()
    {
        AudioController.Instance.SetLPF(SceneManager.GetActiveScene().name, 0.1f);
    }
    public void LoadSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            if (transition == null) transition = GetComponent<MenuGroup>();

            transition.SetVisible(true);
            toLoad = sceneName;
        }
    }

    public void LoadScene()
    {
        AudioController.Instance.SetLPF(toLoad);
        SceneManager.LoadScene(toLoad);
    }


}

