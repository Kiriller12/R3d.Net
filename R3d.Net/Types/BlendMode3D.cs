namespace R3d.Net.Types
{
    /// <summary>
    /// Blend modes for rendering.
    /// 
    /// Defines common blending modes used in 3D rendering to combine source and destination colors
    /// </summary>
    /// <remarks>
    /// The blend mode is applied only if you are in forward rendering mode or auto-detect mode
    /// </remarks>
    public enum BlendMode3D
    {
        /// <summary>
        /// No blending, the source color fully replaces the destination color
        /// </summary>
        Opaque,

        /// <summary>
        /// Alpha blending: source color is blended with the destination based on alpha value
        /// </summary>
        Alpha,

        /// <summary>
        /// Additive blending: source color is added to the destination, making bright effects
        /// </summary>
        Additive,

        /// <summary>
        /// Multiply blending: source color is multiplied with the destination, darkening the image
        /// </summary>
        Multiply
    }
}
