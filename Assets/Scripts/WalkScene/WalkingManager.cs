using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalkScene;

public class WalkingManager : MonoBehaviour
{
    struct LocationStamp
    {
        public float latitude;  // 緯度
        public float longitude;  // 経度
        public double timestamp;  // 時刻

        public readonly bool PositionEquals(LocationInfo info)
        {
            return latitude == info.latitude
                && longitude == info.longitude;
        }

        // override object.Equals
        public readonly override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (obj is LocationStamp other)
            {
                return latitude == other.latitude
                && longitude == other.longitude
                && timestamp == other.timestamp;
            }

            if (obj is LocationInfo info)
            {
                return latitude == info.latitude
                && longitude == info.longitude
                && timestamp == info.timestamp;
            }

            return false;
        }

        public readonly override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    private static WaitForSeconds _waitForSeconds1_0 = new(1.0f);
    private int prevStepCount = 0;  // 歩数
    private LocationInfo prevPosition;  // 緯度経度
    private readonly List<LocationStamp> history = new();  // 座標履歴

    private int startStepCount = -1;
    private LocationStamp startPosition = new() { latitude = -1.0f, longitude = -1.0f, timestamp = -1.0 };

    [SerializeField]
    private WalkingController walkingController;
    [SerializeField]
    private InfoController infoController;

    private void Start()
    {
        StartCoroutine(UpdateLocation());
    }

    private void Update()
    {
    }

    private void CheckMoving()
    {

    }

    /// <summary>
    /// 試しに位置情報を追加する
    /// </summary>
    /// <param name="info">新規/重複不明な位置情報</param>
    /// <returns>追加した true / false 重複で追加されなかった</returns>
    private bool TryAddHistory(in LocationInfo info)
    {
        if (history.Count <= 0)  // 履歴なしなら追加
        {
            AddHistory(info);
            startPosition = history[0];
        }
        else
        {
            if (history[^1].Equals(info))
            {
                return false;
            }
            AddHistory(info);
        }
        return true;

        /// <summary>
        /// 座標履歴に追加する
        /// </summary>
        /// <param name="info">Unityのロケーションサービスから取得した場所情報</param>
        void AddHistory(in LocationInfo info)
        {
            history.Add(new LocationStamp
            {
                latitude = info.latitude,
                longitude = info.longitude,
                timestamp = info.timestamp
            });
        }
    }

    /// <summary>
    /// 位置情報の更新処理コルーチン
    /// </summary>
    /// <returns>コルーチン</returns>
    private IEnumerator UpdateLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Failed to enabled location service by user.");
            yield break;
        }
        Input.location.Start();

        // 初期化を20秒待つ
        int waitLeft = 20;
        while (Input.location.status == LocationServiceStatus.Initializing
            && waitLeft > 0)
        {
            yield return _waitForSeconds1_0;
            waitLeft--;
        }

        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.Log("Failed init location service.");
            yield break;
        }

        while (true)
        {
            LocationInfo locationInfo = Input.location.lastData;
            if (TryAddHistory(locationInfo))
            {
                Debug.Log("Updated location!!!");
            }
            yield return _waitForSeconds1_0;
        }
    }
}
