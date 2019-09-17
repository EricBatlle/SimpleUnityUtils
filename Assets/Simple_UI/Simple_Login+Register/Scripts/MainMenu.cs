using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    /// <summary>
    /// Class in charge of implementing the first screen (MainMenu) behaviuor after login
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        //Used to let inheritance and abstraction works and set events when the controller is assigned
        [SerializeField] private LoginRegisterController lrController = null;
        public LoginRegisterController LRController
        {
            get { return lrController; }
            set { lrController = value; SetOnClickEvents(); }   //assign new value and set button OnclickEvents
        }

        [Header("Buttons")]
        [SerializeField] private Button backButton = null;

        private void SetOnClickEvents()
        {
            backButton.onClick.AddListener(LRController.OnGoBackToLoginFromMainMenuButtonClickImpl);
        }
    }
}
