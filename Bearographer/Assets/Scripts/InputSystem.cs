using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControllerInputSysten
{
    public class InputSystem : MonoBehaviour
    {
        // input string caching
        static readonly string HorizontalInput = "Horizontal";
        static readonly string VerticalInput = "Vertical";

        static readonly string HorizontalInputAD = "HorizontalAD";

        static readonly string HorizontalInputArrow = "HorizontalArrow";

        static readonly string JumpInput = "Jump";

        static readonly string DashInput = "Dash";

        public static float HorizontalRaw()
        {
            return Input.GetAxisRaw(HorizontalInput);
        }

        public static float VerticalRaw()
        {
            return Input.GetAxisRaw(VerticalInput);
        }

        public static float HorizontalRawArrows()
        {
            return Input.GetAxisRaw(HorizontalInputArrow);
        }

        public static float HorizontalRawAD()
        {
            return Input.GetAxisRaw(HorizontalInputAD);
        }

        public static bool Jump()
        {
            return Input.GetButtonDown(JumpInput);
        }

        public static bool JumpUp()
        {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }

        public static bool JumpW()
        {
            return Input.GetKeyDown(KeyCode.W);
        }

        public static bool Dash()
        {
            return Input.GetButtonDown(DashInput);
        }

        public static bool ChangeBear()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}
