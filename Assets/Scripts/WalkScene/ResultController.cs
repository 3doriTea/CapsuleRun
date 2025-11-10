using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WalkScene;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshDistance;
    [SerializeField]
    private TextMeshProUGUI textMeshSteps;

    [SerializeField]
    private GameObject infoGameObject;
    [SerializeField]
    private WalkingManager walkingManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoGameObject.SetActive(false);

        textMeshDistance.text = $"{walkingManager.TotalDistance:F2} km移動したよ";
        textMeshSteps.text = $"{walkingManager.StepCount} 歩だったよ！";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
