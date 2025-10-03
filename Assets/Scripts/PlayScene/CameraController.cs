using UnityEngine;
using PlayScene;

[RequireComponent(typeof(Transform))]
public class CameraController : MonoBehaviour
{
    private new Transform transform;
    [SerializeField]
    private Transform playerTransform;
    // [SerializeField]
    // private Transform cameraPointsTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();

        Debug.Assert(playerTransform != null, "プレイヤーのトランスフォームが指定されてないよ");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの方向ベクトル
        Vector3 dir = Vector3.Normalize(playerTransform.position - transform.position);
        // 回転する
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir, Vector3.up),
            Config.Camera.LookingRateSec * Time.deltaTime);


    }
}
