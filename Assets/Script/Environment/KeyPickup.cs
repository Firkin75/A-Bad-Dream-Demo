using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject uiKeyDisplay;  // �I��ȡ�ä����Ȥ��˱�ʾ����UI
    public GameObject enemySet;      // �Iȡ����˳��F�����딳

    void OnTriggerEnter(Collider other)
    {
        // �ץ쥤��`�����줿���ϤΤ߷���
        if (other.CompareTag("Player"))
        {
            // �����åȤ���F������
            if (enemySet != null)
            {
                enemySet.SetActive(true);
            }

            // �ץ쥤��`���I��֤�����
            other.GetComponent<PlayerInventory>().hasKey = true;

            // UI��ʾ������������I����������ʾ��
            uiKeyDisplay.SetActive(true);

            // ���Υ��֥������ȣ��I��������
            Destroy(gameObject);
        }
    }
}
