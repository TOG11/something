using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance = null;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            transform.position = Camera.main.transform.position;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
