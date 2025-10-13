/*
* Player Action Status Bundle A
*/

using UnityEngine;

namespace PlayScene
{
	/// <summary>
	/// プレイヤーのステータス-走る
	/// </summary>
	public class PlayerActionStatusRun : IPlayerActionStatus
	{
		public void UpdateJump(PlayerStatus status, IPlayerAction action)
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

		public void UpdateMove(PlayerStatus status, IPlayerAction action)
		{
			if (status.InputMoveX * status.InputMoveX > Config.Input.MoveDeadzoneSquared)
			{
				action.Move(status.InputMoveX);  // 入力がデッドゾーンより大きいなら移動アクション
			}
		}
	}
}
