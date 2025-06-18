using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // ���N��˥�`�����UI����
    public GameObject settingMenu;  // �O������
    public GameObject mainMenu;     // �ᥤ���˥�`
    public GameObject extra;        // �������ȥ黭��
    public AudioMixer audioMixer;   // ���`�ǥ����ߥ����`
    public Slider volumeSlider;     // �������饤���`

    // �����һ�Ȥ������Ф��
    void Start()
    {
        // ���椵�줿�����O�����i���z��
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat("MasterVol", savedVolume);

        // �ե�`���`���O��
        Application.targetFrameRate = 60;

        // ��˥�`��ʾ״�B���ڻ�
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
        extra.SetActive(false);

        // ���`�����ʾ����å����
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // �������`�ȡ��ܥ���Ѻ���줿�Ȥ�
    public void gameStart()
    {
        SceneManager.LoadScene(1); // ���`�ॷ�`�󣨥���ǥå���1�����i���z��
        mainMenu.SetActive(false);
        settingMenu.SetActive(false);
        extra.SetActive(false);
    }

    // �������ȥ黭����Ƅ�
    public void toExtra()
    {
        extra.SetActive(true);
        mainMenu.SetActive(false);
        settingMenu.SetActive(false);
    }

    // �O��������ʾ
    public void ToSettingMenu()
    {
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // ���`���K�ˣ��ӥ�ɰ���Є���
    public void exitTheGame()
    {
        Application.Quit();
    }

    // �ᥤ���˥�`�ؑ���
    public void backToMainMenu()
    {
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
        extra.SetActive(false);
    }

    // �������O����������
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVol", volume);     // ������ꥢ�륿������O��
        PlayerPrefs.SetFloat("MasterVolume", volume); // �����򱣴�
        PlayerPrefs.Save();
    }
}
