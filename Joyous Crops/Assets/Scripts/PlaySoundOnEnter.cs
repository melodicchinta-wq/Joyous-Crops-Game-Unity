using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    private AudioSource source;
    private Coroutine fadeCoroutine;

    [Header("Fade Settings")]
    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 1f;
    public float targetVolume = 1f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;   // mulai dari sunyi
        source.loop = true;   // musik terus berulang
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // hentikan coroutine lama
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            if (!source.isPlaying) source.Play();
            fadeCoroutine = StartCoroutine(FadeToVolume(targetVolume, fadeInSpeed));

            // fade out background
            if (BGMFader.Instance != null) BGMFader.Instance.FadeOutBGM();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToVolume(0f, fadeOutSpeed, stopAfterFade: true));

            // fade in background
            if (BGMFader.Instance != null) BGMFader.Instance.FadeInBGM();
        }
    }

    private System.Collections.IEnumerator FadeToVolume(float target, float speed, bool stopAfterFade = false)
    {
        while (!Mathf.Approximately(source.volume, target))
        {
            source.volume = Mathf.MoveTowards(source.volume, target, speed * Time.deltaTime);
            yield return null;
        }

        source.volume = target;

        if (stopAfterFade) source.Stop();
    }
}
