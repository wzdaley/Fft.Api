namespace CalculationEngine
{
    public sealed class TroopQuality
    {
        public static Quality Poor = new Quality
        {
            QualityNumber = 7,
            CohesionDistance = 2,
            ToHitModifier = -3,
            GunRateOfFireModifier = -2
        };
        public static Quality Marginal = new Quality
        {
            QualityNumber = 7,
            CohesionDistance = 2,
            ToHitModifier = -2,
            GunRateOfFireModifier = -1
        };
        public static Quality Fair = new Quality
        {
            QualityNumber = 6,
            CohesionDistance = 4,
            ToHitModifier = -1,
            GunRateOfFireModifier = 0
        };
        public static Quality Average = new Quality
        {
            QualityNumber = 5,
            CohesionDistance = 4,
            ToHitModifier = 0,
            GunRateOfFireModifier = 0
        };
        public static Quality Good = new Quality
        {
            QualityNumber = 4,
            CohesionDistance = 6,
            ToHitModifier = 1,
            GunRateOfFireModifier = 0
        };
        public static Quality Excelent = new Quality
        {
            QualityNumber = 3,
            CohesionDistance = 6,
            ToHitModifier = 1,
            GunRateOfFireModifier = 1
        };
        public static Quality Superb = new Quality
        {
            QualityNumber = 2,
            CohesionDistance = 8,
            ToHitModifier = 1,
            GunRateOfFireModifier = 1
        };
    }
}