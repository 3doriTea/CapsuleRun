using UnityEngine;

namespace PlayScene
{
    /// <summary>
    /// プレイシーンの設定
    /// </summary>
    public class Config
    {
        /// <summary>
        /// カメラの設定
        /// </summary>
        public class Camera
        {
            // カメラがプレイヤを見るときの滑らか具合 (1秒間あたりの)
            public const float LookingRateSec = 1.0f;
        }

        /// <summary>
        /// プレイヤー関連の設定
        /// </summary>
        public class Player
        {
            // 移動スピード(m/s)
            public const float MoveSpeedPerSec = 1.0f;
            // ジャンプする高さ(m)
            public const float JumpHeight = 2.0f;
        }
    }
}