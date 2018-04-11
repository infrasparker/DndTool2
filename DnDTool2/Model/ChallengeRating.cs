using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public enum ChallengeRatingNumber
    {
        ZERO, ONE_EIGHTH, ONE_FOURTH, ONE_HALF, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, ELEVEN,
        TWELVE, THIRTEEN, FOURTEEN, FIFTEEN, SIXTEEN, SEVENTEEN, EIGHTEEN, NINETEEN, TWENTY, TWENTY_ONE, TWENTY_TWO,
        TWENTY_THREE, TWENTY_FOUR, TWENTY_FIVE, TWENTY_SIX, TWENTY_SEVEN, TWENTY_EIGHT, TWENTY_NINE, THIRTY
    }

    public class ChallengeRating
    {
        private ChallengeRatingNumber cr;
        private int exp, profBonus;

        public ChallengeRatingNumber CR { get => cr; set => cr = value; }
        public int Exp { get => exp; set => exp = value; }
        public int ProfBonus { get => profBonus; set => profBonus = value; }

        public ChallengeRating(ChallengeRatingNumber cr, int exp, int profBonus)
        {
            this.CR = cr;
            this.Exp = exp;
            this.ProfBonus = profBonus;
        }

        public static List<ChallengeRating> GetStandardCRs()
        {
            List<ChallengeRating> ret = new List<ChallengeRating>();
            List<ChallengeRatingNumber> CRs = Enum.GetValues(typeof(ChallengeRatingNumber)).Cast<ChallengeRatingNumber>().ToList();
            CRs.Insert(1, ChallengeRatingNumber.ZERO);
            List<int> exps = new List<int> {0, 10, 25, 50, 100, 200, 450, 700, 1100, 1800, 2300, 2900, 3900, 5000, 5900,
                                    7200, 8400, 10000, 11500, 13000, 15000, 18000, 20000, 22000, 25000, 33000,
                                    41000, 50000, 62000, 75000, 90000, 150000, 120000, 135000, 155000};
            List<int> profs = new List<int> {2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6,
                                    7, 7, 7, 7, 8, 8, 8, 8, 9, 9};
            for (int i = 0; i < CRs.Count; i++)
            {
                ret.Add(new ChallengeRating(CRs[i], exps[i], profs[i]));
            }
            return ret;
        }
    }
}
