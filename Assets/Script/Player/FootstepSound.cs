using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource; // ������AudioSource
    public float minSpeed;                  // �������Q�뤿�����С�Ƅ��ٶ�
    public float footstepDelay;             // �������g�����룩

    private CharacterController characterController;
    private Vector3 previousPosition;       // ǰ�ե�`���λ��
    private float currentSpeed;             // �F�ڤ��Ƅ��ٶ�
    private float timeSinceLastFootstep;    // ǰ�ؤ���������νU�^�r�g

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (footstepAudioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!"); // AudioSource��δ�O���Έ��ϥ���`
        }

        previousPosition = transform.position;         // ����λ�ä�ӛ�h
        timeSinceLastFootstep = footstepDelay;         // �����ީ`���ڻ�
    }

    void Update()
    {
        // �F�ڤ��Ƅ��ٶȤ�Ӌ��
        Vector3 positionDelta = transform.position - previousPosition;
        currentSpeed = positionDelta.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        // �ӵ�״�B�Ǥ����ٶȤ�һ�����ϤΈ��ϡ�����������
        if (characterController.isGrounded && currentSpeed > minSpeed)
        {
            timeSinceLastFootstep += Time.deltaTime;

            if (timeSinceLastFootstep >= footstepDelay)
            {
                PlayFootstep();                  // ����������
                timeSinceLastFootstep = 0f;      // �����ީ`�ꥻ�å�
            }
        }
        else
        {
            // �ƄӤ��Ƥ��ʤ����ޤ��Ͽ��ФΈ��ϡ�������ֹͣ
            if (footstepAudioSource.isPlaying)
            {
                return;
            }
        }
    }

    // ���������������Ǥ������ФǤʤ���У�
    void PlayFootstep()
    {
        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }
}
