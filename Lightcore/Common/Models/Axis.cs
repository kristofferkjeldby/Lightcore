namespace Lightcore.Common.Models
{
    public enum  Axis
    {
        X = 0,
        Y = 1,
        Z = 2,
        Unknown = 3,
        R = 0,
        /// <summary>
        /// The polar distance (0 to PI)
        /// </summary>
        Theta = 1,
        /// <summary>
        /// The azimuthal angle (0 to 2 * PI)  
        /// </summary>
        Phi = 2
    }
}
