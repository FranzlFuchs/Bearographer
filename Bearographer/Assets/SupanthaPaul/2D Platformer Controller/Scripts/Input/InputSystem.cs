using UnityEngine;

namespace SupanthaPaul
{
    public class InputSystem : MonoBehaviour
    {
        // input string caching
        static readonly string HorizontalInput = "Horizontal";

        static readonly string HorizontalInputAD = "HorizontalAD";

        static readonly string HorizontalInputArrow = "HorizontalArrow";

        static readonly string JumpInput = "Jump";

        static readonly string DashInput = "Dash";

        public static float HorizontalRaw()
        {
            return Input.GetAxisRaw(HorizontalInput);
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
            Debug.Log("JUMP");
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
