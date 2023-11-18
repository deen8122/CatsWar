using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class Constants
    {
        public static readonly float MinVelocity = 0.15f;

        /// <summary>
        /// The collider of the bird is bigger when on idle state
        /// This will make it easier for the user to tap on it
        /// </summary>
        public static readonly float BirdColliderRadiusNormal = 0.235f;
        public static readonly float BirdColliderRadiusBig = 0.5f;
		public static readonly float minCameraX = 0;

		public static readonly int BOX_TYPE_BLOCK = 0;
		public static readonly int BOX_TYPE_ICE = 1;
		public static readonly int BOX_TYPE_PALKA_BOX = 3;
		public static readonly int BOX_TYPE_PALKA_ICE = 4;
	}
}
