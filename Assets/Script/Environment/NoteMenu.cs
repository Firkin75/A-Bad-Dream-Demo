using Unity.VisualScripting;
using UnityEngine;

public class NoteMenu : MonoBehaviour
{
    private bool wasReading = false; // ֱǰ��״�B�����i���С����ä���

    void Update()
    {
        // �Ω`��UI����ʾ����Ƥ��뤫�ɤ���
        bool isNoteActive = UIManager.Instance.noteUI.activeInHierarchy;

        // �Ω`�Ȥ��_�����Ȥ��΄I��
        if (isNoteActive && !wasReading)
        {
            Cursor.lockState = CursorLockMode.None; // ���`�������
            Cursor.visible = true;                 // ���`�����ʾ
            Time.timeScale = 0f;                   // ���`��ֹͣ
            GameManager.isReading = true;          // �i���ե饰ON
            wasReading = true;                     // ״�Bӛ�h
        }
        // �Ω`�Ȥ��]�����Ȥ��΄I��
        else if (!isNoteActive && wasReading)
        {
            Cursor.lockState = CursorLockMode.Locked; // ���`������å�
            Cursor.visible = false;                   // ���`����Ǳ�ʾ
            Time.timeScale = 1f;                      // ���`�����_
            GameManager.isReading = false;            // �i���ե饰OFF
            wasReading = false;                       // ״�Bӛ�h
        }
    }

    // �ܥ���ʤɤ�����ӳ������Ω`��UI���]����
    public void CloseNoteUI()
    {
        UIManager.Instance.noteUI.SetActive(false);
    }
}
