using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���`��ȫ���״�B�������ե饰
    public static bool gameIsPaused; // һ�rֹͣ�Ф�
    public static bool isReading;    // �Ω`�Ȥ�ޥåפʤ���E�Ф�
    public static bool isAlive;      // �ץ쥤��`�����椷�Ƥ��뤫

    // �����һ�Ȥ������Ф��
    void Start()
    {
        Application.targetFrameRate = 60; // FPS���ޤ��O��

        gameIsPaused = false; // ����״�B���ǥݩ`��
        isAlive = true;       // �ץ쥤��`�������Ƥ���
        isReading = false;    // ��E�ФǤϤʤ�
        Time.timeScale = 1.0f; // ���`��Εr�g��ͨ��ͨ����M��

    }
}
