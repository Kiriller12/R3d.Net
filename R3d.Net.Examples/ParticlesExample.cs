using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Particles example
    /// </summary>
    internal unsafe class ParticlesExample : IExample
    {
        private Types.Mesh _sphere;
        private Types.Material _material;
        private Skybox _skybox;

        private Camera3D _camera;

        private InterpolationCurve _curve;
        private ParticleSystem _particles;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            R3d.SetBloomMode(Bloom.Additive);
            R3d.SetBackgroundColor(new Color(4, 4, 4));
            R3d.SetAmbientColor(Color.Black);

            _sphere = R3d.GenMeshSphere(0.1f, 16, 32, true);

            _material = R3d.GetDefaultMaterial();
            _material.Emission.Color = new Color(255, 0, 0, 255);
            _material.Emission.Energy = 1.0f;

            _curve = R3d.LoadInterpolationCurve(3);
            R3d.AddKeyframe(ref _curve, 0.0f, 0.0f);
            R3d.AddKeyframe(ref _curve, 0.5f, 1.0f);
            R3d.AddKeyframe(ref _curve, 1.0f, 0.0f);

            _particles = R3d.LoadParticleSystem(2048);
            _particles.InitialVelocity = new Vector3(0, 10.0f, 0);
            _particles.SpreadAngle = 45.0f;
            _particles.EmissionRate = 2048;
            _particles.Lifetime = 2.0f;

            fixed (InterpolationCurve* curve = &_curve)
            {
                _particles.ScaleOverLifetime = curve;
            }

            R3d.CalculateParticleSystemBoundingBox(ref _particles);

            _camera.Position = new Vector3(-7, 7, -7);
            _camera.Target = Vector3.UnitY;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Orbital);
            R3d.UpdateParticleSystem(ref _particles, deltaTime);
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);
            R3d.DrawParticleSystem(ref _particles, ref _sphere, ref _material);
            R3d.End();

            Raylib.BeginMode3D(_camera);
            Raylib.DrawBoundingBox(_particles.Aabb, Color.Green);
            Raylib.EndMode3D();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadInterpolationCurve(_curve);
            R3d.UnloadParticleSystem(ref _particles);

            R3d.UnloadMesh(ref _sphere);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}
