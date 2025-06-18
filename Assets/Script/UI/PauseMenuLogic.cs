using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLogic : MonoBehaviour
{
    public AudioMixer audioMixer;     // ���`�ǥ����ߥ����`�ؤβ��գ������{���ã�
    public Slider volumeSlider;       // �����{�����饤���`

    private bool isPaused = false;    // �ݩ`���Ф��ɤ����Υե饰

    void Start()
    {
        // ���椵�줿������ȡ�ã��ǥե���Ȥ�0 dB��
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        audioMixer.SetFloat("MasterVol", savedVolume);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
    }

    void Update()
    {
        // �i���� or �����Фϥݩ`���o��
        if (!GameManager.isAlive || GameManager.isReading) return;

        // ESC���`�ǥݩ`���Υ���/�����Ф��椨
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ���`������������ֱ�������`��0�����i���z�ߣ�
    public void reStart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // �ᥤ���˥�`�ؑ��루���`��0���i���z�ߣ�
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // ���`���K�ˣ��ӥ�ɰ�ǤΤ��Є���
    public void exitTheGame()
    {
        Application.Quit();
    }

    // �ݩ`���I��
    public void PauseGame()
    {
        GameManager.gameIsPaused = true;
        UIManager.Instance.ShowPauseMenu(true); // �ݩ`����˥�`���ʾ

        Time.timeScale = 0f;                    // ���`���r�gֹͣ
        Cursor.lockState = CursorLockMode.None; // ���`������
        Cursor.visible = true;

        isPaused = true;
    }

    // �����O���ȱ���
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVol", volume);      // ���r��ӳ
        PlayerPrefs.SetFloat("MasterVolume", volume);  // ����
        PlayerPrefs.Save();
    }

    // �ݩ`������I��
    public void ResumeGame()
    {
        UIManager.Instance.ShowPauseMenu(false);   // ��˥�`��Ǳ�ʾ
        Time.timeScale = 1f;                       // ���`�����_

        Cursor.lockState = CursorLockMode.Locked;  // ���`����̶�
        Cursor.visible = false;

        GameManager.gameIsPaused = false;
        isPaused = false;
    }
}
