using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string scene_name;

    public void load_scene()
    {
        SceneManager.LoadScene(scene_name);
    }
}
