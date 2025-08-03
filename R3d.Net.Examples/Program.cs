using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initializing raylib and r3d
            Raylib.InitWindow(800, 600, "[r3d/R3d.Net] - Basic example");
            R3d.Init(800, 600, 0);
            Raylib.SetTargetFPS(60);

            // Setting up the scene
            var plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            var sphere = R3d.GenMeshSphere(0.5f, 64, 64, true);
            var material = R3d.GetDefaultMaterial();

            var light = R3d.CreateLight(LightType.Spot);
            R3d.LightLookAt(light, new Vector3(0, 10, 5), Vector3.Zero);
            R3d.EnableShadow(light, 4096);
            R3d.SetLightActive(light, true);

            var camera = new Camera3D
            {
                Position = new Vector3(0, 2, 2),
                Target = Vector3.Zero,
                Up = Vector3.UnitY,
                FovY = 60.0f,
                Projection = CameraProjection.Perspective
            };

            // Main rendering loop
            while (!Raylib.WindowShouldClose())
            {
                Raylib.UpdateCamera(ref camera, CameraMode.Orbital);

                Raylib.BeginDrawing();
                R3d.Begin(camera);

                R3d.DrawMesh(ref plane, ref material, Raymath.MatrixTranslate(0, -0.5f, 0));
                R3d.DrawMesh(ref sphere, ref material, Raymath.MatrixIdentity());

                R3d.End();
                Raylib.EndDrawing();
            }

            // Cleaning memory
            R3d.UnloadMesh(ref plane);
            R3d.UnloadMesh(ref sphere);

            // Closing r3d and raylib
            R3d.Close();
            Raylib.CloseWindow();
        }
    }
}
