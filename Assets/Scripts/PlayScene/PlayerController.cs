using UnityEngine;

namespace PlayScene
{
    /// <summary>
    /// プレイヤーの動作を実装するためのインタフェース
    /// </summary>
    public interface IPlayerAction
    {
        /// <summary>
        /// ジャンプする
        /// </summary>
        void Jump();
        /// <summary>
        /// 左右に移動する
        /// </summary>
        /// <param name="force">移動方向と力</param>
        void Move(float force);
    }

    /// <summary>
    /// 動作として使うプレイヤーのパーツ
    /// </summary>
    public class PlayerParts
    {
        // プレイヤーの移動に使うキャラクターコントローラコンポーネント
        public CharacterController CharacterController { get; set; }
        // プレイヤーの座標系
        public Transform Transform { get; set; }
        // 手の位置の当たり判定-右
        public BoxCollider HandTriggerRight { get; set; }
        // 手の位置の当たり判定-左
        public BoxCollider HandTriggerLeft { get; set; }
        // 頭の位置の当たり判定
        public BoxCollider HeadTrigger { get; set; }
    }

    /// <summary>
    /// 動作として参照する状態パラメータ
    /// </summary>
    public class PlayerStatus
    {
        // 地面にいるか true / false
        public bool IsGrounded { get; set; }
        // 壁をタッチしているか true / false
        public bool IsTouchWallForward { get; set; }
        // 横方向移動入力
        public float InputMoveX { get; set; }
        // ジャンプ入力
        public bool InputJump { get; set; }
        // 速度(m/s)
        public Vector3 Velocity { get; set; }
    }

    /// <summary>
    /// プレイヤーとして操作したいゲームオブジェクトにアタッチするスクリプト
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // プレイヤーの状態
        private readonly PlayerStatus status = new();
        // プレイヤーのパーツ
        private readonly PlayerParts parts = new();
        // プレイヤーの動作
        private IPlayerAction action;

        void Start()
        {
            parts.CharacterController = GetComponent<CharacterController>();
            parts.Transform = transform;

            action = new PlayerActionA(parts, status);
        }

        void Update()
        {
            // 状態の更新
            UpdateState();

            // アクションの更新
            UpdateAction();
        }

        /// <summary>
        /// 状態の更新
        /// </summary>
        void UpdateState()
        {
            status.IsGrounded = parts.CharacterController.isGrounded;
            status.InputJump = Input.GetButton("Jump");
            status.InputMoveX = Input.GetAxis("Horizontal");
        }

        /// <summary>
        /// アクションの更新
        /// </summary>
        void UpdateAction()
        {
            if (status.InputMoveX * status.InputMoveX > Config.Input.MoveDeadzoneSquared)
            {
                action.Move(status.InputMoveX);  // 入力がデッドゾーンより大きいなら移動アクション
            }

            if (status.InputJump && status.IsGrounded)
            {
                action.Jump();  // 地面に触れていて、ジャンプボタンが押されたらジャンプ
            }

            parts.CharacterController.Move(status.Velocity);
        }

        void OnCollisionEnter(Collision collision)
        {
            foreach (ContactPoint point in collision.contacts)
            {
                // point.thisCollider.GetId
                // TODO: あたったコライダーとあたった元のコライダーを取得する
            }
        }

        void OnCollisionExit(Collision collision)
        {

        }
    }
}
