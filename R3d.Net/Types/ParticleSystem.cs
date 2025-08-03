using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a CPU-based particle system with various properties and settings.
    /// 
    /// This structure contains configuration data for a particle system, such as mesh information, initial properties,
    /// curves for controlling properties over time, and settings for shadow casting, emission rate, and more
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ParticleSystem
    {
        /// <summary>
        /// Pointer to the array of particles in the system
        /// </summary>
        public Particle* Particles;

        /// <summary>
        /// The maximum number of particles the system can manage
        /// </summary>
        public int Capacity;

        /// <summary>
        /// The current number of active particles in the system
        /// </summary>
        public int Count;

        /// <summary>
        /// The initial position of the particle system. Default: (0, 0, 0)
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The gravity applied to the particles. Default: (0, -9.81, 0)
        /// </summary>
        public Vector3 Gravity;

        /// <summary>
        /// The initial scale of the particles. Default: (1, 1, 1)
        /// </summary>
        public Vector3 InitialScale;

        /// <summary>
        /// The variance in particle scale. Default: 0.0f
        /// </summary>
        public float ScaleVariance;

        /// <summary>
        /// The initial rotation of the particles in Euler angles (degrees). Default: (0, 0, 0)
        /// </summary>
        public Vector3 InitialRotation;

        /// <summary>
        /// The variance in particle rotation in Euler angles (degrees). Default: (0, 0, 0)
        /// </summary>
        public Vector3 RotationVariance;

        /// <summary>
        /// The initial color of the particles. Default: WHITE
        /// </summary>
        public Color InitialColor;

        /// <summary>
        /// The variance in particle color. Default: BLANK
        /// </summary>
        public Color ColorVariance;

        /// <summary>
        /// The initial velocity of the particles. Default: (0, 0, 0)
        /// </summary>
        public Vector3 InitialVelocity;

        /// <summary>
        /// The variance in particle velocity. Default: (0, 0, 0)
        /// </summary>
        public Vector3 VelocityVariance;

        /// <summary>
        /// The initial angular velocity of the particles in Euler angles (degrees). Default: (0, 0, 0)
        /// </summary>
        public Vector3 InitialAngularVelocity;

        /// <summary>
        /// The variance in angular velocity. Default: (0, 0, 0)
        /// </summary>
        public Vector3 AngularVelocityVariance;

        /// <summary>
        /// The lifetime of the particles in seconds. Default: 1.0f
        /// </summary>
        public float Lifetime;

        /// <summary>
        /// The variance in lifetime in seconds. Default: 0.0f
        /// </summary>
        public float LifetimeVariance;

        /// <summary>
        /// Use to control automatic emission, should not be modified manually
        /// </summary>
        public float EmissionTimer;

        /// <summary>
        /// The rate of particle emission in particles per second. Default: 10.0f
        /// </summary>
        public float EmissionRate;

        /// <summary>
        /// The angle of propagation of the particles in a cone (degrees). Default: 0.0f
        /// </summary>
        public float SpreadAngle;

        /// <summary>
        /// Curve controlling the scale evolution of the particles over their lifetime. Default: NULL
        /// </summary>
        public InterpolationCurve* ScaleOverLifetime;

        /// <summary>
        /// Curve controlling the speed evolution of the particles over their lifetime. Default: NULL
        /// </summary>
        public InterpolationCurve* SpeedOverLifetime;

        /// <summary>
        /// Curve controlling the opacity evolution of the particles over their lifetime. Default: NULL
        /// </summary>
        public InterpolationCurve* OpacityOverLifetime;

        /// <summary>
        /// Curve controlling the angular velocity evolution of the particles over their lifetime. Default: NULL
        /// </summary>
        public InterpolationCurve* AngularVelocityOverLifetime;

        /// <summary>
        /// For frustum culling. Defaults to a large AABB; compute manually via `CalculateParticleSystemBoundingBox` after setup
        /// </summary>
        public BoundingBox Aabb;

        /// <summary>
        /// Indicates whether particle emission is automatic when calling `UpdateParticleSystem`.
        /// 
        /// If false, emission is manual using `EmitParticle`. Default: true.
        /// </summary>
        public bool AutoEmission;
    }
}
