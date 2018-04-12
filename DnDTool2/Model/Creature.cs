using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public enum CreatureSize
    {
        TINY, SMALL, MEDIUM, LARGE, HUGE, GARGANTUAN
    }
    public enum CreatureType
    {
        ABERRATION, BEAST, CELESTIAL, CONSTRUCT, DRAGON, ELEMENTAL, FEY, FIEND, GIANT, HUMANOID, MONSTROSITY, OOZE, PLANT, UNDEAD
    }
    public enum Alignment
    {
        LG, LN, LE, NG, TN, NE, CG, CN, CE, ANYG, ANYE, ANY, U
    }
    public enum ChallengeRatingNumber
    {
        ZERO, ONE_EIGHTH, ONE_FOURTH, ONE_HALF, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, ELEVEN,
        TWELVE, THIRTEEN, FOURTEEN, FIFTEEN, SIXTEEN, SEVENTEEN, EIGHTEEN, NINETEEN, TWENTY, TWENTY_ONE, TWENTY_TWO,
        TWENTY_THREE, TWENTY_FOUR, TWENTY_FIVE, TWENTY_SIX, TWENTY_SEVEN, TWENTY_EIGHT, TWENTY_NINE, THIRTY
    }
    public enum SkillProficiencyType
    {
        NONE, PROFICIENCY, EXPERTISE
    }
    public enum DamageType
    {
        ACID, BLUDGEONING, COLD, FIRE, FORCE, LIGHTNING, NECROTIC, PIERCING, POISON, PSYCHIC, RADIANT, SLASHING, THUNDER,
        NONMAGICAL, NONMAGICAL_ADAMANTINE
    }
    public enum DamageEffect
    {
        NONE, VULNERABILITY, RESISTANCE, IMMUNITY
    }
    public enum ConditionType
    {
        BLINDED, CHARMED, DEAFENED, EXHAUSTION, FRIGHTENED, GRAPPLED, INCAPACITATED, INVISIBLE, PARALYZED, PETRIFIED, POISONED,
        PRONE, RESTRAINED, STUNNED, UNCONSCIOUS
    }
    public enum SenseType
    {
        BLINDSIGHT, DARKVISION, TREMORSENSE, TRUESIGHT
    }

    public class Creature : ObservableClass
    {
        private string name;
        private CreatureSize size;
        private CreatureType type;
        private Alignment alignment;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public CreatureSize Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged("Size");
                hd = CreatureSizeToHitDie(size);
                OnPropertyChanged("HD");
                hp = 
                OnPropertyChanged("HP");
            }
        }
        public CreatureType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public Alignment Alignment
        {
            get => alignment;
            set
            {
                alignment = value;
                OnPropertyChanged("Alignment");
            }
        }

        private int ac;
        private Armor armor;
        private bool shield;
        private bool naturalArmor;
        private bool mageArmor;
        private bool barkskin;

        public int AC
        {
            get => ac;
        }
        public Armor Armor
        {
            get => armor;
            set
            {
                armor = value;
                OnPropertyChanged("Armor");
                OnPropertyChanged("AC");
            }
        }
        public bool Shield
        {
            get => shield;
            set
            {
                shield = value;
                OnPropertyChanged("Shield");
                OnPropertyChanged("AC");
            }
        }
        public bool NaturalArmor
        {
            get => naturalArmor;
            set
            {
                naturalArmor = value;
                OnPropertyChanged("NaturalArmor");
                OnPropertyChanged("AC");
            }
        }
        public bool MageArmor
        {
            get => mageArmor;
            set
            {
                mageArmor = value;
                OnPropertyChanged("MageArmor");
                OnPropertyChanged("AC");
            }
        }
        public bool Barkskin
        {
            get => barkskin;
            set
            {
                barkskin = value;
                OnPropertyChanged("Barkskin");
                OnPropertyChanged("AC");
            }
        }

        private int hp;
        private int hdCount;
        private Die hd;

        public int HP
        {
            get => hp;
        }
        public int HDCount
        {
            get => hdCount;
            set
            {
                hdCount = value;
                OnPropertyChanged("HDCount");
                OnPropertyChanged("HP");
            }
        }
        public Die HD
        {
            get => hd;
        }

        private int speed, climb, swim, burrow, fly;
        private bool hover;

        private ChallengeRatingNumber cr;
        private int exp;
        private int proficiencyBonus;

        private int[] scores;
        private int[] mods;
        private bool[] saves;
        private SkillProficiencyType[] skills;

        private DamageEffect[] damageEffects;

        private bool[] conditionImmunities;

        private int darkvision, blindsight, tremorsense, truesight;
        private bool blindOutside;

        private List<string> languages;
        private int otherLanguages;

        public Creature(string name, CreatureSize size, CreatureType type, Alignment alignment,
            int ac, Armor armor, bool shield, bool naturalArmor, bool mageArmor, bool barkskin,
            int hp, int hdCount, Die hd,
            int speed, int swim, int climb, int burrow, int fly, bool hover,
            ChallengeRatingNumber cr, int exp, int proficiencyBonus,
            int[] scores, int[] mods, bool[] saves, SkillProficiencyType[] skills,
            DamageEffect[] damageEffects, bool[] conditionImmunities,
            int darkvision, int blindsight, int tremorsense, int truesight, bool blindOutside,
            List<string> languages, int otherLanguages)
        {
            this.name = name;
            this.size = size;
            this.type = type;
            this.alignment = alignment;

            this.ac = ac;
            this.armor = armor;
            this.shield = shield;
            this.naturalArmor = naturalArmor;
            this.mageArmor = mageArmor;
            this.barkskin = barkskin;

            this.hp = hp;
            this.hdCount = hdCount;
            this.hd = hd;

            this.speed = speed;
            this.swim = swim;
            this.climb = climb;
            this.burrow = burrow;
            this.fly = fly;
            this.hover = hover;

            this.cr = cr;
            this.exp = exp;
            this.proficiencyBonus = proficiencyBonus;

            this.scores = scores;
            this.mods = mods;
            this.saves = saves;
            this.skills = skills;

            this.damageEffects = damageEffects;
            this.conditionImmunities = conditionImmunities;

            this.darkvision = darkvision;
            this.blindsight = blindsight;
            this.tremorsense = tremorsense;
            this.truesight = truesight;
            this.blindOutside = blindOutside;

            this.languages = languages;
            this.otherLanguages = otherLanguages;
        }

        public Creature(string name, CreatureSize size, CreatureType type, Alignment alignment,
            int ac, Armor armor, bool shield, bool naturalArmor, bool mageArmor, bool barkskin,
            int hp, int hdCount, Die hd,
            int speed, int swim, int climb, int burrow, int fly, bool hover,
            ChallengeRatingNumber cr, int exp, int proficiencyBonus,
            int[] scores, bool[] saves, SkillProficiencyType[] skills,
            DamageEffect[] damageEffects, bool[] conditionImmunities,
            int darkvision, int blindsight, int tremorsense, int truesight, bool blindOutside,
            List<string> languages, int otherLanguages) :
            this(name, size, type, alignment,
                ac, armor, shield, naturalArmor, mageArmor, barkskin,
                hp, hdCount, hd,
                speed, swim, climb, burrow, fly, hover,
                cr, exp, proficiencyBonus, scores, ScoresToMods(scores), saves, skills,
                damageEffects, conditionImmunities,
                darkvision, blindsight, tremorsense, truesight, blindOutside,
                languages, otherLanguages)
        { }

        public Creature(string name, int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore)
        {
            this.name = name;
            this.size = CreatureSize.MEDIUM;
            this.type = CreatureType.HUMANOID;
            this.alignment = Alignment.U;

            this.ac = 10;
            this.armor = null;
            this.shield = false;
            this.naturalArmor = false;
            this.mageArmor = false;
            this.barkskin = false;

            this.hp = 3;
            this.hdCount = 1;
            this.hd = new Die(6);

            this.speed = 30;
            this.swim = 0;
            this.climb = 0;
            this.burrow = 0;
            this.fly = 0;
            this.hover = false;

            this.cr = ChallengeRatingNumber.ZERO;
            this.exp = 0;
            this.proficiencyBonus = 2;

            this.scores = new int[] { strScore, dexScore, conScore, intScore, wisScore, chaScore };
            this.mods = ScoresToMods(this.scores);
            this.saves = new bool[] { false, false, false, false, false, false };
            this.skills = new SkillProficiencyType[]
            {
                SkillProficiencyType.NONE, // Athletics
                SkillProficiencyType.NONE, // Acrobatics
                SkillProficiencyType.NONE, // Sleight of Hand
                SkillProficiencyType.NONE, // Stealth
                SkillProficiencyType.NONE, // Arcana
                SkillProficiencyType.NONE, // History
                SkillProficiencyType.NONE, // Investigation
                SkillProficiencyType.NONE, // Nature
                SkillProficiencyType.NONE, // Religion
                SkillProficiencyType.NONE, // Animal Handling
                SkillProficiencyType.NONE, // Insight
                SkillProficiencyType.NONE, // Medicine
                SkillProficiencyType.NONE, // Perception
                SkillProficiencyType.NONE, // Survival
                SkillProficiencyType.NONE, // Deception
                SkillProficiencyType.NONE, // Intimidation
                SkillProficiencyType.NONE, // Performance
                SkillProficiencyType.NONE, // Persuasion
            };

            this.damageEffects = new DamageEffect[]
            {
                DamageEffect.NONE, // Acid
                DamageEffect.NONE, // Bludgeoning
                DamageEffect.NONE, // Cold
                DamageEffect.NONE, // Fire
                DamageEffect.NONE, // Force
                DamageEffect.NONE, // Lightning
                DamageEffect.NONE, // Necrotic
                DamageEffect.NONE, // Piercing
                DamageEffect.NONE, // Poison
                DamageEffect.NONE, // Psychic
                DamageEffect.NONE, // Radiant
                DamageEffect.NONE, // Slashing
                DamageEffect.NONE, // Thunder
                DamageEffect.NONE, // Bludgeoning, piercing, and slashing from nonmagical attacks
                DamageEffect.NONE, // Bludgeoning, piercing, and slashing from nonmagical attacks not made with adamantine weapons
            };
            this.conditionImmunities = new bool[]
            {
                false, // Blinded
                false, // Charmed
                false, // Deafened
                false, // Exhaustion
                false, // Frightened
                false, // Grappled
                false, // Incapacitated
                false, // Invisible
                false, // Paralyzed
                false, // Petrified
                false, // Poisoned
                false, // Prone
                false, // Restrained
                false, // Stunned
                false, // Unconscious
            };

            this.darkvision = 0;
            this.blindsight = 0;
            this.tremorsense = 0;
            this.truesight = 0;
            this.blindOutside = false;

            this.languages = new List<string>();
            this.otherLanguages = 0;
        }

        public Creature() : this("", 10, 10, 10, 10, 10, 10) { }

        private static Die CreatureSizeToHitDie(CreatureSize size)
        {
            switch (size)
            {
                case CreatureSize.TINY:
                    return new Die(4);
                case CreatureSize.SMALL:
                    return new Die(6);
                case CreatureSize.MEDIUM:
                    return new Die(8);
                case CreatureSize.LARGE:
                    return new Die(10);
                case CreatureSize.HUGE:
                    return new Die(12);
                case CreatureSize.GARGANTUAN:
                    return new Die(20);
                default:
                    throw new NotSupportedException();
            }
        }

        private static int CalcHP(int hdCount, Die hd, int conMod)
        {

        }

        private static int ScoreToMod(int score)
        {
            return (int)Math.Floor(((double)score - 10) / 2);
        }

        private static int[] ScoresToMods(int[] scores)
        {
            int[] ret = new int[6];
            for (int i = 0; i < scores.Length; i++)
            {
                ret[i] = ScoreToMod(scores[i]);
            }
            return ret;
        }
    }
}
