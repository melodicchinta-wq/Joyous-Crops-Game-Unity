using UnityEngine;

public class BGMFader : MonoBehaviour
{
    public static BGMFader Instance;

    public AudioSource musicSource;
    public float fadeSpeed = 1f;

    private float targetVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.MoveTowards(
                musicSource.volume,
                targetVolume,
                fadeSpeed * Time.deltaTime
            );
        }
    }

    public void FadeOutBGM()
    {
        targetVolume = 0f;
    }

    public void FadeInBGM()
    {
        targetVolume = 1f;
    }
}
