/*
* Player Action Status Bundle A
*/

using UnityEngine;

namespace PlayScene
{
    /// <summary>
    /// プレイヤーのステータス-走る
    /// </summary>
    public class PlayerActionStatusGoal : IPlayerActionStatus
    {
        public void UpdateJump(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            if (status.IsClimbing)
            {
                action.Climb(status.InputMoveX);
            }
            else if (status.IsGrounded)
            {
                action.Landing();  // 地面に触れているなら着地処理

                status.IsJumping = false;
            }
            else
            {
                action.Gravity();  // 重力適用
            }
        }

        public void UpdateMove(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            action.Move(1.0f);
        }
    }
    /// <summary>
    /// プレイヤーのステータス-走る
    /// </summary>
    public class PlayerActionStatusRun : IPlayerActionStatus
    {
        public void UpdateJump(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            Debug.Log("act");
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
                        Debug.Log("jump");
                        status.IsJumping = true;
                    }
                }
            }
            else
            {
                action.Gravity();  // 重力適用
            }
        }

        public void UpdateMove(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            if (status.InputMoveX * status.InputMoveX > Config.Input.MoveDeadzoneSquared)
            {
                float move = status.InputMoveX;
                if (status.IsDussing)
                {
                    move *= PlayerStatus.DushRate;
                }
                action.Move(move);  // 入力がデッドゾーンより大きいなら移動アクション
            }
        }
    }

    /// <summary>
    /// プレイヤーのステータス-登る
    /// </summary>
    public class PlayerActionStatusClimb : IPlayerActionStatus
    {
        public void UpdateJump(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            Debug.Log("UpdateJump --------- CLIMB");
            if (status.IsClimbing)
            {
                action.Landing();
                if (status.InputJump)
                {
                    status.IsJumping = true;
                }
            }
            else
            {
                //action.Gravity();  // 重力適用
            }
        }

        public void UpdateMove(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            if (status.IsTouchWallForward)
            {
                if (status.InputMoveX * status.InputMoveX > Config.Input.MoveDeadzoneSquared)
                {
                    float move = status.InputMoveX;
                    if (status.IsDussing)
                    {
                        move *= PlayerStatus.DushRate;
                    }
                    action.Climb(move);  // 入力がデッドゾーンより大きいなら移動アクション
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーのステータス-天井伝い
    /// </summary>
    public class PlayerActionStatusSwing : IPlayerActionStatus
    {
        public void UpdateJump(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
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
        }

        public void UpdateMove(
            PlayerStatus status,
            IPlayerAction action,
            System.Action<IPlayerActionStatus.Type> changeStatus)
        {
            //Debug.Log("UpdateActionSwing");
            if (status.InputMoveX * status.InputMoveX > Config.Input.MoveDeadzoneSquared)
            {
                float move = status.InputMoveX;
                if (status.IsDussing)
                {
                    move *= PlayerStatus.DushRate;
                }
                action.Move(move);  // 入力がデッドゾーンより大きいなら移動アクション
            }
        }
    }
}
