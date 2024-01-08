using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager
{
    private AudioClip _audioClip;
    private AudioSource _bgmSource;
    private AudioMixer _audioMixer;
    private string _bgFilename;

    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@Sound_Root");
            if (root == null)
            {
                root = new GameObject("@Sound_Root");
                Object.DontDestroyOnLoad(root);
            }
            else
            {
                Object.DontDestroyOnLoad(root);
            }

            return root;
        }
    }

    public void Init()
    {
        var go = new GameObject("@BGM");
        _bgmSource = go.AddComponent<AudioSource>();
        go.transform.parent = Root.transform;

        _audioMixer = Resources.Load<AudioMixer>("F2Mixwer");

        SceneManager.sceneLoaded += LoadedsceneEvent;
        BgSoundPlay("BG1", 0.05f);
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "KDH_Obstacle")
        {
            _bgFilename = "BG1";
        }
        else if (scene.name == "99.BJH")
        {
            _bgFilename = "BG3";
        }
        BgSoundPlay(_bgFilename, 0.05f);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioGo = new GameObject(sfxName + "Sound");
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();

        audiosource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
        _audioClip = Resources.Load<AudioClip>("Audios/SFX/"+sfxName);
        if (_audioClip!=null) 
        {
            audiosource.clip = _audioClip;
            audiosource.volume = audioVolume;
            audiosource.Play();

            Object.Destroy(audiosource.gameObject, audiosource.clip.length);
        }        
    }

    public void BgSoundPlay(string BgName, float audioVolume)
    {
        _audioClip = Resources.Load<AudioClip>("Audios/BGM/"+ BgName);
        _bgmSource.clip = _audioClip;
        _bgmSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
        _bgmSource.loop = true;
        _bgmSource.volume = audioVolume;
        _bgmSource.Play();
    }

    //��������
    public void BGSoundVolume() 
    {
        //float bgmsound = _bgmSlider.value;
        //_audioMixer.SetFloat("BGM", bgmsound);
        // _audioMixer.GetFloat("BGM", out float value);
    }
    public void SFXSoundVolume()
    {
        //float sfxsound = _sfxSlider.value;
        //_audioMixer.SetFloat("SFXVolume", sfxsound);
    }
    public void MasterVolume()
    {
        //float mastersound = _masterSlider.value;
        //_audioMixer.SetFloat("Master", mastersound);
    }
}