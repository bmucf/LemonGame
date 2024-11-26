using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    public bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    public float timeRemaining = 91f;
    public TextMeshProUGUI clockDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    public void ClockCountdown()
    {
        if (m_IsPlayerAtExit || m_IsPlayerCaught)
        {
            return;
        }

        if (timeRemaining > 70)
        {
            timeRemaining -= 1 * Time.deltaTime;
            clockDisplay.text = "01:" + (int)(timeRemaining - 60);
        }
        else if (timeRemaining >= 60)
        {
            timeRemaining -= 1 * Time.deltaTime;
            clockDisplay.text = "01:0" + (int)(timeRemaining -60);
        }
        else if (timeRemaining > 10)
        {
            timeRemaining -= 1 * Time.deltaTime;
            clockDisplay.text = "00:" + (int)(timeRemaining);
        }
        else
        {
            timeRemaining -= 1 * Time.deltaTime;
            clockDisplay.text = "00:0" + (int)(timeRemaining);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClockCountdown();
        
        if (timeRemaining < 1)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, exitAudio);
        }

        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

        void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
        {
            if (!m_HasAudioPlayed)
            {
                audioSource.Play();
                m_HasAudioPlayed = true;
            }

            m_Timer += Time.deltaTime;
            imageCanvasGroup.alpha = m_Timer / fadeDuration;

            if (m_Timer > fadeDuration + displayImageDuration)
            {
                if (doRestart)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }
}
