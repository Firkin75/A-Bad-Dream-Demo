using UnityEngine;

public class PCInteract : MonoBehaviour, IInteractable
{
    public bool isNote;        // �Ω`�ȣ��R�귵��ʹ�ÿɣ�
    public bool isMap;         // �ޥåױ�ʾ��1�ؤΤߣ�
    public bool isSecurity;    // �������ƥ����`ȡ�ö�ĩ��1�ؤΤߣ�

    public GameObject enemySet;     // �����󼤻�ĵ�����
    public GameObject indicator;    // ��ʾUI�����������أ�

    [Header("Security Light")]
    public Light securityLight;     // �������ƥ��饤�ȣ��vɫ�ˉ仯��

    private bool hasInteracted = false; // һ���ޤ�΄I���ʹ�ã��Ω`�����⣩

    public void Interact()
    {
        // �Ω`�������һ���ޤ�
        if (!isNote && hasInteracted) return;

        // ��ͨ����Ч����
        MessageManager.Instance.PlayPickupSound();

        // -------- �Ω`�� --------���R�귵���ɣ�
        if (isNote)
        {
            UIManager.Instance.ShowNoteUI(true);
            if (enemySet != null) enemySet.SetActive(true);
        }

        // -------- �ޥå� --------��1�ؤΤߣ�
        if (isMap && !hasInteracted)
        {
            UIManager.Instance.ShowMiniMap(true);
            MessageManager.Instance.ShowWarningMessage("You got the map for this area");
        }

        // -------- �������ƥ����` --------��1�ؤΤߣ�
        if (isSecurity && !hasInteracted)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    inventory.hasKey = true;
                    UIManager.Instance.ShowKeyUI(true);
                    MessageManager.Instance.ShowPickupMessage("The door is now unlocked");
                }
                else
                {
                    Debug.LogWarning("PlayerInventory ����ݩ`�ͥ�Ȥ�Ҋ�Ĥ���ޤ���");
                }
            }
            else
            {
                Debug.LogWarning("�ץ쥤��`��Ҋ�Ĥ���ޤ���");
            }

            if (securityLight != null)
                securityLight.color = Color.green;
        }

        // ��ͨ�I��ָʾ��������Ǳ�ʾ�������F
        if (indicator != null)
            indicator.SetActive(false);

        if (enemySet != null)
            enemySet.SetActive(true);

        // �Ω`������ʤ�һ���ޤ�
        if (!isNote)
            hasInteracted = true;
    }

    public bool IsInteractable()
    {
        // �Ω`���������ʹ�ò���
        return isNote || !hasInteracted;
    }
}
