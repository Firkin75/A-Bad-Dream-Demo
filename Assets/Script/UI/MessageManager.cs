using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; } // ���󥰥�ȥ󥤥󥹥���

    [Header("UI Components")]
    public Text pickupText;    // �������¤ˡ�������ȡ�ä������ȱ�ʾ
    public Text warningText;   // ������������ˡ��ɥ�����å�����Ƥ��롹�ȱ�ʾ

    [Header("Display Settings")]
    public float pickupDuration = 2f;   // ȡ�å�å��`���α�ʾ�r�g
    public float warningDuration = 2f;  // �����å��`���α�ʾ�r�g

    [Header("SFX")]
    public AudioSource pickupAudioSource; // ȡ�Õr�΄�����

    private Coroutine pickupCoroutine;
    private Coroutine warningCoroutine;

    void Awake()
    {
        // ���󥰥�ȥ���O��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        pickupText.text = "";   // ���ڻ���ȡ�å�å��`����դ�
        warningText.text = "";  // ���ڻ��������å��`����դ�
    }

    // ȡ�å�å��`�����ʾ
    public void ShowPickupMessage(string message)
    {
        if (pickupCoroutine != null)
        {
            StopCoroutine(pickupCoroutine); // �ȴ�Υ���`�����ֹͣ
        }
        pickupCoroutine = StartCoroutine(ShowTemporaryMessage(pickupText, message, pickupDuration));
    }

    // �����å��`�����ʾ
    public void ShowWarningMessage(string message)
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine); // �ȴ�Υ���`�����ֹͣ
        }
        warningCoroutine = StartCoroutine(ShowTemporaryMessage(warningText, message, warningDuration));
    }

    // ȡ�Õr�΄�����������
    public void PlayPickupSound()
    {
        pickupAudioSource.Play();
    }

    // һ���r�g��å��`�����ʾ�����Ǳ�ʾ�ˤ��빲ͨ�I��
    private IEnumerator ShowTemporaryMessage(Text textComponent, string message, float duration)
    {
        textComponent.text = message;
        yield return new WaitForSecondsRealtime(duration); // ָ���������C�����`��ֹͣ�Ф⥫����ȣ�
        textComponent.text = "";
    }
}
