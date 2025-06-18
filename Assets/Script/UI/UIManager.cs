using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // ���󥰥�ȥ󥤥󥹥���

    [Header("UI Elements")]
    public GameObject weaponUI;       // ����UI
    public GameObject keyDisplayUI;   // �I��ȡ��UI
    public GameObject deadScrn;       // ��������
    public GameObject pm;             // �ݩ`����˥�`
    public GameObject noteUI;         // �Ω`�ȱ�ʾUI
    public GameObject miniMap;        // �ߥ˥ޥå�UI

    [Header("Tutorial UI")]
    public GameObject moveTutorial;       // �Ƅӥ���`�ȥꥢ��
    public GameObject interact;           // ���󥿥饯�ȥҥ��UI
    public GameObject combatTutorial;     // ���L����`�ȥꥢ��

    void Awake()
    {
        // ���󥰥�ȥ�γ��ڻ��I��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // ��UI�����״�B�ǷǱ�ʾ���O��
        pm.SetActive(false);
        keyDisplayUI.SetActive(false);
        noteUI.SetActive(false);
        miniMap.SetActive(false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���Ʃ`��1�Ǥ��Ƅӥ���`�ȥꥢ����ʾ��������UI�ϷǱ�ʾ
        if (currentSceneIndex == 1)
        {
            ShowMovementTutorial(true);
            weaponUI.SetActive(false);
        }
        else
        {
            weaponUI.SetActive(true);
        }
    }

    // --------------------------------------------------
    // ��������ץȤ�����ӳ���UI�����᥽�å�Ⱥ
    // --------------------------------------------------

    public void ShowWeaponUI(bool show)
    {
        weaponUI.SetActive(show); // ����UI��ʾ����
    }

    public void ShowPauseMenu(bool show)
    {
        pm.SetActive(show); // �ݩ`����˥�`��ʾ����
    }

    public void ShowMiniMap(bool show)
    {
        miniMap.SetActive(show); // �ߥ˥ޥåױ�ʾ����
    }

    public void ShowNoteUI(bool show)
    {
        noteUI.SetActive(show); // �Ω`��UI��ʾ����
    }

    public void ShowKeyUI(bool show)
    {
        keyDisplayUI.SetActive(show); // �IUI��ʾ����
    }

    public void ShowDeathScreen(bool show)
    {
        deadScrn.SetActive(show); // ���������ʾ����
    }

    public void ShowMovementTutorial(bool show)
    {
        Time.timeScale = 0f;                     // ���`��ֹͣ
        GameManager.isReading = true;
        Cursor.lockState = CursorLockMode.None;  // ���`������
        Cursor.visible = true;
        moveTutorial.SetActive(true);            // �Ƅӥ���`�ȥꥢ���ʾ
    }

    public void CloseMovementTutorial()
    {
        Debug.Log("clicked");
        moveTutorial.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // ���`����̶�
        Cursor.visible = false;
        GameManager.isReading = false;
        Time.timeScale = 1f;                      // ���`�����_
    }

    public void ShowCombatTutorial(bool show)
    {
        Time.timeScale = 0f;
        GameManager.isReading = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        combatTutorial.SetActive(true);           // ���L����`�ȥꥢ���ʾ
    }

    public void CloseCombatTutorial()
    {
        Debug.Log("clicked");
        combatTutorial.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.isReading = false;
        Time.timeScale = 1f;
    }

    public void ShowInteractUI(bool show)
    {
        interact.SetActive(show); // ���󥿥饯��UI��ʾ����
    }
}
