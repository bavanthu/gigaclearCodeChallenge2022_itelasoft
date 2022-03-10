namespace Gigaclear_code_challenge
{
    public class RateCard
    {
        public int Cabinet { get; set; }
        public int Pot { get; set; }
        public int Chamber { get; set; }
        public int TrenchRoad { get; set; }
        public int TrenchVerge { get; set; }
        public int PotFromCabinet { get; set; }
        public bool IsNonZero => Cabinet != 0 || Pot != 0 || Chamber != 0 || TrenchVerge != 0 || TrenchRoad != 0 || PotFromCabinet != 0;

        public override string ToString()
        {
            return $"Cabinet=£{Cabinet}; Pot=£{Pot}; Chamber=£{Chamber}; Trench road /m=£{TrenchRoad}; Trench verge /m=£{TrenchVerge}; Pot cost /m from cabinet=£{PotFromCabinet}* trench length from Cabinet";
        }
    }
}
