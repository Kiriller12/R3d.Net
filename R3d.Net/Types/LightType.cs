namespace R3d.Net.Types
{
    /// <summary>
    /// Types of lights supported by the rendering engine.
    /// 
    /// Each light type has different behaviors and use cases
    /// </summary>
    public enum LightType
    {
        /// <summary>
        /// Directional light, affects the entire scene with parallel rays
        /// </summary>
        Directional,

        /// <summary>
        /// Spot light, emits light in a cone shape
        /// </summary>
        Spot,

        /// <summary>
        /// Omni light, emits light in all directions from a single point
        /// </summary>
        Omnidirectional
    }
}
