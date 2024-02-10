using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;
    public AudioClip[] bgmClips; // 다양한 BGM을 저장할 배열

    private float volume; // 초기 볼륨 설정

    // 초기화
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        if (PlayerPrefs.GetInt("IsMute") == 1)
        {
            SetVolume(volume);
        }
        else if (PlayerPrefs.GetInt("IsMute") == 0)
        {
            SetVolume(0);
        }
        bgmSource.clip = bgmClips[0];
        bgmSource.Play();
    }

    // BGM 재생
    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
        {
            Debug.LogWarning("Invalid BGM index!");
            return;
        }

        bgmSource.clip = bgmClips[index];
        bgmSource.Play();
    }

    // 볼륨 조절
    public void SetVolume(float volume)
    {
        this.volume = volume;
        bgmSource.volume = volume;
    }

    // 볼륨 가져오기
    public float GetVolume()
    {
        return volume;
    }
}
