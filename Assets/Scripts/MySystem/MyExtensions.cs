using UnityEngine;

namespace MySystem
{
    public static class MyExtensions
    {
        public static Vector2 ClampToScreen(Vector2 position)
        {
            return new Vector2(Mathf.Clamp(position.x, 0, Screen.width), 
                                                Mathf.Clamp(position.y, 0, Screen.height));
        }
    }
}