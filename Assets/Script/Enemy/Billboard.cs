using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform; // �ᥤ�󥫥���Transform�ؤβ���

    void Start()
    {
        // �ᥤ�󥫥���ȡ��
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Billboard: �ᥤ�󥫥�餬Ҋ�Ĥ���ޤ���");
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return; // ����餬δ�O���Έ��ϤτI����ֹ

        // ����鷽��ؤΥ٥��ȥ��Ӌ��
        Vector3 direction = cameraTransform.position - transform.position;

        // ���ץ�������¤λ�ܞ��̶����᷽������򤯤褦�ˤ��룩
        direction.y = 0;

        // �����η�����򤯤褦�˻�ܞ
        transform.rotation = Quaternion.LookRotation(direction);

        // �����ʾ�����ץ饤�Ȥ����򤭤Έ��ϣ�
        transform.Rotate(0, 180f, 0);
    }
}
