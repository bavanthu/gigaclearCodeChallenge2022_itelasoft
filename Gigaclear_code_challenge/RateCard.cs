namespace Gigaclear_code_challenge
{
    public class RateCard
    {
        public int CabinetRateCard { get; set; }
        public int PotRateCard { get; set; }
        public int ChamberRateCard { get; set; }
        public int TrenchRoadRateCard { get; set; }
        public int TrenchVergeRateCard { get; set; }
        public int PotFromCabinetRateCard { get; set; }
        public bool IsNonZero => CabinetRateCard != 0 || PotRateCard != 0 || ChamberRateCard != 0 || TrenchVergeRateCard != 0 || TrenchRoadRateCard != 0 || PotFromCabinetRateCard != 0;

        public override string ToString()
        {
            return $"Cabinet=£{CabinetRateCard}; Pot=£{PotRateCard}; Chamber=£{ChamberRateCard}; Trench road /m=£{TrenchRoadRateCard}; Trench verge /m=£{TrenchVergeRateCard}; Pot cost /m from cabinet=£{PotFromCabinetRateCard}* trench length from Cabinet";
        }
    }
}
