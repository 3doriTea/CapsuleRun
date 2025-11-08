using UnityEngine;
using TMPro;

namespace PlayScene
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMesh;
        private float timer = 0.0f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            int minutes = (int)(timer / 60.0f);

            int seconds = (int)(timer % 60.0f);

            int milliseconds = (int)(timer % 1.0f * 100.0f);

            textMesh.SetText($"{minutes:D2} : {seconds:D2} . {milliseconds:D2}");
        }
    }
}
