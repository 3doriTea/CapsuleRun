using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

namespace WalkScene
{
    public class WalkingManager : MonoBehaviour
    {
        const string ActivityRecognition = "android.permission.ACTIVITY_RECOGNITION";
        const int HistoryCapacity = 1000;

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
        private static WaitForSeconds stepCountDelay = new(3.0f);
        private int StepCount  // 計測開始からの歩数
        {
            get
            {
                return previousStepCount - startStepCount;
            }
        }
        private float TotalDistance
        {
            get
            {
                double totalDistance = 0.0;
                for (int i = 1; i < history.Count; i++)
                {
                    int prev = i - 1;
                    int curr = i;
                    totalDistance += GetDistanceKm(
                        history[prev].latitude,
                        history[prev].longitude,
                        history[curr].latitude,
                        history[curr].longitude);
                }
                return (float)totalDistance;

                static double GetDistanceKm(double x1, double y1, double x2, double y2)
                {
                    const double EARTH_RADIUS_KM = 6371.0;

                    double dx = (x2 - x1) * Math.PI / 180.0;
                    double dy = (y2 - y1) * Math.PI / 180.0;
                    double rX1 = x1 * Math.PI / 180.0;
                    double rX2 = x2 * Math.PI / 180.0;

                    double a = Math.Sin(dx / 2) * Math.Sin(dx / 2)
                        + Math.Sin(dy / 2) * Math.Sin(dy / 2) * Math.Cos(rX1) * Math.Cos(rX2);
                    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                    return EARTH_RADIUS_KM * c;
                }
            }
        }
        private readonly List<LocationStamp> history = new(HistoryCapacity);  // 座標履歴

        private int startStepCount = -1;
        private int previousStepCount = -1;
        private int waitingCounter = 0;  // 停止中のカウント
        private LocationStamp startPosition = new() { latitude = -1.0f, longitude = -1.0f, timestamp = -1.0 };

        [SerializeField]
        private WalkingController walkingController;
        [SerializeField]
        private InfoController infoController;

        public void DestroyMe()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            Debug.Assert(walkingController != null, "walkingController is null");
            Debug.Assert(infoController != null, "infoController is null");

            if (!Permission.HasUserAuthorizedPermission(ActivityRecognition))
            {
                Permission.RequestUserPermission(ActivityRecognition);
            }

            InputSystem.EnableDevice(StepCounter.current);

            StartCoroutine(UpdateLocation());
            StartCoroutine(UpdateStepCounter());
        }

        private void Update()
        {
        }

        private void CheckMoving()
        {
            Debug.Log($"Steps = previousStepCount:{previousStepCount} - startStepCount:{startStepCount}");
            infoController.SetStepCount(StepCount);
            float totalDistance = TotalDistance;
            infoController.SetDistance(totalDistance);
            Debug.Log($"dist:{totalDistance}, historyCount:{history.Count}");
            if (history.Count > 0)
            {
                infoController.SetStartTime(ToLocalDateTime(history[0].timestamp));
            }

            static DateTime ToLocalDateTime(double time)
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(time)
                    .ToLocalTime();
            }
        }

        private void AddStep(int add)
        {
            const int IsStopWaitCount = 3;
            if (add == 0)
            {
                waitingCounter++;
                if (waitingCounter > IsStopWaitCount)
                {
                    walkingController.MoveScene(WalkingController.InnerScene.Stop);
                }
            }
            else
            {
                waitingCounter = 0;
                walkingController.MoveScene(WalkingController.InnerScene.Walking);
            }
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
                if (history[^1].PositionEquals(info))
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
                    CheckMoving();
                }
                yield return _waitForSeconds1_0;
            }
        }

        /// <summary>
        /// 歩数の更新処理コルーチン
        /// </summary>
        /// <returns>コルーチン</returns>
        private IEnumerator UpdateStepCounter()
        {
            StepCounter stepCounter = StepCounter.current;
            if (stepCounter == null)
            {
                Debug.Log("Failed get step counter service.");
                yield break;
            }

            if (!stepCounter.enabled)
            {
                Debug.Log("Failed: The service is not working with the step counter.");
                yield break;
            }

            while (true)
            {
                int currentStepCount = stepCounter.stepCounter.ReadValue();

                if (currentStepCount > 0)
                {

                    if (previousStepCount < 0)
                    {
                        previousStepCount = currentStepCount;
                        Debug.Log($"初回起動prevStep：{previousStepCount}");
                    }
                    if (startStepCount < 0)
                    {
                        startStepCount = currentStepCount;
                        Debug.Log($"初回起動startStepCount：{startStepCount}");
                    }

                    AddStep(currentStepCount - previousStepCount);
                    previousStepCount = currentStepCount;
                }

                yield return stepCountDelay;
            }
        }
    }
}
