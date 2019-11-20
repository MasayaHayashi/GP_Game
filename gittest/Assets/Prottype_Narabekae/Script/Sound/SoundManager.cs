using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //====== enum ========
    //--- ゲームSE ----
    public enum eGameSE {
        takeItem,           //アイテムを持つ・交換する
        putItem,            //アイテムを置く
        itemSpawn,       //アイテムスポーン演出
        disturbBug,             //お邪魔アイテムでレーンにバグが起きた！
        correctPressPos,      //正解のアイテムがセットされた
        missPressPos,         //不正解のアイテムがセットされた
        inDust,                   //アイテムをごみ箱に捨てた
    }


    [SerializeField] bool dontDestroyOnLoad = true;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] string manageTag = "";
    public string ManageTag { get { return manageTag; }  }

    AudioSource audioSource;

    public static List<SoundManager> soundManagerInstanceList = new List<SoundManager>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
        soundManagerInstanceList.Add(this);
    }

    private void OnDestroy()
    {
        soundManagerInstanceList.Remove(this);
    }

    void OnApplicationQuit()
    {
        Destroy(gameObject);
    }


    //----- SEの再生 ------
    public bool PlayOneShot(int index)
    {
        if (audioClips.Length <= index)
            return false;
        audioSource.PlayOneShot(audioClips[index]);
        return true;
    }

    public bool PlayOneShot(string name)
    {
        for(int i = 0; i < audioClips.Length; i++)
        {
            if (audioClips[i].name != name)
                continue;
            audioSource.PlayOneShot(audioClips[i]);
            return true;
        }
        return false;
    }

    public bool PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        return true;
    }

    //----- BGMの再生 ------
    public bool Play(int index)
    {
        if (audioClips.Length <= index)
            return false;
        audioSource.clip = audioClips[index];
        audioSource.Play();
        return true;
    }

    public bool Play(string name)
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (audioClips[i].name != name)
                continue;
            audioSource.clip = audioClips[i];
            audioSource.Play();
            return true;
        }
        return false;
    }

    public bool Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        return true;
    }

    //------ ボリュームの設定 -------
    public void SetAudioVolume(float vol)
    {
        audioSource.volume = vol;
    }
}
