using UnityEngine;

public class FloatingItems : MonoBehaviour
{
    public float amplitude;  // �����ƄӤθߤ��������
    public float frequency;  // �����ƄӤ��٤����ܲ�����

    private Vector3 startPos; // ����λ�ä�ӛ�h

    void Start()
    {
        startPos = transform.position; // ����λ�ä�ӛ�h
    }

    void Update()
    {
        // �r�g�ˏꤸ��Y���ˤ����¤�����
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Ԫ��λ�ä�Y����Υ��ե��åȤ�Ӥ��Ƹ���
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
}
