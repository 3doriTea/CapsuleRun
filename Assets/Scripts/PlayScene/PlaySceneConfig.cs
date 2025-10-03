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
        /// 物理関連の設定
        /// </summary>
        public class Physics
        {
            // 重力加速度
            public const float Gravity = 9.0f;
        }

        /// <summary>
        /// プレイヤー関連の設定
        /// </summary>
        public class Player
        {
            // 移動スピード(m/s)
            public const float MoveSpeedPerSec = 5.0f;
            // ジャンプする高さ(m)
            public const float JumpHeight = 3.0f;
        }

        /// <summary>
        /// 入力関連の設定
        /// </summary>
        public class Input
        {
            // 左右移動入力として受け取るしきい値(ちっちゃい誤入力を無視する)
            private const float MoveDeadzone = 0.1f;
            // 上記の2乗した値
            public const float MoveDeadzoneSquared = MoveDeadzone * MoveDeadzone;
        }
    }
}