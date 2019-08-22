using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button backButton = null;

        private void Awake()
        {
            backButton.onClick.AddListener(UIController.s_Instance.OnGoBackToLoginFromMainMenuButtonClickImpl);
        }
    }
}
