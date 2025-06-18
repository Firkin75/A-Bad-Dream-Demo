using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public bool isPlayerAlive = true;              // �ץ쥤��`�������Ƥ��뤫�ɤ���
    public static int maxHealth = 100;             // ���HP
    public static int currentHealth;               // �F�ڤ�HP������`�Х빲�У�

    public Image redScreenOverlay;                 // ����`���r�γ�ե�å���
    public float fadeDuration = 1.5f;              // �໭��Υե��`�ɕr�g

    public Text hpText;                            // HP��ʾUI
    public GameObject ui;                          // �����r�˷Ǳ�ʾ�ˤ���UI
    public AudioSource hitAudio;                   // �����������
    public AudioSource dieSound;                   // �����������

    [SerializeField]
    private float hitSoundCooldown = 2f;           // �������Υ��`�������
    private float lastHitTime = -1f;               // ����˱��������Q�餷���r�g

    void Start()
    {
        currentHealth = maxHealth;                 // HP���ڻ�
    }

    void Update()
    {
        // HP�ƥ����ȸ��£����ե�`�ࣩ
        hpText.text = currentHealth.ToString();
    }

    // ����`���I��
    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        // �������Υ��`��������ж�
        if (Time.time - lastHitTime >= hitSoundCooldown)
        {
            hitAudio.Play();
            lastHitTime = Time.time;
        }

        // HP��p�餹
        if (remainingDamage > 0)
        {
            currentHealth -= remainingDamage;

            // �����ж�
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // �؏̈́I��
    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    // �����I��
    private void Die()
    {
        Debug.Log("Player has died.");
        dieSound.Play();

        GameManager.isAlive = false;   // ����`�Х�״�B�����
        ui.SetActive(false);           // ���`��UI��Ǳ�ʾ
        Time.timeScale = 0f;           // ���`��ֹͣ

        StartCoroutine(FadeToRedWhilePaused());
    }

    // �����r����ե��`�ɡ�һ���r�g���GameOver�����
    private IEnumerator FadeToRedWhilePaused()
    {
        float timer = 0f;
        float fadeDuration = 2f;
        Color startColor = new Color(1, 0, 0, 0);
        Color endColor = new Color(1, 0, 0, 1);

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            redScreenOverlay.color = Color.Lerp(startColor, endColor, t);
            timer += Time.unscaledDeltaTime; // �ݩ`���ФǤ��M���r�g
            yield return null;
        }

        redScreenOverlay.color = endColor;

        // �໭����٤��S��
        float waitTime = 1f;
        float waitTimer = 0f;
        while (waitTimer < waitTime)
        {
            waitTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f; // ���`������_
        SceneManager.LoadScene(2); // GameOver���`����w��
    }
}
