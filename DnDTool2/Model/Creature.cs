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
    public enum SkillProficiencyType
    {
        NONE, PROFICIENCY, EXPERTISE
    }
    public enum SkillType
    {
        ATHLETICS, ACROBATICS, SLEIGHT_OF_HAND, STEALTH, ARCANA, HISTORY, INVESTIGATION, NATURE, RELIGION, ANIMAL_HANDLING, INSIGHT,
        MEDICINE, PERCEPTION, SURVIVAL, DECEPTION, INTIMIDATION, PERFORMANCE, PERSUASION
    }
    public enum DamageEffect
    {
        NONE, VULNERABILITY, RESISTANCE, IMMUNITY
    }
    public enum DamageType
    {
        ACID, BLUDGEONING, COLD, FIRE, FORCE, LIGHTNING, NECROTIC, PIERCING, POISON, PSYCHIC, RADIANT, SLASHING, THUNDER, NONMAGICAL
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
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }

        private CreatureType type;
        public CreatureType Type { get => type; set { type = value; OnPropertyChanged("Type"); } }

        private Alignment alignment;
        public Alignment Alignment { get => alignment; set { alignment = value; OnPropertyChanged("Type"); } }

        private CreatureSize size;
        public CreatureSize Size { get => size; set { size = value; OnPropertyChanged("Size"); this.HD = SizeToHitDie(this.Size); } }

        private int amthd;
        public int AmtHD { get => amthd; set { amthd = value; OnPropertyChanged("AmtHD"); CalcHP(); } }

        private Die hd;
        public Die HD { get => hd; set { hd = value; OnPropertyChanged("HD"); CalcHP(); } }

        private int hp;
        public int HP { get => hp; set { hp = value; OnPropertyChanged("HP"); } }

        private Armor armorSource;
        public Armor ArmorSource { get => armorSource; set { armorSource = value; OnPropertyChanged("ArmorSource"); CalcAC(); } }

        private string natArmorSource;
        public string NatArmorSource { get => natArmorSource; set { natArmorSource = value; OnPropertyChanged("NatArmorSource"); } }

        private int natArmorAC;
        public int NatArmorAC { get => natArmorAC; set { natArmorAC = value; OnPropertyChanged("NatArmorAC"); CalcAC(); } }

        private bool natArmor;
        public bool NatArmor { get => natArmor; set { natArmor = value; OnPropertyChanged("NatArmor"); CalcAC(); } }

        private bool shield;
        public bool Shield { get => shield; set { shield = value; OnPropertyChanged("Shield"); CalcAC(); } }

        private int ac;
        public int AC { get => ac; set { ac = value; OnPropertyChanged("AC"); } }

        private int speed, climb, swim, burrow, fly;
        public int Speed { get => speed; set { speed = value; OnPropertyChanged("Speed"); } }
        public int Climb { get => climb; set { climb = value; OnPropertyChanged("Climb"); } }
        public int Swim { get => swim; set { swim = value; OnPropertyChanged("Swim"); } }
        public int Burrow { get => burrow; set { burrow = value; OnPropertyChanged("Burrow"); } }
        public int Fly { get => fly; set { fly = value; OnPropertyChanged("Fly"); } }

        private bool hover;
        public bool Hover { get => hover; set { hover = value; OnPropertyChanged("Hover"); } }

        private int strScore, dexScore, conScore, intScore, wisScore, chaScore;
        public int StrScore { get => strScore; set
            {
                strScore = value;
                OnPropertyChanged("StrScore");
                OnPropertyChanged("StrMod");
                OnPropertyChanged("StrSave");
                OnPropertyChanged("Athletics");
            } }
        public int DexScore { get => dexScore; set
            {
                dexScore = value;
                OnPropertyChanged("DexScore");
                OnPropertyChanged("DexMod");
                OnPropertyChanged("DexSave");
                OnPropertyChanged("Acrobatics");
                OnPropertyChanged("SleightOfHand");
                OnPropertyChanged("Stealth");
                CalcAC(); } }
        public int ConScore { get => conScore; set { conScore = value; OnPropertyChanged("ConScore"); OnPropertyChanged("ConMod"); OnPropertyChanged("ConSave"); CalcHP(); } }
        public int IntScore { get => intScore; set
            {
                intScore = value;
                OnPropertyChanged("IntScore");
                OnPropertyChanged("IntMod");
                OnPropertyChanged("IntSave");
                OnPropertyChanged("Arcana");
                OnPropertyChanged("History");
                OnPropertyChanged("Investigation");
                OnPropertyChanged("Nature");
                OnPropertyChanged("Religion");
            } }
        public int WisScore { get => wisScore; set
            {
                wisScore = value;
                OnPropertyChanged("WisScore");
                OnPropertyChanged("WisMod");
                OnPropertyChanged("WisSave");
                OnPropertyChanged("AnimalHandling");
                OnPropertyChanged("Insight");
                OnPropertyChanged("Medicine");
                OnPropertyChanged("Perception");
                OnPropertyChanged("Survival");
            } }
        public int ChaScore { get => chaScore; set
            {
                chaScore = value;
                OnPropertyChanged("ChaScore");
                OnPropertyChanged("ChaMod");
                OnPropertyChanged("ChaSave");
                OnPropertyChanged("Deception");
                OnPropertyChanged("Intimidation");
                OnPropertyChanged("Performance");
                OnPropertyChanged("Persuasion");
            } }
        public int StrMod { get => ScoreToMod(StrScore); }
        public int DexMod { get => ScoreToMod(DexScore); }
        public int ConMod { get => ScoreToMod(ConScore); }
        public int IntMod { get => ScoreToMod(IntScore); }
        public int WisMod { get => ScoreToMod(WisScore); }
        public int ChaMod { get => ScoreToMod(ChaScore); }

        private ChallengeRating cr;
        public ChallengeRating CR { get => cr; set { cr = value; OnPropertyChanged("CR"); UpdateProfRelated(); } }

        private bool strSaveProf, dexSaveProf, conSaveProf, intSaveProf, wisSaveProf, chaSaveProf;
        public bool StrSaveProf { get => strSaveProf; set { strSaveProf = value; OnPropertyChanged("StrSaveProf"); OnPropertyChanged("StrSave"); } }
        public bool DexSaveProf { get => dexSaveProf; set { dexSaveProf = value; OnPropertyChanged("DexSaveProf"); OnPropertyChanged("DexSave"); } }
        public bool ConSaveProf { get => conSaveProf; set { conSaveProf = value; OnPropertyChanged("ConSaveProf"); OnPropertyChanged("ConSave"); } }
        public bool IntSaveProf { get => intSaveProf; set { intSaveProf = value; OnPropertyChanged("IntSaveProf"); OnPropertyChanged("IntSave"); } }
        public bool WisSaveProf { get => wisSaveProf; set { wisSaveProf = value; OnPropertyChanged("WisSaveProf"); OnPropertyChanged("WisSave"); } }
        public bool ChaSaveProf { get => chaSaveProf; set { chaSaveProf = value; OnPropertyChanged("ChaSaveProf"); OnPropertyChanged("ChaSave"); } }

        public int StrSave { get => StrMod + (StrSaveProf ? CR.ProfBonus : 0); }
        public int DexSave { get => DexMod + (DexSaveProf ? CR.ProfBonus : 0); }
        public int ConSave { get => ConMod + (ConSaveProf ? CR.ProfBonus : 0); }
        public int IntSave { get => IntMod + (IntSaveProf ? CR.ProfBonus : 0); }
        public int WisSave { get => WisMod + (WisSaveProf ? CR.ProfBonus : 0); }
        public int ChaSave { get => ChaMod + (ChaSaveProf ? CR.ProfBonus : 0); }

        private SkillProficiencyType athleticsProf, acrobaticsProf, sleightOfHandProf, stealthProf, arcanaProf, historyProf, investigationProf, natureProf,
            religionProf, animalHandlingProf, insightProf, medicineProf, perceptionProf, survivalProf, deceptionProf, intimidationProf, performanceProf,
            persuasionProf;
        public SkillProficiencyType AthleticsProf
        {
            get => athleticsProf;
            set
            {
                athleticsProf = value;
                OnPropertyChanged("AthleticsProf");
                OnPropertyChanged("Athletics");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType AcrobaticsProf
        {
            get => acrobaticsProf;
            set
            {
                acrobaticsProf = value;
                OnPropertyChanged("AcrobaticsProf");
                OnPropertyChanged("Acrobatics");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType SleightOfHandProf
        {
            get => sleightOfHandProf;
            set
            {
                sleightOfHandProf = value;
                OnPropertyChanged("SleightOfHandProf");
                OnPropertyChanged("SleightOfHand");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType StealthProf
        {
            get => stealthProf;
            set
            {
                stealthProf = value;
                OnPropertyChanged("StealthProf");
                OnPropertyChanged("Stealth");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType ArcanaProf
        {
            get => arcanaProf;
            set
            {
                arcanaProf = value;
                OnPropertyChanged("ArcanaProf");
                OnPropertyChanged("Arcana");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType HistoryProf
        {
            get => historyProf;
            set
            {
                historyProf = value;
                OnPropertyChanged("HistoryProf");
                OnPropertyChanged("History");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType InvestigationProf
        {
            get => investigationProf;
            set
            {
                investigationProf = value;
                OnPropertyChanged("InvestigationProf");
                OnPropertyChanged("Investigation");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType NatureProf
        {
            get => natureProf;
            set
            {
                natureProf = value;
                OnPropertyChanged("NatureProf");
                OnPropertyChanged("Nature");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType ReligionProf
        {
            get => religionProf;
            set
            {
                religionProf = value;
                OnPropertyChanged("ReligionProf");
                OnPropertyChanged("Religion");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType AnimalHandlingProf
        {
            get => animalHandlingProf;
            set
            {
                animalHandlingProf = value;
                OnPropertyChanged("AnimalHandlingProf");
                OnPropertyChanged("AnimalHandling");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType InsightProf
        {
            get => insightProf;
            set
            {
                insightProf = value;
                OnPropertyChanged("InsightProf");
                OnPropertyChanged("Insight");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType MedicineProf
        {
            get => medicineProf;
            set
            {
                medicineProf = value;
                OnPropertyChanged("MedicineProf");
                OnPropertyChanged("Medicine");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType PerceptionProf
        {
            get => perceptionProf;
            set
            {
                perceptionProf = value;
                OnPropertyChanged("PerceptionProf");
                OnPropertyChanged("Perception");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType SurvivalProf
        {
            get => survivalProf;
            set
            {
                survivalProf = value;
                OnPropertyChanged("SurvivalProf");
                OnPropertyChanged("Survival");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType DeceptionProf
        {
            get => deceptionProf;
            set
            {
                deceptionProf = value;
                OnPropertyChanged("DeceptionProf");
                OnPropertyChanged("Deception");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType IntimidationProf
        {
            get => intimidationProf;
            set
            {
                intimidationProf = value;
                OnPropertyChanged("IntimidationProf");
                OnPropertyChanged("Intimidation");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType PerformanceProf
        {
            get => performanceProf;
            set
            {
                performanceProf = value;
                OnPropertyChanged("PerformanceProf");
                OnPropertyChanged("Performance");
                OnPropertyChanged("SkillList");
            }
        }
        public SkillProficiencyType PersuasionProf
        {
            get => persuasionProf;
            set
            {
                persuasionProf = value;
                OnPropertyChanged("PersuasionProf");
                OnPropertyChanged("Persuasion");
                OnPropertyChanged("SkillList");
            }
        }

        public int Athletics { get => SkillBonus(AthleticsProf, CR.ProfBonus) + StrMod; }
        public int Acrobatics { get => SkillBonus(AcrobaticsProf, CR.ProfBonus) + DexMod; }
        public int SleightOfHand { get => SkillBonus(SleightOfHandProf, CR.ProfBonus) + DexMod; }
        public int Stealth { get => SkillBonus(StealthProf, CR.ProfBonus) + DexMod; }
        public int Arcana { get => SkillBonus(ArcanaProf, CR.ProfBonus) + IntMod; }
        public int History { get => SkillBonus(HistoryProf, CR.ProfBonus) + IntMod; }
        public int Investigation { get => SkillBonus(InvestigationProf, CR.ProfBonus) + IntMod; }
        public int Nature { get => SkillBonus(NatureProf, CR.ProfBonus) + IntMod; }
        public int Religion { get => SkillBonus(ReligionProf, CR.ProfBonus) + IntMod; }
        public int AnimalHandling { get => SkillBonus(AnimalHandlingProf, CR.ProfBonus) + WisMod; }
        public int Insight { get => SkillBonus(InsightProf, CR.ProfBonus) + WisMod; }
        public int Medicine { get => SkillBonus(MedicineProf, CR.ProfBonus) + WisMod; }
        public int Perception { get => SkillBonus(PerceptionProf, CR.ProfBonus) + WisMod; }
        public int Survival { get => SkillBonus(SurvivalProf, CR.ProfBonus) + WisMod; }
        public int Deception { get => SkillBonus(DeceptionProf, CR.ProfBonus) + ChaMod; }
        public int Intimidation { get => SkillBonus(IntimidationProf, CR.ProfBonus) + ChaMod; }
        public int Performance { get => SkillBonus(PerformanceProf, CR.ProfBonus) + ChaMod; }
        public int Persuasion { get => SkillBonus(PersuasionProf, CR.ProfBonus) + ChaMod; }

        public string SkillList { get
            {
                string s = "";
                if (AthleticsProf != SkillProficiencyType.NONE)
                    s += ", Athletics " + ModDisplay(Athletics);
                if (AcrobaticsProf != SkillProficiencyType.NONE)
                    s += ", Acrobatics " + ModDisplay(Acrobatics);
                if (SleightOfHandProf != SkillProficiencyType.NONE)
                    s += ", Sleight of Hand " + ModDisplay(SleightOfHand);
                if (StealthProf != SkillProficiencyType.NONE)
                    s += ", Stealth " + ModDisplay(Stealth);
                if (ArcanaProf != SkillProficiencyType.NONE)
                    s += ", Arcana " + ModDisplay(Arcana);
                if (HistoryProf != SkillProficiencyType.NONE)
                    s += ", History " + ModDisplay(History);
                if (InvestigationProf != SkillProficiencyType.NONE)
                    s += ", Investigation " + ModDisplay(Investigation);
                if (NatureProf != SkillProficiencyType.NONE)
                    s += ", Nature " + ModDisplay(Nature);
                if (ReligionProf != SkillProficiencyType.NONE)
                    s += ", Religion " + ModDisplay(Religion);
                if (AnimalHandlingProf != SkillProficiencyType.NONE)
                    s += ", Animal Handling " + ModDisplay(AnimalHandling);
                if (InsightProf != SkillProficiencyType.NONE)
                    s += ", Insight " + ModDisplay(Insight);
                if (MedicineProf != SkillProficiencyType.NONE)
                    s += ", Medicine " + ModDisplay(Medicine);
                if (PerceptionProf != SkillProficiencyType.NONE)
                    s += ", Perception " + ModDisplay(Perception);
                if (SurvivalProf != SkillProficiencyType.NONE)
                    s += ", Survival " + ModDisplay(Survival);
                if (DeceptionProf != SkillProficiencyType.NONE)
                    s += ", Deception " + ModDisplay(Deception);
                if (IntimidationProf != SkillProficiencyType.NONE)
                    s += ", Intimidation " + ModDisplay(Intimidation);
                if (PerformanceProf != SkillProficiencyType.NONE)
                    s += ", Performance " + ModDisplay(Performance);
                if (PersuasionProf != SkillProficiencyType.NONE)
                    s += ", Persuasion " + ModDisplay(Persuasion);
                return (s.Equals("") ? s : s.Substring(2));
            } }

        private DamageEffect acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder, nonmagical;
        public DamageEffect Acid { get => acid; set { acid = value; OnPropertyChanged("Acid"); } }
        public DamageEffect Bludgeoning { get => bludgeoning; set { bludgeoning = value; OnPropertyChanged("Bludgeoning"); } }
        public DamageEffect Cold { get => cold; set { cold = value; OnPropertyChanged("Cold"); } }
        public DamageEffect Fire { get => fire; set { fire = value; OnPropertyChanged("Fire"); } }
        public DamageEffect Force { get => force; set { force = value; OnPropertyChanged("Force"); } }
        public DamageEffect Lightning { get => lightning; set { lightning = value; OnPropertyChanged("Lightning"); } }
        public DamageEffect Necrotic { get => necrotic; set { necrotic = value; OnPropertyChanged("Necrotic"); } }
        public DamageEffect Piercing { get => piercing; set { piercing = value; OnPropertyChanged("Piercing"); } }
        public DamageEffect Poison { get => poison; set { poison = value; OnPropertyChanged("Poison"); } }
        public DamageEffect Psychic { get => psychic; set { psychic = value; OnPropertyChanged("Psychic"); } }
        public DamageEffect Radiant { get => radiant; set { radiant = value; OnPropertyChanged("Radiant"); } }
        public DamageEffect Slashing { get => slashing; set { slashing = value; OnPropertyChanged("Slashing"); } }
        public DamageEffect Thunder { get => thunder; set { thunder = value; OnPropertyChanged("Thunder"); } }
        public DamageEffect Nonmagical { get => nonmagical; set { nonmagical = value; OnPropertyChanged("Nonmagical"); } }



        private bool blindOutside;
        private int extraLanguages;
        
        public ObservableCollection<ConditionType> CondImmunities { get; set; }
        public ObservableCollection<int> Senses { get; set; }
        public ObservableCollection<string> Languages { get; set; }

        // Some list of abilities
        // Some list of actions
        // Some list of legendary actions

        public Creature(string name, CreatureType type, Alignment alignment, CreatureSize size, int amthd, Die hd, int hp, Armor armorSource,
                        string natArmorSource, int natArmorAC, bool natArmor, bool shield, int ac, int speed, int climb, int burrow, int fly,
                        bool hover, int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore, ChallengeRating cr,
                        bool strSaveProf, bool dexSaveProf, bool conSaveProf, bool intSaveProf, bool wisSaveProf, bool chaSaveProf,
                        bool athleticsProf, bool acrobaticsProf, bool sleightOfHandProf, bool stealthProf, bool arcanaProf, bool historyProf,
                        bool investigationProf, bool natureProf, bool religionProf, bool animalHandlingProf, bool insightProf, bool medicineProf,
                        bool PerceptionProf, bool survivalProf, bool deceptionProf, bool intimidationProf, bool performanceProf, bool persuasionProf)
        {
            this.name = name;
            this.type = type;
            this.alignment = alignment;
            this.size = size;
            this.amthd = amthd;
            this.hd = hd;
            this.hp = hp;
            this.armorSource = armorSource;
            this.natArmorSource = natArmorSource;
            this.natArmorAC = natArmorAC;
            this.natArmor = natArmor;
            this.shield = shield;
            this.ac = ac;
            this.speed = speed;
            this.climb = climb;
            this.burrow = burrow;
            this.fly = fly;
            this.hover = hover;
            this.strScore = strScore;
            this.dexScore = dexScore;
            this.conScore = conScore;
            this.intScore = intScore;
            this.wisScore = wisScore;
            this.chaScore = chaScore;
            this.cr = cr;
            this.strSaveProf = strSaveProf;
            this.dexSaveProf = dexSaveProf;
            this.conSaveProf = conSaveProf;
            this.intSaveProf = intSaveProf;
            this.wisSaveProf = wisSaveProf;
            this.chaSaveProf = chaSaveProf;
        }

        public Creature(string name, int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore) : this(name, CreatureType.HUMANOID,
                        Alignment.U, CreatureSize.MEDIUM, 1, new Die(6), 3, null, "", 10, false, false, 10, 30, 0, 0, 0, false, 10, 10, 10, 10, 10, 10,
                        new ChallengeRating(0, 0, 2), false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                        false, false, false, false, false, false, false, false, false, false) { }

        public Creature(string name) : this(name, 10, 10, 10, 10, 10, 10) { }

        public void CalcHP()
        {
            this.HP = (int)Math.Floor(this.AmtHD * (this.HD.Average + this.ConMod));
        }

        public void CalcAC()
        {
            if (NatArmor)
                AC = NatArmorAC + DexMod + (Shield ? 2 : 0);
            else
            {
                if (ArmorSource == null)
                    AC = 10 + DexMod;
                else
                {
                    switch (ArmorSource.Type)
                    {
                        case ArmorType.HEAVY:
                            AC = ArmorSource.BaseAC;
                            break;
                        case ArmorType.MEDIUM:
                            AC = ArmorSource.BaseAC + Math.Min(2, DexMod);
                            break;
                        default:
                            AC = ArmorSource.BaseAC + DexMod;
                            break;
                    }
                }
                if (Shield)
                    AC += 2;
            }
        }

        private static int ScoreToMod(int score)
        {
            return (int)Math.Floor(((double)score - 10) / 2);
        }

        private static int SkillBonus(SkillProficiencyType prof, int bonus)
        {
            switch (prof)
            {
                case SkillProficiencyType.EXPERTISE:
                    return bonus * 2;
                case SkillProficiencyType.PROFICIENCY:
                    return bonus;
                default:
                    return 0;
            }
        }

        private static Die SizeToHitDie(CreatureSize size)
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

        private static string ModDisplay(int mod)
        {
            return (mod < 0 ? "" : "+") + mod;
        }

        private void UpdateProfRelated()
        {
            List<string> skills = new List<string> { "Athletics", "Acrobatics", "SleightOfHand", "Stealth", "Arcana", "History", "Investigation", "Nature", "Religion", "AnimalHandling",
                "Insight", "Medicine", "Perception", "Survival", "Deception", "Intimidation", "Performance", "Persuasion"};
            List<string> saves = new List<string> { "StrSave", "DexSave", "ConSave", "IntSave", "WisSave", "ChaSave" };

            PropertyUpdateList(skills);
            PropertyUpdateList(saves);
        }
    }
}
