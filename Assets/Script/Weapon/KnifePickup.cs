using UnityEngine;

public class KnifePickup : MonoBehaviour
{
    public string weaponName = "Knife";     // ��������ǰ����������˵��h�����ã�
    public int slotIndex = 0;               // ��������åȷ���
    public GameObject weaponUI;             // ����UI�����������ʾ��
    public GameObject enemySet;             // �ʥ���ȡ����˳��F�����딳���å�

    void OnTriggerEnter(Collider other)
    {
        // �ץ쥤��`���Ӵ��������_�J
        if (other.CompareTag("Player"))
        {
            weaponUI.SetActive(true); // ����UI���ʾ

            // WeaponManager�Υ��󥹥��󥹤�̽��
            WeaponManager weaponManager = FindFirstObjectByType<WeaponManager>();
            if (weaponManager != null)
            {
                // ��������h
                weaponManager.PickupWeapon(weaponName, slotIndex);

                // ������F������
                if (enemySet != null)
                {
                    enemySet.SetActive(true);
                }

                // ���Υ��֥������Ȥ�����
                Destroy(gameObject);

                // ��å��`����ʾ�Ȅ�����
                MessageManager.Instance.ShowPickupMessage("Picked up a knife");
                MessageManager.Instance.PlayPickupSound();

                // UI
                UIManager.Instance.ShowCombatTutorial(true);


            }
        }
    }
}
