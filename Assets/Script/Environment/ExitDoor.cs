using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour, IInteractable
{
    private AudioSource lockedSound;               // �I���ʤ����ϤΥ������
    private PlayerInventory playerInventory;       // �ץ쥤��`������Ʒ���

    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>(); // �ץ쥤��`�Υ���٥�ȥ�ȡ��
        lockedSound = GetComponent<AudioSource>();                  // AudioSourceȡ��
    }

    public void Interact()
    {
        if (playerInventory.hasKey)
        {
            // �I�����֤��Ƥ���дΤΥ��`��أ����`��3��
            SceneManager.LoadScene(3);
        }
        else
        {
            // �I���ʤ����Ϥ����Ⱦ����å��`�����ʾ
            lockedSound.Play();
            MessageManager.Instance.ShowWarningMessage("A key is required");
        }
    }
}
