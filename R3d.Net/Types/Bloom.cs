namespace R3d.Net.Types
{
    /// <summary>
    /// Bloom effect modes.
    /// 
    /// Specifies different post-processing bloom techniques that can be applied
    /// to the rendered scene. Bloom effects enhance the appearance of bright areas
    /// by simulating light bleeding, contributing to a more cinematic and realistic look.
    /// </summary>
    public enum Bloom
    {
        /// <summary>
        /// Bloom effect is disabled. The scene is rendered without any glow enhancement
        /// </summary>
        Disabled,

        /// <summary>
        /// Blends the bloom effect with the original scene using linear interpolation (Lerp)
        /// </summary>
        Mix,

        /// <summary>
        /// Adds the bloom effect additively to the scene, intensifying bright regions
        /// </summary>
        Additive,

        /// <summary>
        /// Combines the scene and bloom using screen blending, which brightens highlights
        /// </summary>
        Screen
    }
}
