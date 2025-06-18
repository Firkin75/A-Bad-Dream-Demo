using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amountOfHealth; // �؏ͤ������������

    void OnTriggerEnter(Collider other)
    {
        // �ץ쥤��`�ȽӴ��������_�J
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            // ���Ǥ�������������ʤ�Τ⤷�ʤ�
            if (PlayerHealth.currentHealth >= PlayerHealth.maxHealth)
            {
                return;
            }

            // ������؏ͤ�����å��`�����ʾ
            health.AddHealth(amountOfHealth);
            MessageManager.Instance.ShowPickupMessage("Picked up a medic kit");

            // ������������
            MessageManager.Instance.PlayPickupSound();

            // ���λ؏ͥ����ƥ����ȥ
            Destroy(gameObject);
        }
    }
}
