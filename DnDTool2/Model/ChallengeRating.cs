using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class ChallengeRating
    {
        public ChallengeRatingNumber cr;
        public int exp, proficiencyBonus;

        public ChallengeRating(ChallengeRatingNumber cr, int exp, int proficiencyBonus)
        {
            this.cr = cr;
            this.exp = exp;
            this.proficiencyBonus = proficiencyBonus;
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

        public override string ToString() {
            switch (cr)
            {
                case ChallengeRatingNumber.ZERO:
                    return "0";
                case ChallengeRatingNumber.ONE_EIGHTH:
                    return "1/8";
                case ChallengeRatingNumber.ONE_FOURTH:
                    return "1/4";
                case ChallengeRatingNumber.ONE_HALF:
                    return "1/2";
                default:
                    return ((int)cr - 3).ToString();
            }
        }
    }
}
