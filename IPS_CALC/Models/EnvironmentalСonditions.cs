namespace IPS_CALC.Models
{
    /// <summary>
    /// Условия лаборотории
    /// </summary>
    public struct EnvironmentalСonditions
    {
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Влажность
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// Атмосферное давление
        /// </summary>
        public double Baro { get; set; }
    }
}