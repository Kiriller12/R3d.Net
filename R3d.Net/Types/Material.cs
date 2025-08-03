using Raylib_cs;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a material with textures, parameters, and rendering modes.
    /// 
    /// Combines multiple texture maps and settings used during shading
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Material
    {
        /// <summary>
        /// Albedo map
        /// </summary>
        public MapAlbedo Albedo;

        /// <summary>
        /// Emission map
        /// </summary>
        public MapEmission Emission;

        /// <summary>
        /// Normal map
        /// </summary>
        public MapNormal Normal;

        /// <summary>
        /// Occlusion-Roughness-Metalness map
        /// </summary>
        public MapOcclusionRoughnessMetalness Orm;

        /// <summary>
        /// Blend mode used for rendering the material
        /// </summary>
        public BlendMode3D BlendMode;

        /// <summary>
        /// Face culling mode used for the material
        /// </summary>
        public CullMode CullMode;

        /// <summary>
        /// Shadow casting mode for the object
        /// </summary>
        public ShadowCastMode ShadowCastMode;

        /// <summary>
        /// Billboard mode applied to the object
        /// </summary>
        public BillboardMode BillboardMode;

        /// <summary>
        /// Alpha threshold below which fragments are discarded
        /// </summary>
        public float AlphaCutoff;
    }

    /// <summary>
    /// Albedo map
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MapAlbedo
    {
        /// <summary>
        /// Albedo (base color) texture
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Albedo color multiplier
        /// </summary>
        public Color Color;
    }

    /// <summary>
    /// Emission map
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MapEmission
    {
        /// <summary>
        /// Emission texture
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Emission color
        /// </summary>
        public Color Color;

        /// <summary>
        /// Emission energy multiplier
        /// </summary>
        public float Energy;
    }

    /// <summary>
    /// Normal map
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MapNormal
    {
        /// <summary>
        /// Normal map texture
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Normal scale
        /// </summary>
        public float Scale;
    }

    /// <summary>
    /// Occlusion-Roughness-Metalness map
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MapOcclusionRoughnessMetalness
    {
        /// <summary>
        /// Combined Occlusion-Roughness-Metalness texture
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Occlusion multiplier
        /// </summary>
        public float Occlusion;

        /// <summary>
        /// Roughness multiplier
        /// </summary>
        public float Roughness;

        /// <summary>
        /// Metalness multiplier
        /// </summary>
        public float Metalness;
    }
}
