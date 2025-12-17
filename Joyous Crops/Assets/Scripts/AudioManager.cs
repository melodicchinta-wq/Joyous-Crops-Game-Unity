using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("--- Audio Source ---")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("--- Audio Clip ---")]
    public AudioClip backgroundScene1And2;
    public AudioClip backgroundScene3;
    public AudioClip feedback;
    public AudioClip mencangkul;
    public AudioClip berjalan;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // biar musik ga berhenti saat ganti scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicForScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        PlayMusicForScene(scene.buildIndex);
    }

    void PlayMusicForScene(int sceneIndex)
    {
        AudioClip clipToPlay = backgroundScene1And2;

        if (sceneIndex == 2) // Scene 3 (index 2)
        {
            clipToPlay = backgroundScene3;
        }

        if (musicSource.clip != clipToPlay)
        {
            musicSource.Stop();
            musicSource.clip = clipToPlay;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
