using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    public GameObject overlay = null;
    private bool overlay_enabled = false;

    private void Start()
    {
        overlay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            overlay_enabled = !overlay_enabled;
            overlay.SetActive(overlay_enabled);
        }
    }
}
