namespace CalculationEngine
{
    public class AntiVehicleFireRequest
    {
        public int RateOfFire { get; set; }
        public EngagemnetRange EngagementRange { get; set; }
        public WeaponType WeaponType { get; set; }
        public TroopQuality TroopQuality { get; set; }
    }
}