using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform; // メインカメラのTransformへの参照

    void Start()
    {
        // メインカメラを取得
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Billboard: メインカメラが見つかりません！");
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return; // カメラが未設定の場合は処理中止

        // カメラ方向へのベクトルを計算
        Vector3 direction = cameraTransform.position - transform.position;

        // オプション：上下の回転を固定（横方向だけ向くようにする）
        direction.y = 0;

        // カメラの方向を向くように回転
        transform.rotation = Quaternion.LookRotation(direction);

        // 背面表示（スプライトが逆向きの場合）
        transform.Rotate(0, 180f, 0);
    }
}
