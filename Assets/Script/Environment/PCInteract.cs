using UnityEngine;

/// <summary>
/// PCや端末とのインタラクション処理を行うスクリプト
/// ノート・マップ・セキュリティ端末に応じた挙動を管理
/// </summary>
public class PCInteract : MonoBehaviour, IInteractable
{
    public bool isNote;        // ノートのUIを開くか（繰り返し使用可能）
    public bool isMap;         // マップを表示する端末か（1回のみ）
    public bool isSecurity;    // 鍵を取得するセキュリティ端末か（1回のみ）

    public GameObject enemySet;     // インタラクト後に出現させる敵グループ
    public GameObject indicator;    // 交互前に表示されるUIアイコン（Eキーなど）

    [Header("Security Light")]
    public Light securityLight;     // セキュリティ端末のライト（成功時に緑点灯）

    private bool hasInteracted = false; // ノート以外の一度限り処理のフラグ

    /// <summary>
    /// プレイヤーがインタラクトした際の処理本体
    /// </summary>
    public void Interact()
    {
        // ノート以外は一度だけ使用可能
        if (!isNote && hasInteracted) return;

        // すべて共通の処理：取得音再生
        MessageManager.Instance.PlayPickupSound();

        // -------- ノート処理（繰り返し可）--------
        if (isNote)
        {
            UIManager.Instance.ShowNoteUI(true);         // ノートUI表示
            if (enemySet != null) enemySet.SetActive(true); // 敵出現
        }

        // -------- マップ処理（1回のみ）--------
        if (isMap && !hasInteracted)
        {
            UIManager.Instance.ShowMiniMap(true);  // ミニマップUI表示
            MessageManager.Instance.ShowWarningMessage("You got the map for this area");
        }

        // -------- セキュリティキー処理（1回のみ）--------
        if (isSecurity && !hasInteracted)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    inventory.hasKey = true;                        // 鍵を所持状態に
                    UIManager.Instance.ShowKeyUI(true);            // 鍵UI表示
                    MessageManager.Instance.ShowPickupMessage("The door is now unlocked");
                }
                else
                {
                    Debug.LogWarning("PlayerInventory コンポーネントが見つかりません！");
                }
            }
            else
            {
                Debug.LogWarning("プレイヤーが見つかりません！");
            }

            // セキュリティライトを緑色に変更
            if (securityLight != null)
                securityLight.color = Color.green;
        }

        // -------- 共通後処理 --------
        if (indicator != null)
            indicator.SetActive(false);  // UIアイコン非表示

        if (enemySet != null)
            enemySet.SetActive(true);    // 敵出現（ノートでも有効）

        // ノート以外は使用済みに設定
        if (!isNote)
            hasInteracted = true;
    }

    /// <summary>
    /// インタラクト可能かどうかの判定
    /// </summary>
    public bool IsInteractable()
    {
        // ノートは繰り返し可、それ以外は未使用のみ可
        return isNote || !hasInteracted;
    }
}
