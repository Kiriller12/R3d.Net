using Raylib_cs;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Application
    /// </summary>
    internal class Program
    {
        private const int Width = 800;
        private const int Height = 600;

        /// <summary>
        /// Examples list
        /// </summary>
        private static readonly List<ExampleDescription> Examples = new()
        {
            new ExampleDescription("Basic example", () => new BasicExample()),
            new ExampleDescription("Animation example", () => new AnimationExample(), "Model made by zhuoyi0904"),
            new ExampleDescription("Bloom example", () => new BloomExample()),
            new ExampleDescription("Directional light example", () => new DirectionalExample()),
            new ExampleDescription("Emission example", () => new EmissionExample(), "Model by har15204405"),
            new ExampleDescription("Fog example", () => new FogExample()),
            new ExampleDescription("Instanced rendering example", () => new InstancedExample()),
            new ExampleDescription("Many lights example", () => new LightsExample()),
            new ExampleDescription("Particles example", () => new ParticlesExample()),
            new ExampleDescription("PBR car example", () => new PbrCarExample(), "Model made by MaximePages"),
            new ExampleDescription("PBR musket example", () => new PbrMusketExample(), "Model made by TommyLingL"),
            new ExampleDescription("Resize example", () => new ResizeExample()),
            new ExampleDescription("Skybox example", () => new SkyboxExample()),
            new ExampleDescription("Sponza example", () => new SponzaExample()),
            new ExampleDescription("Sprite example", () => new SpriteExample()),
            new ExampleDescription("Instanced sprites example", () => new InstancedSpritesExample()),
            new ExampleDescription("Transparency example", () => new TransparencyExample())
        };

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Enter the number of the exemple to run:");
            Console.WriteLine();

            for (var i = 0; i < Examples.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Examples[i].Name}");
            }

            Console.WriteLine();
            Console.Write("Example: ");

            var exampleDescription = GetExample();
            if (exampleDescription is null)
            {
                Console.ReadKey();

                return;
            }

            Console.WriteLine("Running example: \"{0}\"", exampleDescription.Name);
            Console.WriteLine();

            using var example = exampleDescription.Create();

            Raylib.InitWindow(Width, Height, "[r3d/R3d.Net] - " + exampleDescription.Name);
            example.Init(Width, Height);

            while (!Raylib.WindowShouldClose())
            {
                var deltaTime = Raylib.GetFrameTime();
                example.Update(deltaTime);

                Raylib.BeginDrawing();

                example.Render();
                RenderCredits(exampleDescription.Credits);
                Raylib.DrawFPS(10, 10);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        /// <summary>
        /// Returns an example to run
        /// </summary>
        private static ExampleDescription? GetExample()
        {
            try
            {
                var input = Console.ReadLine();
                var index = Convert.ToInt32(input);

                return Examples[index - 1];
            }
            catch
            {
                Console.WriteLine("Please select a valid number from list above!");

                return null;
            }
        }

        /// <summary>
        /// Renders credits text
        /// </summary>
        /// <param name="text">Credits text</param>
        private static void RenderCredits(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            var len = Raylib.MeasureText(text, 16);

            Raylib.DrawRectangle(0, Height - 36, 20 + len, 36, Raylib.ColorAlpha(Color.Black, 0.5f));
            Raylib.DrawRectangleLines(0, Height - 36, 20 + len, 36, Color.Black);
            Raylib.DrawText(text, 10, Height - 26, 16, Color.Lime);
        }
    }
}
