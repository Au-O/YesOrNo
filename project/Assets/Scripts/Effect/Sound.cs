using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private Sound m_instance;

    public AudioSource m_Bg;
    public AudioSource m_effect;

    public AudioClip bgclip;
    public AudioClip effclip;

    public float soundValue = 1;
    public float bgValue = 1;
    public float effectValue = 1;

    public Sound Instance { get => m_instance; set => m_instance = value; }

    protected void Awake()
    {
        m_instance = this;
        m_Bg = gameObject.AddComponent<AudioSource>();
        m_Bg.playOnAwake = false;
        m_Bg.loop = true;

        m_effect = gameObject.AddComponent<AudioSource>();
        m_Bg.clip = bgclip;
        m_effect.clip = effclip;
    }
    private void Update()
    {
        //if (m_Bg != null && m_Bg.isPlaying == false)
        //{
        //    m_Bg.Play();
        //}
    }
    //���ű�������
    public void PlayBG(string audioName, float value = 1)
    {
        m_Bg.Stop();
        string BGDir = "BGM";
        value = m_Bg.volume = soundValue * bgValue;
        string oldName = "";
        if (m_Bg.clip)
        {
            oldName = m_Bg.clip.name;
        }
        if (oldName != audioName)
        {
            //������Դ
            string path = BGDir + "/" + audioName;
            bgclip = Resources.Load<AudioClip>(path);

            //����
            if (bgclip)
            {
                m_Bg.volume = value;
                m_Bg.clip = bgclip;
                m_Bg.Play();
            }
        }
        else
        {
            //������Դ
            string path = BGDir + "/" + oldName;
            bgclip = Resources.Load<AudioClip>(path);

            //����
            if (bgclip)
            {
                m_Bg.volume = value;
                m_Bg.clip = bgclip;
                m_Bg.Play();
            }
        }
    }

    //������Ч
    public void PlayEffect(string audioName, float value = 1)
    {
        StopPlayEffect();
        string SoundDir = "Sound";
        value = soundValue * effectValue;
        //������Դ
        string path = SoundDir + "/" + audioName;
        effclip = Resources.Load<AudioClip>(path);

        //����
        if (effclip)
        {
            m_effect.volume = value;
            m_effect.PlayOneShot(effclip);
        }
    }

    //ֹͣ������Ч
    public void StopPlayEffect()
    {
        m_effect.Stop();
    }

    //ֹͣ��������
    public void StopAllSound()
    {
        if (m_Bg)
        {
            m_Bg.Stop();
        }
        if (m_effect)
        {
            m_effect.Stop();
        }
    }

    public void ChangeSoundValue(float sValue)
    {
        soundValue = sValue;
        m_Bg.volume = soundValue * bgValue;
        m_effect.volume = soundValue * effectValue;
    }

    public void ChangeEffectValue(float eValue)
    {
        effectValue = eValue;
        m_effect.volume = soundValue * effectValue;
    }

    public void ChangeBgValue(float bValue)
    {
        bgValue = bValue;
        m_Bg.volume = soundValue * bgValue;
        m_Bg.clip = m_Bg.clip;
    }
}