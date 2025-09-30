using UnityEngine;

namespace PlayScene
{
    /// <summary>
    /// プレイヤーのアクションA式
    /// </summary>
    public class PlayerActionA : IPlayerAction
    {
        // プレイヤーのパーツ
        private PlayerParts parts;
        // プレイヤーの状態
        private PlayerStatus status;
        public PlayerActionA(PlayerParts parts, PlayerStatus status)
        {
            this.parts = parts;
            this.status = status;
        }

        /// <summary>
        /// 着地の処理
        /// </summary>
        public void Landing()
        {
            status.Velocity = Vector3.zero;
        }

        /// <summary>
        /// 重力を適用する
        /// </summary>
        public void Gravity()
        {
            status.Velocity += Config.Physics.Gravity * Vector3.down;
        }

        /// <summary>
        /// ジャンプの処理
        /// </summary>
        public void Jump()
        {
            status.Velocity += Vector3.up * JumpForce(Config.Physics.Gravity, Config.Player.JumpHeight);

            /// <summary>
            /// ジャンプに必要な初速を求める関数
            /// </summary>
            /// <param name="g">重力加速度</param>
            /// <param name="h">高さ(m)</param>
            /// <returns>初速(m/s)</returns>
            static float JumpForce(float g, float h)
            {
                return Mathf.Sqrt(2.0f * g * h);
            }
        }

        /// <summary>
        /// 横移動処理
        /// </summary>
        /// <param name="force">移動力(m/s)</param>
        public void Move(float force)
        {
            Vector3 v = status.Velocity;
            v.x = force;
            status.Velocity = v;
        }
    }
}
