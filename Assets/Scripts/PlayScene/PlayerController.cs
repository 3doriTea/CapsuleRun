using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

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
        /// <summary>
        /// 登る
        /// </summary>
        /// <param name="force">移動方向と力</param>
        void Climb(float force);
    }

    public interface IPlayerActionStatus
    {
        public enum Type
        {
            Run,
            Climb,
            Swing,
            Goal,
            Max,
        }

        /// <summary>
        /// 左右移動更新処理
        /// </summary>
        void UpdateMove(PlayerStatus status, IPlayerAction action, Action<Type> changeStatus);
        /// <summary>
        /// ジャンプ処理
        /// </summary>
        void UpdateJump(PlayerStatus status, IPlayerAction action, Action<Type> changeStatus);
    }

    /// <summary>
    /// 動作として使うプレイヤーのパーツ
    /// </summary>
    [Serializable]
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
        // 登っているか
        public bool IsClimbing { get; set; }
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
        const float MovingVeloDeadZone = 1.0f;
        const float RightAngle = 0.0f;
        const float LeftAngle = 180.0f;

        [SerializeField]
        private CapAnimController animController;
        // 横入力スライダー
        [SerializeField]
        private MoveSliderController moveSliderController;
        // プレイヤーの状態
        public readonly PlayerStatus status = new();
        // プレイヤーのパーツ
        [SerializeField]
        private PlayerParts parts;
        // プレイヤーの動作
        private IPlayerAction action;
        // プレイヤーの動作ステータス
        private IPlayerActionStatus[] playerActionStatus = new IPlayerActionStatus[(int)IPlayerActionStatus.Type.Max];
        //private IPlayerActionStatus actionStatus;
        private IPlayerActionStatus.Type currentPASType;
        // 入力コントローラ
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private LinesController linesController;
        private InputAction moveAction;

        private bool onGoal = false;
        public UnityEvent OnGoalAction = new ();

        void Start()
        {
            OnGoalAction.AddListener(OnGoal);

            Debug.Assert(moveSliderController != null, "横移動入力スライダーをアタッチしてください。");

            parts.CharacterController = GetComponent<CharacterController>();
            parts.Transform = transform;

            action = new PlayerActionA(parts, status);
            playerActionStatus[(int)IPlayerActionStatus.Type.Run] = new PlayerActionStatusRun();
            playerActionStatus[(int)IPlayerActionStatus.Type.Climb] = new PlayerActionStatusClimb();
            playerActionStatus[(int)IPlayerActionStatus.Type.Swing] = new PlayerActionStatusSwing();
            playerActionStatus[(int)IPlayerActionStatus.Type.Goal] = new PlayerActionStatusGoal();
            currentPASType = IPlayerActionStatus.Type.Run;

            foreach (IPlayerActionStatus status in playerActionStatus)
            {
                if (status == null)
                {
                    Debug.LogError("playerActionStatusにnullが指定されています。");
                }
            }

            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.Enable();
        }

        void Update()
        {
            // 状態の更新
            UpdateState();

            // アクションの更新
            UpdateAction();

            animController.SetIsRunning(Mathf.Abs(status.Velocity.x) > MovingVeloDeadZone);
            if (status.IsGrounded && status.InputJump)
            {
                animController.Jump();
            }

            //Debug.Log(status);
        }

        /// <summary>
        /// ゴールした時の処理
        /// </summary>
        public void OnGoal()
        {
            onGoal = true;
            currentPASType = IPlayerActionStatus.Type.Goal;
        }

        void OnDestroy()
        {
            moveAction.Disable();
        }

        /// <summary>
        /// 状態の更新
        /// </summary>
        void UpdateState()
        {
            if (linesController.GetGoalDistanceRate() >= 1.0f)
            {
                OnGoalAction.Invoke();
            }

            status.IsGrounded = parts.CharacterController.isGrounded;
            status.InputJump = inputController.InputJump
#if UNITY_EDITOR
                || Keyboard.current[Key.Space].isPressed;
#endif
            ;
            // MEMO: 横移動入力スライダーに切り替え 元:status.InputMoveX = inputController.InputHorizontal + moveAction.ReadValue<Vector2>().x;
            status.InputMoveX = moveSliderController.Value;

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
            Debug.Log($"UpdateActionPC{currentPASType}");

            if (status.InputMoveX > 0.0f)
            {
                transform.rotation = Quaternion.Euler(0.0f, RightAngle, 0.0f);
            }
            else if (status.InputMoveX < 0.0f)
            {
                transform.rotation = Quaternion.Euler(0.0f, LeftAngle, 0.0f);
            }
            
            if (status.IsClimbing && (status.InputMoveX * status.InputMoveX > 0.0f))
            {
                currentPASType = IPlayerActionStatus.Type.Climb;
            }
            else
            {
                currentPASType = IPlayerActionStatus.Type.Run;
            }

            playerActionStatus[(int)currentPASType].UpdateMove(
                status,
                action,
                type =>
                {
                    currentPASType = type;
                });

            playerActionStatus[(int)currentPASType].UpdateJump(
                status,
                action,
                type =>
                {
                    currentPASType = type;
                });

            parts.CharacterController.Move(status.Velocity * Time.deltaTime);
        }
    }
}
