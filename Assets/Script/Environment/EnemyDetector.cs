using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private DoorScript doorScript; // �ɥ�������������ץȲ���

    void Start()
    {
        // �H���֥������Ȥ��� DoorScript ��ȡ��
        doorScript = GetComponentInParent<DoorScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����������ä���ɥ���֪ͨ
        if (other.CompareTag("Enemy"))
        {
            if (doorScript != null)
            {
                doorScript.OnEnemyEnter(); // �ɥ��ˡ������ӽ���֪ͨ
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �������줫�������ɥ���֪ͨ
        if (other.CompareTag("Enemy"))
        {
            if (doorScript != null)
            {
                doorScript.OnEnemyExit(); // �ɥ��ˡ������x�줿��֪ͨ
            }
        }
    }
}
