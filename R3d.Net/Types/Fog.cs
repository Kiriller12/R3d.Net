namespace R3d.Net.Types
{
    /// <summary>
    /// Fog effect modes.
    /// 
    /// Determines how fog is applied to the scene, affecting depth perception and atmosphere.
    /// </summary>
    public enum Fog
    {
        /// <summary>
        /// Fog effect is disabled
        /// </summary>
        Disabled,

        /// <summary>
        /// Fog density increases linearly with distance from the camera
        /// </summary>
        Linear,

        /// <summary>
        /// Exponential fog (exp2), where density increases exponentially with distanceExponential fog (exp2), where density increases exponentially with distance
        /// </summary>
        Exp2,

        /// <summary>
        /// Exponential fog, similar to EXP2 but with a different rate of increaseExponential fog, similar to EXP2 but with a different rate of increase
        /// </summary>
        Exp
    }
}
