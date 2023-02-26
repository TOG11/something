using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void Awake()
    {
        transform.position = Camera.main.transform.position;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
