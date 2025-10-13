using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace PlayScene
{
    /// <summary>
    /// プレイヤーの動作を実装するためのインタフェース
    /// </summary>
    public interface IPlayerAction
    {
        /// <summary>
        /// 着地を適用
        /// </summary>
        void Landing();
        /// <summary>
        /// 重力を適用する
        /// </summary>
        void Gravity();
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

    public interface IPlayerActionStatus
    {
        enum PlayerActionStatusType
        {
            Run,
            Jump
        }

        /// <summary>
        /// 左右移動更新処理
        /// </summary>
        void UpdateMove(PlayerStatus status, IPlayerAction action);
        /// <summary>
        /// ジャンプ処理
        /// </summary>
        void UpdateJump(PlayerStatus status, IPlayerAction action);
    }

    /// <summary>
    /// 動作として使うプレイヤーのパーツ
    /// </summary>
    [System.Serializable]
    public class PlayerParts
    {
        [HideInInspector]
        // プレイヤーの移動に使うキャラクターコントローラコンポーネント
        public CharacterController CharacterController { get; set; }
        // プレイヤーの座標系
        [HideInInspector]
        public Transform Transform { get; set; }
        // // 手の位置の当たり判定-右
        // public BoxCollider handTriggerRight;
        // // 手の位置の当たり判定-左
        // public BoxCollider handTriggerLeft;
        // // 頭の位置の当たり判定
        // public BoxCollider headTrigger;
        public List<(int self, int target)> OnTriggersId { get; set; }
        // プレイヤーのパーツ
        [SerializeField]
        public PlayerPartsController[] partsControllers;
    }

    /// <summary>
    /// 動作として参照する状態パラメータ
    /// </summary>
    public class PlayerStatus
    {
        // ジャンプしているか
        public bool IsJumping { get; set; }
        // 地面にいるか true / false
        public bool IsGrounded { get; set; }
        // 壁をタッチしているか true / false
        public bool IsTouchWallForward { get; set; }
        // 天井をタッチしているか
        public bool IsTouchWallUp { get; set; }
        // 横方向移動入力
        public float InputMoveX { get; set; }
        // ジャンプ入力
        public bool InputJump { get; set; }
        // 速度(m/s)
        public Vector3 Velocity { get; set; }
        // 当たっているコライダーのId
        public List<(int self, int target)> HitColliderId { get; set; } = new();

        public override string ToString()
        {
            return string.Join("", new string[]
            {
                $"IsJumping:{IsJumping}, IsGrounded:{IsGrounded}, ",
                $"IsTouchWallForward:{IsTouchWallForward}, IsTouchWallUp:{IsTouchWallUp}, ",
                $"InputMoveX:{InputMoveX}, InputJump:{InputJump}, ",
                $"Velocity:{Velocity}, HitColliderId.Count:{HitColliderId.Count}",
            });
        }
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
        [SerializeField]
        private PlayerParts parts;
        // プレイヤーの動作
        private IPlayerAction action;
        // プレイヤーの動作ステータス
        private IPlayerActionStatus actionStatus;


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

            Debug.Log(status);
        }

        /// <summary>
        /// 状態の更新
        /// </summary>
        void UpdateState()
        {
            status.IsGrounded = parts.CharacterController.isGrounded;
            status.InputJump = Input.GetButton("Jump");
            status.InputMoveX = Input.GetAxis("Horizontal");

            status.IsTouchWallUp = false;
            status.IsTouchWallForward = false;

            foreach (PlayerPartsController controller in parts.partsControllers)
            {
                if (controller.IsTouching())
                {
                    switch (controller.GetPartsType())
                    {
                        case PlayerPartsType.Head:
                            status.IsTouchWallUp = true;
                            break;
                        case PlayerPartsType.HandRight:
                            status.IsTouchWallForward = true;
                            break;
                        case PlayerPartsType.HandLeft:
                            status.IsTouchWallForward = true;
                            break;
                        default:
                            Debug.LogError($"未処理のパーツタイプ");
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// アクションの更新
        /// </summary>
        void UpdateAction()
        {
            action.Move(status.InputMoveX);

            if (status.IsGrounded)
            {
                action.Landing();  // 地面に触れているなら着地処理

                if (status.IsJumping)
                {
                    action.Jump();  // 地面に触れていて、ジャンプボタンが押されたらジャンプ
                    status.IsJumping = false;
                }
                else
                {
                    if (status.InputJump)
                    {
                        status.IsJumping = true;
                    }
                }
            }
            else
            {
                action.Gravity();  // 重力適用
            }

            parts.CharacterController.Move(status.Velocity * Time.deltaTime);
        }
    }
}
