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

        //Assign initial volumes
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        //Get base volume
        float baseVolume = 1;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("soundVolume", 1); //Load last saved data
        currentVolume += _change;

        //Maximum or minimum volume
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        soundSource.volume = finalVolume;

        //Save final value
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicVolume(float _change)
    {
        //Get base volume
        float baseVolume = 0.3f;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("musicVolume", 1); //Load last saved data
        currentVolume += _change;

        //Maximum or minimum volume
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        musicSource.volume = finalVolume;

        //Save final value
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}
