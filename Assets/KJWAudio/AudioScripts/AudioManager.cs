using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BgSound;
    public AudioSource[] SFXSound;


    //����� �ٲ� �� GMTest.Instance.audioManager.

    public void SFXPlay(string sfxName, AudioClip clip) 
    {
        GameObject AudioGo = new GameObject( sfxName+"Sound" );
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(audiosource, clip.length );
    }
    //GMTest.Instance.audioManager.SFXPlay("die", AudioClip);

    public void BgSoundPlay(AudioClip clip) 
    {
        BgSound.clip = clip;
        BgSound.loop = true;
        BgSound.volume = 0.1f;
        BgSound.Play();
    }
    //GMTest.Instance.audioManager.BgSoundPlay(AudioClip);


    //mix ���� ����
}