using UnityEngine;

namespace Assets.ScrollingShooter.Scripts.Helpers
{
    public static class UnityHelper
    {
        public enum ScrollingShooterLayers
        {
            Enemies = 8,
            Player = 9,
        }

        public static void SetLayerRecursively(
            GameObject obj,
            int newLayer )
        {
            if( obj == null )
                return;

            obj.layer = newLayer;

            foreach( Transform child in obj.transform )
                SetLayerRecursively( child.gameObject, newLayer );
        }
    }
}