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
            public const float LookingRateSec = 0.7f;
            // カメラがプレイヤを追従するときの滑らか具合 (1秒間あたりの)
            public const float MoveRateSec = 0.1f;
            // プレイヤとカメラの距離
            public const float ToPlayerDistance = 20.0f;
            // プレイヤとカメラの高さの差
            public const float ToPlayerHeightOffset = 4.0f;
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
            public const float MoveSpeedPerSec = 10.0f;
            // ジャンプする高さ(m)
            public const float JumpHeight = 7.0f;
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