namespace R3d.Net.Types
{
    /// <summary>
    /// Defines the shadow casting mode for objects in the scene.
    /// 
    /// Determines how an object contributes to shadow mapping, which can affect
    /// performance and visual accuracy depending on the rendering technique used
    /// </summary>
    public enum ShadowCastMode
    {
        /// <summary>
        /// The object does not cast shadows
        /// </summary>
        Disabled,

        /// <summary>
        /// Only front-facing polygons cast shadows
        /// </summary>
        CastFrontFaces,

        /// <summary>
        /// Only back-facing polygons cast shadows
        /// </summary>
        CastBackFaces,

        /// <summary>
        /// Both front and back-facing polygons cast shadows
        /// </summary>
        CastAllFaces
    }
}