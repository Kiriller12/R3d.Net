using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Sprite example
    /// </summary>
    internal class SpriteExample : IExample
    {
        private Types.Mesh _plane;
        private Types.Material _material;

        private Texture2D _texture;
        private Sprite _sprite;

        private Camera3D _camera;

        private float birdDirX = 1.0f;
        private Vector3 birdPos = new(0.0f, 0.5f, 0.0f);

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _material = R3d.GetDefaultMaterial();

            _texture = Raylib.LoadTexture("Resources/spritesheet.png");
            _sprite = R3d.LoadSprite(_texture, 4, 1);

            _camera.Position = new Vector3(0, 2, 5);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Spot);
            R3d.LightLookAt(light, new Vector3(0, 10, 10), Vector3.Zero);
            R3d.SetLightActive(light, true);
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            R3d.UpdateSprite(ref _sprite, 10 * deltaTime);

            var birdPosPrev = birdPos;
            var time = (float)Raylib.GetTime();

            birdPos.X = 2.0f * MathF.Sin(time);
            birdPos.Y = 1.0f + MathF.Cos(time * 4.0f) * 0.5f;
            birdDirX = (birdPos.X - birdPosPrev.X >= 0) ? 1 : -1;
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixTranslate(0, -0.5f, 0));
            R3d.DrawSpriteEx(ref _sprite, birdPos, new Vector2(birdDirX, 1.0f), 0.0f);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadSprite(ref _sprite);
            R3d.UnloadMesh(ref _plane);
            Raylib.UnloadTexture(_texture);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}
