using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        bool rightMove = false;
        bool leftMove = false;
        public string Name;

        void OnEnable()
        {

        }

        void Update()
        {
            if (rightMove)
                CrossPlatformInputManager.SetAxisPositive(Name);

            if (leftMove)
                CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            rightMove = true;
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            rightMove = false;
            leftMove = false;
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            leftMove = true;
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

       
    }
}
