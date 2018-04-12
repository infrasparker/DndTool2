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

        private int ac;
        private Armor armor;
        private bool shield;
        private bool naturalArmor;
        private bool mageArmor;
        private bool barkskin;

        private int hp;
        private int hdCount;
        private Die hd;

        private int speed, climb, swim, burrow, fly;
        private bool hover;

        private ChallengeRatingNumber cr;
        private int exp;
        private int proficiencyBonus;

        private int[] scores = new int[6];
        private int[] mods = new int[6];
        private bool[] saves = new bool[] { false, false, false, false, false, false };
        private SkillProficiencyType[] skills = new SkillProficiencyType[]
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

        private DamageEffect[] damageEffects = new DamageEffect[]
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

        private bool[] conditionImmunities = new bool[]
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

        private static ScoreToMod(int score)
        {
            return (int)Math.Floor
        }
    }
}
