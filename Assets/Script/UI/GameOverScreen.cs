using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    void Start()
    {
        // ���`������ʾ����å����
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ���`������_�����`��1�����i���z�ߣ�
    public void reStart()
    {
        SceneManager.LoadSceneAsync(1); // ���`��ץ쥤���`��Υ���ǥå�����1
    }

    // �ᥤ���˥�`�ˑ��루���`��0��
    public void backToMenu()
    {
        SceneManager.LoadScene(0); // �����ȥ뻭��
    }

    // ���`��K�ˣ��ӥ�ɰ���Є���
    public void Quit()
    {
        Application.Quit(); // ���ǥ����Ǥτ������ޤ���
    }
}
