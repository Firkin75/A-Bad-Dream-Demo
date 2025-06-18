using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    public float gunDamage = 1;           // 1�ؤι��Ĥ��뤨�����`��
    public float gunRange = 2;            // ���Ŀ��ܤʾ��x
    public float fireRate = 2f;           // 1�뤢����ι��Ļ��������`�������
    public float soundRange = 2;          // ���������빠�죨������ɰ뾶��

    public AudioSource hitSound;          // �������Ф����r����
    public AudioSource knifeSound;        // �����r����
    public Camera fpsCam;                 // FPSҕ��Υ����
    public LayerMask enemyLayerMask;      // ���ʳ��å쥤��`

    private Animator gunAnim;             // �ʥ��դΥ��˥�`���`
    private Transform player;             // �ץ쥤��`��Transform
    private float nextTimeToFire = 0;     // �Τι��Ŀ��ܕr�g

    void Start()
    {
        gunAnim = GetComponent<Animator>();

        // �ץ쥤��`���֥������Ȥ򥿥���̽��
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! 'Player'�������O������Ƥ��뤫�_�J���Ƥ���������");
        }
    }

    void Update()
    {
        if (GameManager.isReading || GameManager.gameIsPaused || !GameManager.isAlive) return;

        // �󥯥�å��ǹ��ģ����`��������ФǤʤ���У�
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
    }

    // ���ĄI��
    void Fire()
    {
        gunAnim.SetTrigger("Fire"); // ���˥�`���������

        // ������ɹ����ڤΔ��˚ݤŤ�����
        Collider[] enemyColliders = Physics.OverlapSphere(player.position, soundRange, enemyLayerMask);
        foreach (var enemyCollider in enemyColliders)
        {
            EnemyController ai = enemyCollider.GetComponent<EnemyController>();
            if (ai != null)
            {
                ai.OnHeardGunshot(); // �|�����ʥ��դǤ⣩���������򥢥���״�B��
            }
        }

        // DOOM�L�δ�ֱ���`�ȥ����ࣺ�ץ쥤��`���՜ʤ�ˮƽ����Τߤǡ���ֱ������ԄӤ������ж�
        Vector3 origin = fpsCam.transform.position;
        Vector3 horizontalDirection = fpsCam.transform.forward;
        horizontalDirection.y = 0;
        horizontalDirection.Normalize();

        // SphereCast���}���ҥåȗʳ�
        RaycastHit[] hits = Physics.SphereCastAll(origin, 0.5f, horizontalDirection, gunRange, enemyLayerMask);

        RaycastHit? bestTarget = null;
        float bestDistance = float.MaxValue;

        foreach (RaycastHit h in hits)
        {
            if (h.collider.CompareTag("Enemy"))
            {
                hitSound.Play(); // �ҥå������Ȥ��Q�餹���g�H������ж���

                Vector3 toTarget = h.collider.bounds.center - origin;
                Ray rayToTarget = new Ray(origin, toTarget.normalized);
                float distanceToTarget = toTarget.magnitude;

                // �Ϻ�������å���Trigger�oҕ��
                if (Physics.Raycast(rayToTarget, out RaycastHit obstacleHit, distanceToTarget, ~0, QueryTriggerInteraction.Ignore))
                {
                    if (!obstacleHit.collider.CompareTag("Enemy"))
                    {
                        continue; // ��������ڤ��Ƥ���ʤ饹���å�
                    }
                }

                // ҕ����ͨ�äƤ��딳�Ȥ��ƒ��ã�������������ȣ�
                if (distanceToTarget < bestDistance)
                {
                    bestDistance = distanceToTarget;
                    bestTarget = h;
                }
            }
        }

        // ����`���I��
        if (bestTarget.HasValue)
        {
            EnemyController enemy = bestTarget.Value.collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(gunDamage);
                Debug.Log("Auto-aim hit enemy: " + enemy.name);
            }
        }
        else
        {
            knifeSound.Play(); // �����
            Debug.Log("No enemy hit by auto-aim.");
        }
    }
}
