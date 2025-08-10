![Logo](https://raw.githubusercontent.com/Kiriller12/R3d.Net/refs/heads/master/R3d.Net/Resources/Logo256.png "Logo")

# R3d.Net

C# bindings for [R3D](https://github.com/Bigfoot71/r3d), advanced 3D rendering library for [raylib](https://www.raylib.com)

[![GitHub contributors](https://img.shields.io/github/contributors/Kiriller12/R3d.Net)](https://github.com/Kiriller12/R3d.Net/graphs/contributors)
[![License](https://img.shields.io/badge/license-zlib%2Flibpng-blue.svg)](LICENSE)
[![Chat on Discord](https://img.shields.io/discord/426912293134270465.svg?logo=discord)](https://discord.gg/raylib)
[![NuGet Version](https://img.shields.io/nuget/v/R3d.Net)](https://www.nuget.org/packages/R3d.Net/)

R3d.Net targets net8.0 and uses the [R3D 0.3 or later](https://github.com/Bigfoot71/r3d) to build the native libraries. [Raylib-cs 7.0.1](https://github.com/raylib-cs/raylib-cs) is used as nuget dependency.

R3d.Net is heavily inspired by [raylib-cs](https://github.com/raylib-cs/raylib-cs) and uses some of its functionality to implement bindings.

## Status

Currently in active development, available API is subject to change.

## Installation - NuGet

You can add package to the project with this command:

```
dotnet add package R3d.Net
```

## Installation - Manual

1. Download/clone the repo

2. Add [R3d.Net/R3d.Net.csproj](R3d.Net/R3d.Net.csproj) to your project as an existing project.

3. Download/clone the original [R3D repo](https://github.com/Bigfoot71/r3d) with all dependencies.

4. Build R3D's native dependencies and place them in the same directory as the executable. Required files: `r3d.dll` and `assimp.dll` or `assimp-vc143-mt.dll`.

5. You should now be able to use R3d.Net in your projects.

## Basic R3D example in R3d.Net

```csharp
using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize raylib window
            Raylib.InitWindow(800, 600, "R3D Example");

            // Initialize R3D Renderer with default settings
            R3d.Init(800, 600, 0);

            // Load a model to render
            // 'true' indicates that we upload immediately to the GPU
            var mesh = R3d.GenMeshSphere(1.0f, 16, 32, true);

            // Get a material with default values
            var material = R3d.GetDefaultMaterial();

            // Create a directional light
            // NOTE: The direction will be normalized
            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(-1, -1, -1));
            R3d.SetLightActive(light, true);

            // Init a Camera3D
            var camera = new Camera3D
            {
                Position = new Vector3(-3, 3, 3),
                Target = Vector3.Zero,
                Up = Vector3.UnitY,
                FovY = 60.0f,
                Projection = CameraProjection.Perspective
            };

            // Main rendering loop
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                R3d.Begin(camera);
                R3d.DrawMesh(ref mesh, ref material, Raymath.MatrixIdentity());
                R3d.End();
                Raylib.EndDrawing();
            }

            // Close R3D renderer and raylib
            R3d.UnloadMesh(ref mesh);
            R3d.Close();
            Raylib.CloseWindow();
        }
    }
}

```

Other examples can be found in the [R3d.Net.Examples](R3d.Net.Examples) folder. All examples were ported from the original R3D library.

## Contributing

The library still requires a lot of improvements and may contain some issues.

Feel free to open an issue or submit a pull request.

## License

Licenced under the Zlib License. 

See [LICENSE](LICENSE) for details.

## Acknowledgements

A huge thanks to the original [raylib](https://www.raylib.com), [r3d](https://github.com/Bigfoot71/r3d) and [raylib-cs](https://github.com/raylib-cs/raylib-cs) developers for their enormous contributions to our raylib community.