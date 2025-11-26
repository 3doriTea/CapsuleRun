using UnityEngine;
using UnityEngine.UI;

namespace WalkScene
{
    public class CheckButtonController : MonoBehaviour
    {
        
        [SerializeField]
        private Toggle handWashToggle;
        
        [SerializeField]
        private Toggle gragleToggle;

        [SerializeField]
        private GameObject handWashText;
        
        [SerializeField]
        private GameObject gragleText;

        public void OnCheckedGragle(bool onCheck)
        {
            gragleText.SetActive(onCheck);
            gragleToggle.interactable = false;
        }
        public void OnCheckedHandWash(bool onCheck)
        {
            handWashText.SetActive(onCheck);
            handWashToggle.interactable = false;
        }
    }
}
