namespace Gigaclear_code_challenge
{
   public class Values
    {
        public int Cabinet { get; set; }
        public int Pot { get; set; }
        public int Chamber { get; set; }
        public int TrenchRoad { get; set; }
        public int TrenchVerge { get; set; }
        public int PotFromCabinet { get; set; }
        public string Filename { get; set; } = "";
        public RateCard RateCard => new RateCard() { CabinetRateCard = Cabinet, PotRateCard = Pot, ChamberRateCard = Chamber, TrenchRoadRateCard = TrenchRoad, TrenchVergeRateCard = TrenchVerge, PotFromCabinetRateCard = PotFromCabinet };
    }
}
