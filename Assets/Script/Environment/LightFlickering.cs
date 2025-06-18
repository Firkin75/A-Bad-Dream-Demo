using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public float minTime;  // �Τ��Ф��椨�ޤǤ���С�r�g
    public float maxTime;  // �Τ��Ф��椨�ޤǤ����r�g

    private float timer;   // �����ީ`
    private Light lightOBJ; // ����Υ饤�ȥ���ݩ`�ͥ��

    void Start()
    {
        // Light����ݩ`�ͥ��ȡ��
        lightOBJ = GetComponent<Light>();
        // ����Υ����ॿ���ީ`���O��
        timer = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        LightFlicker(); // ���ե�`��������å�
    }

    void LightFlicker()
    {
        // �����ީ`��p�餹
        if (timer > 0)
            timer -= Time.deltaTime;

        // �r�g�Ф�ˤʤä���饤�Ȥ�ON/OFF���Ф��椨���ΤΥ����ީ`���O��
        if (timer <= 0)
        {
            lightOBJ.enabled = !lightOBJ.enabled;
            timer = Random.Range(minTime, maxTime);
        }
    }
}
