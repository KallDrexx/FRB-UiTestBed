using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;

#if !FRB_MDX
using System.Linq;
#endif

namespace UiTestBed
{
	internal static class CameraSetup
	{
		internal static void SetupCamera(Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager)
		{
			FlatRedBallServices.GraphicsOptions.SetResolution(800, 600);
			#if WINDOWS_PHONE || WINDOWS_8
			graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
			#endif
			cameraToSetUp.UsePixelCoordinates(false, 800, 600);



		}
	}
}
