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
    public enum SkillType
    {
        ATHLETICS, ACROBATICS, SLEIGHT_OF_HAND, STEALTH, ARCANA, HISTORY, INVESTIGATION, NATURE, RELIGION, ANIMAL_HANDLING, INSIGHT,
        MEDICINE, PERCEPTION, SURVIVAL, DECEPTION, INTIMIDATION, PERFORMANCE, PERSUASION
    }
    public enum SkillProficiencyType
    {
        NONE, PROFICIENCY, EXPERTISE
    }
    public enum DamageType
    {
        ACID, BLUDGEONING, COLD, FIRE, FORCE, LIGHTNING, NECROTIC, PIERCING, POISON, PSYCHIC, RADIANT, SLASHING, THUNDER,
        NONMAGICAL, NONMAGICAL_ADAMANTINE, NONMAGICAL_SILVER
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
                hp = CalcHP(hdCount, hd, mods[2]);
                OnPropertyChanged("HP");
                OnPropertyChanged("HPCalculation");
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
        private int naturalArmorAC;
        private bool mageArmor;
        private bool barkskin;

        public string AC
        {
            get
            {
                string s = "";
                if (naturalArmor)
                    s += ", natural armor";
                else
                {
                    if (armor != null)
                        if (!armor.Name.Equals("None"))
                            s += ", " + armor.Name.ToLower();
                }
                if (shield)
                    s += ", shield";
                if (mageArmor)
                    s += ", " + Math.Max(ac, (13 + mods[1] + (shield ? 2 : 0))) + " with mage armor";
                if (barkskin)
                    s += ", " + Math.Max(ac, 16) + " with barksin";

                if (!s.Equals(""))
                    return ac + " (" + s.Substring(2) + ")";
                else
                    return ac.ToString();
            }
        }
        public Armor Armor
        {
            get => armor;
            set
            {
                armor = value;
                OnPropertyChanged("Armor");
                ac = CalcAC(armor, mods[1], shield, naturalArmor, naturalArmorAC);
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
                ac = CalcAC(armor, mods[1], shield, naturalArmor, naturalArmorAC);
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
                ac = CalcAC(armor, mods[1], shield, naturalArmor, naturalArmorAC);
                OnPropertyChanged("AC");
            }
        }
        public int NaturalArmorAC
        {
            get => naturalArmorAC;
            set
            {
                naturalArmorAC = value;
                OnPropertyChanged("NaturalArmorAC");
                ac = CalcAC(armor, mods[1], shield, naturalArmor, naturalArmorAC);
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
        public string HPCalculation
        {
            get
            {
                return hdCount + "d" + hd.Faces + (mods[2] < 0 ? " - " : " + ") + mods[2] * hdCount;
            }
        }
        public int HDCount
        {
            get => hdCount;
            set
            {
                hdCount = value;
                OnPropertyChanged("HDCount");
                hp = CalcHP(hdCount, hd, mods[2]);
                OnPropertyChanged("HP");
                OnPropertyChanged("HPCalculation");
            }
        }
        public Die HD
        {
            get => hd;
        }

        private int speed, climb, swim, burrow, fly;
        private bool hover;

        public int Speed
        {
            get => speed;
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public int Climb
        {
            get => climb;
            set
            {
                climb = value;
                OnPropertyChanged("Climb");
            }
        }
        public int Swim
        {
            get => swim;
            set
            {
                swim = value;
                OnPropertyChanged("Swim");
            }
        }
        public int Burrow
        {
            get => burrow;
            set
            {
                burrow = value;
                OnPropertyChanged("Burrow");
            }
        }
        public int Fly
        {
            get => fly;
            set
            {
                fly = value;
                OnPropertyChanged("Fly");
            }
        }
        public bool Hover
        {
            get => hover;
            set
            {
                hover = value;
                OnPropertyChanged("Hover");
            }
        }

        private ChallengeRating cr;

        public ChallengeRating CR
        {
            get => cr;
            set
            {
                cr = value;
                OnPropertyChanged("CR");
                OnPropertyChanged("Saves");
                PropertyUpdateList(GetSkillNames().ToList());
                OnPropertyChanged("PassivePerception");
            }
        }

        private int[] scores;
        private int[] mods;
        private bool[] saves;
        private SkillProficiencyType[] skills;

        public int StrengthScore
        {
            get => scores[0];
            set
            {
                scores[0] = value;
                OnPropertyChanged("StrengthScore");
                mods[0] = ScoreToMod(value);
                OnPropertyChanged("StrengthMod");
                OnPropertyChanged("StrengthSave");
                OnPropertyChanged("Saves");
                OnPropertyChanged("Athletics");
                OnPropertyChanged("Skills");
            }
        }
        public int DexterityScore
        {
            get => scores[1];
            set
            {
                scores[1] = value;
                OnPropertyChanged("DexterityScore");
                mods[1] = ScoreToMod(value);
                OnPropertyChanged("DexterityMod");
                OnPropertyChanged("DexteritySave");
                ac = CalcAC(armor, mods[1], shield, naturalArmor, naturalArmorAC);
                OnPropertyChanged("AC");
                OnPropertyChanged("Saves");
                OnPropertyChanged("Acrobatics");
                OnPropertyChanged("SleightOfHand");
                OnPropertyChanged("Stealth");
                OnPropertyChanged("Skills");
            }
        }
        public int ConstitutionScore
        {
            get => scores[2];
            set
            {
                scores[2] = value;
                OnPropertyChanged("ConstitutionScore");
                mods[2] = ScoreToMod(value);
                OnPropertyChanged("ConstitutionMod");
                OnPropertyChanged("ConstitutionSave");
                hp = CalcHP(hdCount, hd, mods[2]);
                OnPropertyChanged("HP");
                OnPropertyChanged("HPCalculation");
                OnPropertyChanged("Saves");
                OnPropertyChanged("Skills");
            }
        }
        public int IntelligenceScore
        {
            get => scores[3];
            set
            {
                scores[3] = value;
                OnPropertyChanged("IntelligenceScore");
                mods[3] = ScoreToMod(value);
                OnPropertyChanged("IntelligenceMod");
                OnPropertyChanged("IntelligenceSave");
                OnPropertyChanged("Saves");
                OnPropertyChanged("Arcana");
                OnPropertyChanged("History");
                OnPropertyChanged("Investigation");
                OnPropertyChanged("Nature");
                OnPropertyChanged("Religion");
                OnPropertyChanged("Skills");
            }
        }
        public int WisdomScore
        {
            get => scores[4];
            set
            {
                scores[4] = value;
                OnPropertyChanged("WisdomScore");
                mods[4] = ScoreToMod(value);
                OnPropertyChanged("WisdomMod");
                OnPropertyChanged("WisdomSave");
                OnPropertyChanged("Saves");
                OnPropertyChanged("AnimalHandling");
                OnPropertyChanged("Insight");
                OnPropertyChanged("Medicine");
                OnPropertyChanged("Perception");
                OnPropertyChanged("Survival");
                OnPropertyChanged("Skills");
                OnPropertyChanged("PassivePerception");
            }
        }
        public int CharismaScore
        {
            get => scores[5];
            set
            {
                scores[5] = value;
                OnPropertyChanged("CharismaScore");
                mods[5] = ScoreToMod(value);
                OnPropertyChanged("CharismaMod");
                OnPropertyChanged("CharismaSave");
                OnPropertyChanged("Saves");
                OnPropertyChanged("Deception");
                OnPropertyChanged("Intimidation");
                OnPropertyChanged("Performance");
                OnPropertyChanged("Persuasion");
                OnPropertyChanged("Skills");
            }
        }
        public int StrengthMod
        {
            get => mods[0];
        }
        public int DexterityMod
        {
            get => mods[1];
        }
        public int ConstitutionMod
        {
            get => mods[2];
        }
        public int IntelligenceMod
        {
            get => mods[3];
        }
        public int WisdomMod
        {
            get => mods[4];
        }
        public int CharismaMod
        {
            get => mods[5];
        }
        public bool StrengthSaveProf
        {
            get => saves[0];
            set
            {
                saves[0] = value;
                OnPropertyChanged("StrengthSaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public bool DexteritySaveProf
        {
            get => saves[1];
            set
            {
                saves[1] = value;
                OnPropertyChanged("DexteritySaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public bool ConstitutionSaveProf
        {
            get => saves[2];
            set
            {
                saves[2] = value;
                OnPropertyChanged("ConstitutionSaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public bool IntelligenceSaveProf
        {
            get => saves[3];
            set
            {
                saves[3] = value;
                OnPropertyChanged("IntelligenceSaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public bool WisdomSaveProf
        {
            get => saves[4];
            set
            {
                saves[4] = value;
                OnPropertyChanged("WisdomSaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public bool CharismaSaveProf
        {
            get => saves[5];
            set
            {
                saves[5] = value;
                OnPropertyChanged("CharismaSaveProf");
                OnPropertyChanged("Saves");
            }
        }
        public string Saves
        {
            get
            {
                string s = "";
                string[] statNames = GetStatNames();
                for (int i = 0; i < saves.Length; i++)
                {
                    if (saves[i])
                        s += ", " + statNames[i] + " " + ModDisplay(mods[i] + cr.ProficiencyBonus);
                }
                return s.Equals("") ? "" : s.Substring(2);
            }
        }
        public SkillProficiencyType AthleticsProf
        {
            get => skills[0];
            set
            {
                skills[0] = value;
                OnPropertyChanged("AthleticsProf");
                OnPropertyChanged("Athletics");
                OnPropertyChanged("Skills");
            }
        }
        public int Athletics
        {
            get => mods[0] + ProfTypeToInt(skills[0], cr.ProficiencyBonus);
        }
        public SkillProficiencyType AcrobaticsProf
        {
            get => skills[1];
            set
            {
                skills[1] = value;
                OnPropertyChanged("AcrobaticsProf");
                OnPropertyChanged("Acrobatics");
                OnPropertyChanged("Skills");
            }
        }
        public int Acrobatics
        {
            get => mods[1] + ProfTypeToInt(skills[1], cr.ProficiencyBonus);
        }
        public SkillProficiencyType SleightOfHandProf
        {
            get => skills[2];
            set
            {
                skills[2] = value;
                OnPropertyChanged("SleightOfHandProf");
                OnPropertyChanged("SleightOfHand");
                OnPropertyChanged("Skills");
            }
        }
        public int SleightOfHand
        {
            get => mods[1] + ProfTypeToInt(skills[2], cr.ProficiencyBonus);
        }
        public SkillProficiencyType StealthProf
        {
            get => skills[3];
            set
            {
                skills[3] = value;
                OnPropertyChanged("StealthProf");
                OnPropertyChanged("Stealth");
                OnPropertyChanged("Skills");
            }
        }
        public int Stealth
        {
            get => mods[1] + ProfTypeToInt(skills[3], cr.ProficiencyBonus);
        }
        public SkillProficiencyType ArcanaProf
        {
            get => skills[4];
            set
            {
                skills[4] = value;
                OnPropertyChanged("ArcanaProf");
                OnPropertyChanged("Arcana");
                OnPropertyChanged("Skills");
            }
        }
        public int Arcana
        {
            get => mods[3] + ProfTypeToInt(skills[4], cr.ProficiencyBonus);
        }
        public SkillProficiencyType HistoryProf
        {
            get => skills[5];
            set
            {
                skills[5] = value;
                OnPropertyChanged("HistoryProf");
                OnPropertyChanged("History");
                OnPropertyChanged("Skills");
            }
        }
        public int History
        {
            get => mods[3] + ProfTypeToInt(skills[5], cr.ProficiencyBonus);
        }
        public SkillProficiencyType InvestigationProf
        {
            get => skills[6];
            set
            {
                skills[6] = value;
                OnPropertyChanged("InvestigationProf");
                OnPropertyChanged("Investigation");
                OnPropertyChanged("Skills");
            }
        }
        public int Investigation
        {
            get => mods[3] + ProfTypeToInt(skills[6], cr.ProficiencyBonus);
        }
        public SkillProficiencyType NatureProf
        {
            get => skills[7];
            set
            {
                skills[7] = value;
                OnPropertyChanged("NatureProf");
                OnPropertyChanged("Nature");
                OnPropertyChanged("Skills");
            }
        }
        public int Nature
        {
            get => mods[3] + ProfTypeToInt(skills[7], cr.ProficiencyBonus);
        }
        public SkillProficiencyType ReligionProf
        {
            get => skills[8];
            set
            {
                skills[8] = value;
                OnPropertyChanged("ReligionProf");
                OnPropertyChanged("Religion");
                OnPropertyChanged("Skills");
            }
        }
        public int Religion
        {
            get => mods[3] + ProfTypeToInt(skills[8], cr.ProficiencyBonus);
        }
        public SkillProficiencyType AnimalHandlingProf
        {
            get => skills[9];
            set
            {
                skills[9] = value;
                OnPropertyChanged("AnimalHandlingProf");
                OnPropertyChanged("AnimalHandling");
                OnPropertyChanged("Skills");
            }
        }
        public int AnimalHandling
        {
            get => mods[4] + ProfTypeToInt(skills[9], cr.ProficiencyBonus);
        }
        public SkillProficiencyType InsightProf
        {
            get => skills[10];
            set
            {
                skills[10] = value;
                OnPropertyChanged("InsightProf");
                OnPropertyChanged("Insight");
                OnPropertyChanged("Skills");
            }
        }
        public int Insight
        {
            get => mods[4] + ProfTypeToInt(skills[10], cr.ProficiencyBonus);
        }
        public SkillProficiencyType MedicineProf
        {
            get => skills[11];
            set
            {
                skills[11] = value;
                OnPropertyChanged("MedicineProf");
                OnPropertyChanged("Medicine");
                OnPropertyChanged("Skills");
            }
        }
        public int Medicine
        {
            get => mods[4] + ProfTypeToInt(skills[11], cr.ProficiencyBonus);
        }
        public SkillProficiencyType PerceptionProf
        {
            get => skills[12];
            set
            {
                skills[12] = value;
                OnPropertyChanged("PerceptionProf");
                OnPropertyChanged("Perception");
                OnPropertyChanged("Skills");
                OnPropertyChanged("PassivePerception");
            }
        }
        public int Perception
        {
            get => mods[4] + ProfTypeToInt(skills[12], cr.ProficiencyBonus);
        }
        public SkillProficiencyType SurvivalProf
        {
            get => skills[13];
            set
            {
                skills[13] = value;
                OnPropertyChanged("SurvivalProf");
                OnPropertyChanged("Survival");
                OnPropertyChanged("Skills");
            }
        }
        public int Survival
        {
            get => mods[4] + ProfTypeToInt(skills[13], cr.ProficiencyBonus);
        }
        public SkillProficiencyType DeceptionProf
        {
            get => skills[14];
            set
            {
                skills[14] = value;
                OnPropertyChanged("DeceptionProf");
                OnPropertyChanged("Deception");
                OnPropertyChanged("Skills");
            }
        }
        public int Deception
        {
            get => mods[5] + ProfTypeToInt(skills[14], cr.ProficiencyBonus);
        }
        public SkillProficiencyType IntimidationProf
        {
            get => skills[15];
            set
            {
                skills[15] = value;
                OnPropertyChanged("IntimidationProf");
                OnPropertyChanged("Intimidation");
                OnPropertyChanged("Skills");
            }
        }
        public int Intimidation
        {
            get => mods[5] + ProfTypeToInt(skills[15], cr.ProficiencyBonus);
        }
        public SkillProficiencyType PerformanceProf
        {
            get => skills[16];
            set
            {
                skills[16] = value;
                OnPropertyChanged("PerformanceProf");
                OnPropertyChanged("Performance");
                OnPropertyChanged("Skills");
            }
        }
        public int Performance
        {
            get => mods[5] + ProfTypeToInt(skills[16], cr.ProficiencyBonus);
        }
        public SkillProficiencyType PersuasionProf
        {
            get => skills[17];
            set
            {
                skills[17] = value;
                OnPropertyChanged("PersuasionProf");
                OnPropertyChanged("Persuasion");
                OnPropertyChanged("Skills");
            }
        }
        public int Persuasion
        {
            get => mods[5] + ProfTypeToInt(skills[17], cr.ProficiencyBonus);
        }
        public string Skills
        {
            get
            {
                string s = "";
                string[] skillNames = GetSkillNames();
                for (int i = 0; i < skills.Length; i++)
                {
                    if (skills[i] != SkillProficiencyType.NONE)
                        s += ", " + skillNames[i] + " " + ModDisplay(GetSkill(skillNames[i]));
                }
                return s.Equals("") ? "" : s.Substring(2);
            }
        }

        private DamageEffect[] damageEffects;

        public DamageEffect Acid
        {
            get => damageEffects[0];
            set
            {
                damageEffects[0] = value;
                OnPropertyChanged("Acid");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Bludgeoning
        {
            get => damageEffects[1];
            set
            {
                damageEffects[1] = value;
                OnPropertyChanged("Bludgeoning");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Cold
        {
            get => damageEffects[2];
            set
            {
                damageEffects[2] = value;
                OnPropertyChanged("Cold");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Fire
        {
            get => damageEffects[3];
            set
            {
                damageEffects[3] = value;
                OnPropertyChanged("Fire");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Force
        {
            get => damageEffects[4];
            set
            {
                damageEffects[4] = value;
                OnPropertyChanged("Force");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Lightning
        {
            get => damageEffects[5];
            set
            {
                damageEffects[5] = value;
                OnPropertyChanged("Lightning");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Necrotic
        {
            get => damageEffects[6];
            set
            {
                damageEffects[6] = value;
                OnPropertyChanged("Necrotic");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Piercing
        {
            get => damageEffects[7];
            set
            {
                damageEffects[7] = value;
                OnPropertyChanged("Piercing");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Poison
        {
            get => damageEffects[8];
            set
            {
                damageEffects[8] = value;
                OnPropertyChanged("Poison");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Psychic
        {
            get => damageEffects[9];
            set
            {
                damageEffects[9] = value;
                OnPropertyChanged("Psychic");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Radiant
        {
            get => damageEffects[10];
            set
            {
                damageEffects[10] = value;
                OnPropertyChanged("Radiant");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Slashing
        {
            get => damageEffects[11];
            set
            {
                damageEffects[11] = value;
                OnPropertyChanged("Slashing");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Thunder
        {
            get => damageEffects[12];
            set
            {
                damageEffects[12] = value;
                OnPropertyChanged("Thunder");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Nonmagical
        {
            get => damageEffects[13];
            set
            {
                damageEffects[13] = value;
                OnPropertyChanged("Nonmagical");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Nonmagical_Adamantine
        {
            get => damageEffects[14];
            set
            {
                damageEffects[14] = value;
                OnPropertyChanged("Nonmagical_Adamantine");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public DamageEffect Nonmagical_Silver
        {
            get => damageEffects[15];
            set
            {
                damageEffects[15] = value;
                OnPropertyChanged("Nonmagical_Silver");
                OnPropertyChanged("Vulnerabilities");
                OnPropertyChanged("Resistances");
                OnPropertyChanged("Immunities");
            }
        }
        public string Vulnerabilities
        {
            get
            {
                string s = "";
                string[] damageNames = GetDamageNames();
                for (int i = 0; i < damageNames.Length; i++)
                {
                    if (damageEffects[i] == DamageEffect.VULNERABILITY)
                        s += (i <= 12 ? ", " : "; ") + damageNames[i];
                }
                return (s == "" ? "" : s.Substring(2));
            }
        }
        public string Resistances
        {
            get
            {
                string s = "";
                string[] damageNames = GetDamageNames();
                for (int i = 0; i < damageNames.Length; i++)
                {
                    if (damageEffects[i] == DamageEffect.RESISTANCE)
                        s += (i <= 12 ? ", " : "; ") + damageNames[i];
                }
                return (s == "" ? "" : s.Substring(2));
            }
        }
        public string Immunities
        {
            get
            {
                string s = "";
                string[] damageNames = GetDamageNames();
                for (int i = 0; i < damageNames.Length; i++)
                {
                    if (damageEffects[i] == DamageEffect.IMMUNITY)
                        s += (i <= 12 ? ", " : "; ") + damageNames[i];
                }
                return (s == "" ? "" : s.Substring(2));
            }
        }

        private bool[] conditionImmunities;
        public bool Blinded
        {
            get => conditionImmunities[0];
            set
            {
                conditionImmunities[0] = value;
                OnPropertyChanged("Blinded");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Charmed
        {
            get => conditionImmunities[1];
            set
            {
                conditionImmunities[1] = value;
                OnPropertyChanged("Charmed");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Deafened
        {
            get => conditionImmunities[2];
            set
            {
                conditionImmunities[2] = value;
                OnPropertyChanged("Deafened");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Exhaustion
        {
            get => conditionImmunities[2];
            set
            {
                conditionImmunities[2] = value;
                OnPropertyChanged("Frightened");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Grappled
        {
            get => conditionImmunities[3];
            set
            {
                conditionImmunities[3] = value;
                OnPropertyChanged("Grappled");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Incapacitated
        {
            get => conditionImmunities[4];
            set
            {
                conditionImmunities[4] = value;
                OnPropertyChanged("Incapacitated");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Invisible
        {
            get => conditionImmunities[5];
            set
            {
                conditionImmunities[5] = value;
                OnPropertyChanged("Invisible");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Paralyzed
        {
            get => conditionImmunities[6];
            set
            {
                conditionImmunities[6] = value;
                OnPropertyChanged("Paralyzed");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Petrified
        {
            get => conditionImmunities[7];
            set
            {
                conditionImmunities[7] = value;
                OnPropertyChanged("Petrified");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Poisoned
        {
            get => conditionImmunities[8];
            set
            {
                conditionImmunities[8] = value;
                OnPropertyChanged("Poisoned");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Prone
        {
            get => conditionImmunities[9];
            set
            {
                conditionImmunities[9] = value;
                OnPropertyChanged("Prone");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Restrained
        {
            get => conditionImmunities[10];
            set
            {
                conditionImmunities[10] = value;
                OnPropertyChanged("Restrained");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Stunned
        {
            get => conditionImmunities[11];
            set
            {
                conditionImmunities[11] = value;
                OnPropertyChanged("Stunned");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public bool Unconscious
        {
            get => conditionImmunities[12];
            set
            {
                conditionImmunities[12] = value;
                OnPropertyChanged("Unconscious");
                OnPropertyChanged("ConditionImmunities");
            }
        }
        public string ConditionImmunities
        {
            get
            {
                string s = "";
                string[] conditionNames = GetConditionNames();
                for (int i = 0; i < conditionNames.Length; i++)
                {
                    if (conditionImmunities[i])
                        s += ", " + conditionNames[i];
                }
                return (s == "" ? s : s.Substring(2));
            }
        }

        private int darkvision, blindsight, tremorsense, truesight;
        private bool blindOutside, perceptionAdvantage;

        public int Darkvision
        {
            get => darkvision;
            set
            {
                darkvision = value;
                OnPropertyChanged("Darkvision");
            }
        }
        public int Blindsight
        {
            get => blindsight;
            set
            {
                blindsight = value;
                OnPropertyChanged("Blindsight");
            }
        }
        public int Tremorsense
        {
            get => tremorsense; 
            set
            {
                tremorsense = value;
                OnPropertyChanged("Tremorsense");
            }
        }
        public int Truesight
        {
            get => truesight;
            set
            {
                truesight = value;
                OnPropertyChanged("Truesight");
            }
        }
        public bool BlindOutside
        {
            get => blindOutside;
            set
            {
                blindOutside = value;
                OnPropertyChanged("BlindOutside");
            }
        }
        public bool PerceptionAdvantage
        {
            get => perceptionAdvantage;
            set
            {
                perceptionAdvantage = value;
                OnPropertyChanged("PerceptionAdvantage");
                OnPropertyChanged("PassivePerception");
            }
        }
        public int PassivePerception
        {
            get => 10 + GetSkill("Perception") + (perceptionAdvantage ? 5 : 0);
        }


        private ObservableCollection<string> languages;
        public ObservableCollection<string> Languages
        {
            get => languages;
            set
            {
                languages = value;
                OnPropertyChanged("Languages");
            }
        }
        public string LanguageString
        {
            get
            {
                string ret = "";
                foreach (string s in languages) {
                    ret += ", " + s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
                }
                return (ret == "" ? ret : ret.Substring(2));
            }
        }

        private int otherLanguages;

        public Creature(string name, CreatureSize size, CreatureType type, Alignment alignment,
            int ac, Armor armor, bool shield, bool naturalArmor, int naturalArmorAC, bool mageArmor, bool barkskin,
            int hp, int hdCount, Die hd,
            int speed, int swim, int climb, int burrow, int fly, bool hover,
            ChallengeRating cr,
            int[] scores, int[] mods, bool[] saves, SkillProficiencyType[] skills,
            DamageEffect[] damageEffects, bool[] conditionImmunities,
            int darkvision, int blindsight, int tremorsense, int truesight, bool blindOutside, bool perceptionAdvantage,
            ObservableCollection<string> languages, int otherLanguages)
        {
            this.name = name;
            this.size = size;
            this.type = type;
            this.alignment = alignment;

            this.ac = ac;
            this.armor = armor;
            this.shield = shield;
            this.naturalArmor = naturalArmor;
            this.naturalArmorAC = naturalArmorAC;
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
            this.perceptionAdvantage = perceptionAdvantage;

            this.languages = languages;
            this.otherLanguages = otherLanguages;
        }

        public Creature(string name, CreatureSize size, CreatureType type, Alignment alignment,
            int ac, Armor armor, bool shield, bool naturalArmor, int naturalArmorAC, bool mageArmor, bool barkskin,
            int hp, int hdCount, Die hd,
            int speed, int swim, int climb, int burrow, int fly, bool hover,
            ChallengeRating cr,
            int[] scores, bool[] saves, SkillProficiencyType[] skills,
            DamageEffect[] damageEffects, bool[] conditionImmunities,
            int darkvision, int blindsight, int tremorsense, int truesight, bool blindOutside, bool perceptionAdvantage,
            ObservableCollection<string> languages, int otherLanguages) :
            this(name, size, type, alignment,
                ac, armor, shield, naturalArmor, naturalArmorAC, mageArmor, barkskin,
                hp, hdCount, hd,
                speed, swim, climb, burrow, fly, hover,
                cr,
                scores, ScoresToMods(scores), saves, skills,
                damageEffects, conditionImmunities,
                darkvision, blindsight, tremorsense, truesight, blindOutside, perceptionAdvantage,
                languages, otherLanguages)
        { }

        public Creature(string name, int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore)
        {
            this.name = name;
            this.size = CreatureSize.MEDIUM;
            this.type = CreatureType.HUMANOID;
            this.alignment = Alignment.U;

            this.ac = 10;
            this.armor = new Armor("None", ArmorType.NONE, 10);
            this.shield = false;
            this.naturalArmor = false;
            this.naturalArmorAC = 10;
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

            this.cr = new ChallengeRating(ChallengeRatingNumber.ZERO, 0, 2);

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
                DamageEffect.NONE, // Bludgeoning, piercing, and slashing from nonmagical attacks not made with silvered weapons
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
            this.perceptionAdvantage = false;

            this.languages = new ObservableCollection<string>();
            this.otherLanguages = 0;

            OnPropertyChanged("Armor");
        }

        public Creature() : this("", 10, 10, 10, 10, 10, 10) { }

        public void AddLanguage(string lang)
        {
            languages.Add(lang);
            OnPropertyChanged("LanguageString");
            OnPropertyChanged("Languages");
        }

        public void RemoveLanguage(string lang)
        {
            languages.Remove(lang);
            OnPropertyChanged("LanguageString");
            OnPropertyChanged("Languages");
        }

        public void SortLanguages()
        {
            languages = new ObservableCollection<string>(languages.OrderBy(lang => lang));
            OnPropertyChanged("Languages");
            OnPropertyChanged("LanguageString");
        }

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

        public static int CalcHP(int hdCount, Die hd, int conMod)
        {
            return (int)Math.Floor(hdCount * (hd.Average + conMod));
        }

        public static int CalcAC(Armor armor, int dexMod, bool shield, bool naturalArmor, int naturalArmorAC)
        {
            int ac;
            if (naturalArmor)
                ac = naturalArmorAC + dexMod;
            else
            {
                if (armor == null)
                    ac = 10;
                else
                {
                    switch (armor.Type)
                    {
                        case ArmorType.NONE:
                            ac = 10 + dexMod;
                            break;
                        case ArmorType.LIGHT:
                            ac = armor.BaseAC + dexMod;
                            break;
                        case ArmorType.MEDIUM:
                            ac = armor.BaseAC + Math.Min(dexMod, 2);
                            break;
                        case ArmorType.HEAVY:
                            ac = armor.BaseAC;
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
            return ac + (shield ? 2 : 0);
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

        private static int ProfTypeToInt(SkillProficiencyType type, int proficiencyBonus)
        {
            switch (type)
            {
                case SkillProficiencyType.EXPERTISE:
                    return proficiencyBonus * 2;
                case SkillProficiencyType.PROFICIENCY:
                    return proficiencyBonus;
                case SkillProficiencyType.NONE:
                    return 0;
                default:
                    throw new NotSupportedException();
            }
        }

        private static string ModDisplay(int mod)
        {
            return (mod < 0 ? "" : "+") + mod;
        }

        private int GetSkill(string skill)
        {
            switch (skill)
            {
                case "Athletics":
                    return Athletics;
                case "Acrobatics":
                    return Acrobatics;
                case "Sleight of Hand":
                    return SleightOfHand;
                case "Stealth":
                    return Stealth;
                case "Arcana":
                    return Arcana;
                case "History":
                    return History;
                case "Investigation":
                    return Investigation;
                case "Nature":
                    return Nature;
                case "Religion":
                    return Religion;
                case "Animal Handling":
                    return AnimalHandling;
                case "Insight":
                    return Insight;
                case "Medicine":
                    return Medicine;
                case "Perception":
                    return Perception;
                case "Survival":
                    return Survival;
                case "Deception":
                    return Deception;
                case "Intimidation":
                    return Intimidation;
                case "Performance":
                    return Performance;
                case "Persuasion":
                    return Persuasion;
                default:
                    throw new NotSupportedException();
            }
        }

        public static string[] GetStatNames()
        {
            return new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
        }

        public static string[] GetSkillNames()
        {
            return new string[]
            {
                "Athletics",
                "Acrobatics",
                "Sleight of Hand",
                "Stealth",
                "Arcana",
                "History",
                "Investigation",
                "Nature",
                "Religion",
                "Animal Handling",
                "Insight", "Medicine",
                "Perception",
                "Survival",
                "Deception",
                "Intimidation",
                "Performance",
                "Persuasion"
            };
        }

        public static string[] GetDamageNames()
        {
            return new string[]
            {
                "Acid",
                "Bludgeoning",
                "Cold",
                "Fire",
                "Force",
                "Lightning",
                "Necrotic",
                "Piercing",
                "Poison",
                "Psychic",
                "Radiant",
                "Slashing",
                "Thunder",
                "Bludgeoning, piercing, and slashing from nonmagical attacks",
                "Bludgeoning, piercing, and slashing from nonmagical attacks not made with adamantine weapons",
                "Bludgeoning, piercing, and slashing from nonmagical attacks not made with silvered weapons"
            };
        }

        public static string[] GetConditionNames()
        {
            return new string[]
            {
                "Blinded",
                "Charmed",
                "Deafened",
                "Exhaustion",
                "Frightened",
                "Grappled",
                "Incapacitated",
                "Invisible",
                "Paralyzed",
                "Petrified",
                "Poisoned",
                "Prone",
                "Restrained",
                "Stunned",
                "Unconscious"
            };
        }
    }
}
