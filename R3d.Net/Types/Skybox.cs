using Raylib_cs;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Structure representing a skybox and its related textures for lighting.
    /// 
    /// This structure contains textures used for rendering a skybox, as well as
    /// precomputed lighting textures used for image-based lighting (IBL).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Skybox
    {
        /// <summary>
        /// The skybox cubemap texture for the background and reflections
        /// </summary>
        Texture2D cubemap;

        /// <summary>
        /// The irradiance cubemap for diffuse ambient lighting
        /// </summary>
        Texture2D irradiance;

        /// <summary>
        /// The prefiltered cubemap for specular reflections with mipmaps
        /// </summary>
        Texture2D prefilter;
    }
}
