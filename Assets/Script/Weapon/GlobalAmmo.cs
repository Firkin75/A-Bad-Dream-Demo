using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全体武器の弾薬数を管理するグローバルクラス（静的管理）
/// </summary>
public class GlobalAmmo : MonoBehaviour
{
    // ------ 静的変数：全体で共有される弾薬数 ------
    public static int handGunAmmo;       // ハンドガンの弾薬数
    public static int heavyAmmo;         // ライフル／マシンガン用の重弾薬数
    public static string fist = "inf";   // 素手武器の弾薬（無限を文字列で表現）

    // ------ ピックアップ音 ------
    public AudioSource pickupSound;      // 弾薬取得時に再生する音

    void Start()
    {
        // 初期弾薬数を設定（ゲーム開始時）
        handGunAmmo = 15;
        heavyAmmo = 0;
    }

    /// <summary>
    /// ハンドガンの弾薬を追加し、効果音を再生
    /// </summary>
    public static void PickUpPistolAmmo(int amount)
    {
        handGunAmmo += amount;
        PlayPickupSound();
    }

    /// <summary>
    /// ライフルの弾薬を追加し、効果音を再生
    /// </summary>
    public static void PickUpRifleAmmo(int amount)
    {
        heavyAmmo += amount;
        PlayPickupSound();
    }

    /// <summary>
    /// マシンガンの弾薬を追加し、効果音を再生
    /// </summary>
    public static void PickUpMGAmmo(int amount)
    {
        heavyAmmo += amount;
        PlayPickupSound();
    }

    /// <summary>
    /// ピックアップ時の効果音を再生（シーン内のインスタンス経由）
    /// </summary>
    private static void PlayPickupSound()
    {
        // シーン内のGlobalAmmoインスタンスを取得
        GlobalAmmo instance = FindFirstObjectByType<GlobalAmmo>();
        if (instance != null && instance.pickupSound != null)
        {
            instance.pickupSound.Play();
        }
    }
}
