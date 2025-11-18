using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalEffector : MonoBehaviour
{
    [SerializeField]
    private GameObject effectGameObject;
    [SerializeField]
    private GameObject uiGameObject;

    const string ResultSceneName = "ResultScene";

    public void OnGoal()
    {
        //effectGameObject.SetActive(true);
		//uiGameObject.SetActive(true);
        Invoke(nameof(ToResultScene), 3.0f);
    }

    void ToResultScene()
    {
        SceneManager.LoadScene(ResultSceneName);
    }
}
