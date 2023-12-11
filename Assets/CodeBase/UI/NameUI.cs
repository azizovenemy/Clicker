using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class NameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text enemyName;

        public void UpdateName(string value) => 
            enemyName.text = value;
    }
}