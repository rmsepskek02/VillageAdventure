using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;
    public AudioClip[] bgmClips; // �پ��� BGM�� ������ �迭

    private float volume; // �ʱ� ���� ����

    // �ʱ�ȭ
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

    // BGM ���
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

    // ���� ����
    public void SetVolume(float volume)
    {
        this.volume = volume;
        bgmSource.volume = volume;
    }

    // ���� ��������
    public float GetVolume()
    {
        return volume;
    }
}
