using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class WidgetDamageValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text textValue;

        public void SetValue(string newValue) => 
            textValue.text = newValue;

        public void SetColor(Color color) => 
            textValue.color = color;

        private void Handle_AnimationOver() => 
            Destroy(gameObject);
    }
}