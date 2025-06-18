using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    public GameObject enemySet;            // ����_�����Ȥ����F���딳���å�
    public AudioSource doorSound;          // �_�]�r�Υ������
    public AudioSource lockedSound;        // �I����Ҫ�ʈ��ϤΥ�å���
    public bool requiresKey = false;       // �I����Ҫ���ɤ���

    [SerializeField] private float moveSpeed = 2f;             // ����_�]�ٶ�
    [SerializeField] private float autoCloseDelay = 5f;        // �ԄӤ��]�ޤ�ޤǤΕr�g
    [SerializeField] private Vector3 openOffset = new Vector3(3, 0, 0); // ����_�����x

    private Transform doorTransform;        // �g�H�˄Ӥ����饪�֥�������
    private Vector3 closedPosition;         // �]�ޤäƤ���λ��
    private Vector3 openPosition;           // �_���Ƥ���λ��
    private PlayerInventory playerInventory;
    private bool isOpen = false;            // �餬�_���Ƥ��뤫�ɤ���
    private bool isEnemyInRange = false;    // ���������ˤ��뤫�ɤ���
    private float lastOpenTime = -999f;     // ������_�����r�g���Ԅ��]�i�ã�

    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();

        Transform parent = transform.parent;
        if (parent != null)
        {
            doorTransform = parent.Find("Door"); // "Door"�Ȥ�����ǰ���Ӥ�̽��
        }

        if (doorTransform == null)
        {
            Debug.LogWarning("Door object not found under parent.");
            return;
        }

        closedPosition = doorTransform.position;
        openPosition = closedPosition + doorTransform.right * openOffset.magnitude;
    }

    void Update()
    {
        if (doorTransform == null) return;

        // һ���r�g����ԄӤ��]�ޤ�I���������ʤ��Ȥ��Τߣ�
        if (isOpen && !isEnemyInRange && Time.time - lastOpenTime >= autoCloseDelay)
        {
            CloseDoor();
        }

        // �F�ڤ�Ŀ��λ�ä��Ƅӣ����餫���_�]��
        Vector3 target = isOpen ? openPosition : closedPosition;
        doorTransform.position = Vector3.MoveTowards(doorTransform.position, target, moveSpeed * Time.deltaTime);
    }

    // �ץ쥤��`��E���`�ǥ��󥿥饯�Ȥ����Ȥ��΄I��
    public void Interact()
    {
        if (requiresKey)
        {
            if (playerInventory.hasKey)
            {
                OpenDoor();
                if (enemySet != null) enemySet.SetActive(true);
            }
            else
            {
                if (lockedSound != null) lockedSound.Play();
                MessageManager.Instance.ShowWarningMessage("A key is required");
                Debug.Log("Key Required!");
            }
        }
        else
        {
            OpenDoor();
            if (enemySet != null) enemySet.SetActive(true);
        }
    }

    // ����_���I��
    private void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        lastOpenTime = Time.time;

        if (doorSound != null) doorSound.Play();
    }

    // ����]����I��
    private void CloseDoor()
    {
        if (!isOpen) return;

        isOpen = false;

        if (doorSound != null) doorSound.Play();
    }

    // ������Υ��󥵩`����ä��r
    public void OnEnemyEnter()
    {
        isEnemyInRange = true;
        OpenDoor();
    }

    // ������Υ��󥵩`�����x�줿�r
    public void OnEnemyExit()
    {
        isEnemyInRange = false;
    }
}
