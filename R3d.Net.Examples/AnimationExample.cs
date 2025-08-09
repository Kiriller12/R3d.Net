using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Animation example
    /// </summary>
    internal unsafe class AnimationExample : IExample
    {
        private Types.Mesh _plane;
        private Types.Model _dancer;
        private Types.Material _material;
        private readonly Matrix4x4[] _instances = new Matrix4x4[2 * 2];

        private Camera3D _camera;

        private readonly uint[] _lights = new uint[2];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.Fxaa | ConfigFlag.NoFrustrumCulling);
            Raylib.SetTargetFPS(60);

            R3d.SetSSAO(true);
            R3d.SetBloomIntensity(0.03f);
            R3d.SetBloomMode(Bloom.Additive);
            R3d.SetTonemapMode(Tonemap.Aces);

            R3d.SetBackgroundColor(Color.Black);
            R3d.SetAmbientColor(new Color(7, 7, 7, 255));

            _plane = R3d.GenMeshPlane(32, 32, 1, 1, true);
            _dancer = R3d.LoadModel("Resources/dancer.glb");
            _material = R3d.GetDefaultMaterial();

            for (var z = 0; z < 2; z++)
            {
                for (var x = 0; x < 2; x++)
                {
                    _instances[z * 2 + x] = Raymath.MatrixTranslate(x - 0.5f, 0, z - 0.5f);
                }
            }

            var checkedImage = Raylib.GenImageChecked(256, 256, 4, 4, new Color(20, 20, 20, 255), Color.White);
            _material.Albedo.Texture = Raylib.LoadTextureFromImage(checkedImage);
            Raylib.UnloadImage(checkedImage);

            _material.Orm.Roughness = 0.5f;
            _material.Orm.Metalness = 0.5f;

            var anims = R3d.LoadModelAnimations("Resources/dancer.glb", 60);
            fixed (Types.ModelAnimation* anim = &anims[0])
            {
                _dancer.Anim = anim;
            }

            _camera.Position = new Vector3(0, 2.0f, 3.5f);
            _camera.Target = new Vector3(0, 1.0f, 1.5f);
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            _lights[0] = R3d.CreateLight(LightType.Omnidirectional);
            R3d.SetLightPosition(_lights[0], new Vector3(-10.0f, 25.0f, 0.0f));
            R3d.EnableShadow(_lights[0], 4096);
            R3d.SetLightActive(_lights[0], true);

            _lights[1] = R3d.CreateLight(LightType.Omnidirectional);
            R3d.SetLightPosition(_lights[1], new Vector3(10.0f, 25.0f, 0.0f));
            R3d.EnableShadow(_lights[1], 4096);
            R3d.SetLightActive(_lights[1], true);

            Raylib.DisableCursor();
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Free);
            _dancer.AnimFrame++;

            var time = Raylib.GetTime();
            R3d.SetLightColor(_lights[0], Raylib.ColorFromHSV(90.0f * (float)time + 90.0f, 1.0f, 1.0f));
            R3d.SetLightColor(_lights[1], Raylib.ColorFromHSV(90.0f * (float)time - 90.0f, 1.0f, 1.0f));
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixIdentity());
            R3d.DrawModel(ref _dancer, new Vector3(0, 0, 1.5f), 1.0f);
            R3d.DrawModelInstanced(ref _dancer, _instances, 2 * 2);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _plane);
            R3d.UnloadModel(ref _dancer, true);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}
