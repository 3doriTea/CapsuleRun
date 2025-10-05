using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayScene
{
    /// <summary>
    /// パーツの種類
    /// </summary>
    public enum PlayerPartsType
    {
        Head,
        HandLeft,
        HandRight,
    }

    /// <summary>
    /// プレイヤーのパーツ
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class PlayerPartsController : MonoBehaviour
    {
        private readonly List<int> hitInstanceId = new();

        [SerializeField]
        private PlayerPartsType type;

        /// <summary>
        /// このパーツが壁に触れているか
        /// </summary>
        /// <returns>壁に触れている true / false</returns>
        public bool IsTouching()
        {
            return hitInstanceId.Count > 0;
        }

        /// <summary>
        /// このパーツの種類を取得する
        /// </summary>
        /// <returns>プレイヤーパーツの種類</returns>
        public PlayerPartsType GetPartsType()
        {
            return type;
        }

        void OnTriggerEnter(Collider other)
        {
            int otehrIId = other.GetInstanceID();
            if (hitInstanceId.Any(elementId => elementId == otehrIId))
            {
                return;
            }

            hitInstanceId.Add(otehrIId);
        }

        void OnTriggerExit(Collider other)
        {
            int otherIId = other.GetInstanceID();
            int foundIndex = hitInstanceId.IndexOf(otherIId);
            if (foundIndex >= 0)
            {
                hitInstanceId.RemoveAt(foundIndex);
            }
        }
    }
}
