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
			throw new System.NotImplementedException();
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
