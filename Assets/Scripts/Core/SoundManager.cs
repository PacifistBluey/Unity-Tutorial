using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Keep this object even when we go to a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("soundVolume"); //Load last saved data
        currentVolume += _change;

        //Maximum or minimum volume
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        soundSource.volume = currentVolume;

        //Save final value
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicVolume(float _change)
    {
        float currentVolume = 1;
        currentVolume += _change;
        musicSource.volume = currentVolume;
    }
}
