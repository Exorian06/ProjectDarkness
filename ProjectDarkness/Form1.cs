using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;


namespace ProjectDarkness
{
    public partial class MainForm : Form
    {
        private const int gridSize = 7;

        private MapCell[,] map = new MapCell[gridSize, gridSize];

        private int playerX = 3;
        private int playerY = 3;

        private const int cellSize = 50;

        private string lastTypedText = "";

        private Character player;
        private Monster currentMonster;
        private bool inBattle = false;

        public MainForm()
        {

            InitializeComponent();

            LoadVersionFromDatabase();

            InitializeMap();
            UpdateHelyszinDisplay();
            UpdateTerkepDisplay();
            CheckCellEvent();

            player = new Character
            {
                Name = "Hős",
                MaxHealth = 100,
                Health = 100,
                AttackPower = 25
            };
        }

        private void RestartGame()
        {
            playerX = 3;
            playerY = 3;
            lastTypedText = "";
            currentMonster = null;
            inBattle = false;

            player = new Character
            {
                Name = "Hős",
                MaxHealth = 100,
                Health = 100,
                AttackPower = 25
            };

            InitializeMap();
            UpdateHelyszinDisplay();
            UpdateTerkepDisplay();
            CheckCellEvent();


        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MenuListBox.Focus();
            this.ActiveControl = MenuListBox;
        }

        private void InitializeMap()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    map[i, j] = ConfigureCell(j, i);
                }
            }
        }

        private async Task TypeTextAsync(string text, int delay = 25)
        {
            lastTypedText = SzovegTextBox.Text;
            SzovegTextBox.Text = "";

            foreach (char c in text)
            {
                SzovegTextBox.Text += c;
                await Task.Delay(delay);
            }
        }


        #region Cellak


        private MapCell ConfigureCell(int x, int y)
        {
            MapCell cell = new MapCell();

            cell.OnEnter = async () => { SzovegTextBox.Text = lastTypedText; };

            switch ((x, y))
            {
                case (0, 0):
                    cell.CellImage = Properties.Resources._0_0;
                    cell.CellType = CellEventType.Merchant;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Az erdő szélén állsz. Ahogy kilépsz a tisztásra, egy idős, hosszú szakállú, rongyos öltözetű de kedves arcú emberrel találkozol," +
                            " akinek a portékái egy nagy fa aljánál vannak szét pakolva. Főként gyógyitalokat és fegyver fejlesztéseket árul. " +
                            "Mögötte egy másik erdő határait látod mely a végtelenségbe nyúlik el.");
                    };
                    break;

                case (0, 1):
                    cell.CellImage = Properties.Resources._0_1;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy folyó partjára értél ahol egy hatalmas, zöld, izmos orkba ütközöl. Kezében egy irdatlan fejszét, mellkasán korábbi harcok sebeit látod." +
                            " Vérszomjasan vicsorog rád, majd rövid méregetés után megtámad!");
                    };
                    break;

                case (0, 2):
                    cell.CellImage = Properties.Resources._0_2;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy ezüstös színű kincsesládát látsz egy fa tövében. Kinyitva fantasztikus kincsekre teszel szert. " +
                            "Egy mágikus fegyverfejlesztő varázslat szállja meg a kardodat. Ezzel nagyobb sebzésre tettél szert!");
                    };
                    break;

                case (0, 3):
                    cell.CellImage = Properties.Resources._0_3;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy hatalmas Ork harcos áll előtted, kivont kardját fenyegetően rád szegezi majd hirtelen megtámad!");
                    };
                    break;

                case (0, 4):
                    cell.CellImage = Properties.Resources._0_4;
                    cell.CellType = CellEventType.Merchant;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Az úton elérsz egy hatalmas fánál üldögélő öreg emberhez aki portékáját a fa tövéből kivájt kis kuckóban árulja. " +
                            "Gyógyitalokat és fegyver fejlesztést vehetsz itt.");
                    };
                    break;

                case (0, 5):
                    cell.CellImage = Properties.Resources._0_5;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel az erdő mélyén. A távolban egy unikornis alakját látod feltűnnni az aljnövényzet sűrű ölelésében. " +
                            "Tudod hogy szerencséd lesz amint kinyitod a ládát és így is lett. Egy fegyver erősítő varázslat szállja meg kardodat és így nagyobb sebzésre tettél szert.");
                    };
                    break;

                case (0, 6):
                    cell.CellImage = Properties.Resources._0_6;
                    cell.CellType = CellEventType.City;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy városba értél. Itt aztán mindenféle életforma megtalálható. Ember, vadállat, féllények. " +
                            "A város nyüzsög az árúsoktól. Mind a portékáját próbálja értékesíteni. Nézz körül és vásárolj kedvedre.");
                    };
                    break;

                case (1, 0):
                    cell.CellImage = Properties.Resources._1_0;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy gigantikus fekete medve áll előtted. Hosszan méreget. Látszik hogy éhes és úgy néz ki te lettél a kiválaszott, hogy a vacsorájaként szolgálj. Megtámad!");
                    };
                    break;

                case (1, 1):
                    cell.CellImage = Properties.Resources._1_1;
                    cell.CellType = CellEventType.City;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy városba értél. Itt aztán mindenféle életforma megtalálható. " +
                            "Ember, vadállat, féllények. A város nyüzsög az árúsoktól. Mind a portékáját próbálja értékesíteni. Nézz körül és vásárolj kedvedre.");
                    };
                    break;

                case (1, 2):
                    cell.CellImage = Properties.Resources._1_2;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy elhagyatott romhoz értél. A kincsek amiket itt találsz sokat érhetnek egy kincskereső számára. " +
                            "De te nem vagy az. Így inkább nem bolygatod meg a sír békéjét és ott hagyod a kincseket.");
                    };
                    break;

                case (1, 3):
                    cell.CellImage = Properties.Resources._1_3;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;

                case (1, 4):
                    cell.CellImage = Properties.Resources._1_4;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy szép erdei helyre értél. A fák körös körül érintetlenek, mintha soha ember fia még nem járt volna erre.");
                    };
                    break;

                case (1, 5):
                    cell.CellImage = Properties.Resources._1_5;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Három bandita lép ki az erdő sűrűjéből és rádtámad egy patak partján. Védd magad!");
                    };
                    break;

                case (1, 6):
                    cell.CellImage = Properties.Resources._1_6;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy elhagyott rom közelébe érsz. Látod hogy a bejáratát egy szellem őrzi aki nem tűnik túl boldognak hogy a nyughelyét zavarják. " +
                            "Ezért inkább békén hagyod és továbbállsz.");
                    };
                    break;

                case (2, 0):
                    cell.CellImage = Properties.Resources._2_0;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy régi kincsesládát találsz az erdő mélyén. Körülötte törpék ülnek és pipáznak. " +
                            "Egy fegyver fejlesztő varázslatot kapsz tőlük ajándékba amint kinyitod a titkokkal teli ládát.");
                    };
                    break;

                case (2, 1):
                    cell.CellImage = Properties.Resources._2_1;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Két sötét ruhás bandita támad rád az erdő szélén. Védd magad!");
                    };
                    break;

                case (2, 2):
                    cell.CellImage = Properties.Resources._2_2;
                    cell.CellType = CellEventType.Quest;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg csuhással találod szembe magad. Egy fontos küldetést akar rád bízni.");
                    };
                    break;

                case (2, 3):
                    cell.CellImage = Properties.Resources._2_3;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Két gigantikus pók támad rád az erdő mélyén.");
                    };
                    break;

                case (2, 4):
                    cell.CellImage = Properties.Resources._2_4;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Három hatalmas pók kezd el leereszkedni a környező fák közül, és egy kis körözés után megtámadnak téged.");
                    };
                    break;

                case (2, 5):
                    cell.CellImage = Properties.Resources._2_5;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;

                case (2, 6):
                    cell.CellImage = Properties.Resources._2_6;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;

                case (3, 0):
                    cell.CellImage = Properties.Resources._3_0;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy félelmetesen hatalmas fekete páncélt viselő alak bukkan fel előtted a rét közepén. Semmit sem kérdez, csak hosszasan bámul maga elé. " +
                            "Mikor viszont megpróbálod megszólítani akkor hirtelen neked támad.");
                    };
                    break;

                case (3, 1):
                    cell.CellImage = Properties.Resources._3_1;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy lápos, vibrálóan üde színekkel teli erdő szakaszt látsz magad előtt. Olyan mint ha az idő megállt volna egy pillanatra. " +
                            "Egy ideig csodálod a környezet szépségét és magadba szívod minden gyönyörű részletét.");
                    };
                    break;

                case (3, 2):
                    cell.CellImage = Properties.Resources._3_2;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;


                case (3, 3):
                    cell.CellImage = Properties.Resources._3_3;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy erdő mélyén találod magad, a közelben apró patak csörgedezik halkan, csak néha törik meg a víz felszíne az alatta megbújó sziklákon. " +
                            "Madarak csicseregnek, a fák lombjai susognak a szélben. Az ég kék, a fű zöld. Itt indul a kalandod...");
                    };
                    break;

                case (3, 4):
                    cell.CellImage = Properties.Resources._3_4;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;

                case (3, 5):
                    cell.CellImage = Properties.Resources._3_5;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Hatalmas farkasok vesznek körbe az erdő eme elszeparált árnyékos szegletében. Úgy tűnik ma te leszel a vacsorájuk ha nem véded meg magad.");
                    };
                    break;

                case (3, 6):
                    cell.CellImage = Properties.Resources._3_6;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy félelmetesen hatalmas fekete páncélt viselő alak bukkan fel előtted a rét közepén. " +
                            "Semmit sem kérdez, csak hosszasan bámul maga elé. Mikor viszont megpróbálod megszólítani akkor hirtelen neked támad.");
                    };
                    break;

                case (4, 0):
                    cell.CellImage = Properties.Resources._4_0;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("Egy öreg kincsesládát fedeztél fel. Amint kinyitod érzed hogy a fegyvered kicsit nehezebb lett de azt is hogy nagyobbat tudsz vele sebezni.");
                    };
                    break;

                case (4, 1):
                    cell.CellImage = Properties.Resources._4_1;
                    cell.CellType = CellEventType.Merchant;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (4, 2):
                    cell.CellImage = Properties.Resources._4_2;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (4, 3):
                    cell.CellImage = Properties.Resources._4_3;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (4, 4):
                    cell.CellImage = Properties.Resources._4_4;
                    cell.CellType = CellEventType.Merchant;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (4, 5):
                    cell.CellImage = Properties.Resources._4_5;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (4, 6):
                    cell.CellImage = Properties.Resources._4_6;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 0):
                    cell.CellImage = Properties.Resources._5_0;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 1):
                    cell.CellImage = Properties.Resources._5_1;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 2):
                    cell.CellImage = Properties.Resources._5_2;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 3):
                    cell.CellImage = Properties.Resources._5_3;
                    cell.CellType = CellEventType.City;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 4):
                    cell.CellImage = Properties.Resources._5_4;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 5):
                    cell.CellImage = Properties.Resources._5_5;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (5, 6):
                    cell.CellImage = Properties.Resources._5_6;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 0):
                    cell.CellImage = Properties.Resources._6_0;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 1):
                    cell.CellImage = Properties.Resources._6_1;
                    cell.CellType = CellEventType.Discovery;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 2):
                    cell.CellImage = Properties.Resources._6_2;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 3):
                    cell.CellImage = Properties.Resources._6_3;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 4):
                    cell.CellImage = Properties.Resources._6_4;
                    cell.CellType = CellEventType.Upgrade;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 5):
                    cell.CellImage = Properties.Resources._6_5;
                    cell.CellType = CellEventType.Battle;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                case (6, 6):
                    cell.CellImage = Properties.Resources._6_6;
                    cell.CellType = CellEventType.City;
                    cell.OnEnter = async () =>
                    {
                        await TypeTextAsync("");
                    };
                    break;

                default:
                    cell.CellImage = Properties.Resources._default;
                    cell.CellType = CellEventType.None;
                    cell.OnEnter = null;
                    break;
            }
            return cell;
        }

        #endregion

        public void UpdateMenuOptions(CellEventType cellEventType)
        {
            MenuListBox.Items.Clear();

            switch (cellEventType)
            {
                case CellEventType.Battle:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    MenuListBox.Items.Add("Megtámadom");
                    MenuListBox.Items.Add("Gyógyitalt használok");
                    MenuListBox.Items.Add("Megpróbálok elmenekülni");
                    break;

                case CellEventType.City:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    MenuListBox.Items.Add("Beszélgetek a lakókkal");
                    MenuListBox.Items.Add("Gyógyitalt veszek");
                    MenuListBox.Items.Add("Fegyver erősítést veszek");
                    MenuListBox.Items.Add("Küldetést keresek");
                    break;

                case CellEventType.Merchant:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    MenuListBox.Items.Add("Gyógyitalt veszek");
                    MenuListBox.Items.Add("Fegyver erősítést veszek");
                    break;

                case CellEventType.Upgrade:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    MenuListBox.Items.Add("Ajándék elfogadása");
                    break;

                case CellEventType.Discovery:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    break;

                case CellEventType.None:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    break;

                case CellEventType.Quest:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    MenuListBox.Items.Add("Küldetést elfogadom");
                    break;

                default:
                    MenuListBox.Items.Add("Észak felé megyek");
                    MenuListBox.Items.Add("Dél felé megyek");
                    MenuListBox.Items.Add("Nyugat felé megyek");
                    MenuListBox.Items.Add("Kelet felé megyek");
                    break;
            }
        }

        //----------------------------------------------------------------------------------

        private void StartBattle()
        {
            
            switch ((playerX, playerY))
            {
                case (0, 1):
                    currentMonster = new Monster
                    {
                        Name = "Ork Harcos",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (0, 3):
                    currentMonster = new Monster
                    {
                        Name = "Ork Harcos",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (1, 0):
                    currentMonster = new Monster
                    {
                        Name = "Medve",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (1, 5):
                    currentMonster = new Monster
                    {
                        Name = "Bandita",
                        MaxHealth = 75,
                        Health = 75,
                        AttackPower = 20
                    };
                    break;

                case (2, 1):
                    currentMonster = new Monster
                    {
                        Name = "Bandita",
                        MaxHealth = 75,
                        Health = 75,
                        AttackPower = 20
                    };
                    break;

                case (2, 3):
                    currentMonster = new Monster
                    {
                        Name = "Pók",
                        MaxHealth = 50,
                        Health = 50,
                        AttackPower = 15
                    };
                    break;

                case (2, 4):
                    currentMonster = new Monster
                    {
                        Name = "Pók",
                        MaxHealth = 50,
                        Health = 50,
                        AttackPower = 15
                    };
                    break;

                case (3, 0):
                    currentMonster = new Monster
                    {
                        Name = "Fekete Lovag",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (3, 5):
                    currentMonster = new Monster
                    {
                        Name = "Farkas",
                        MaxHealth = 75,
                        Health = 75,
                        AttackPower = 20
                    };
                    break;

                case (3, 6):
                    currentMonster = new Monster
                    {
                        Name = "Fekete Lovag",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (4, 2):
                    currentMonster = new Monster
                    {
                        Name = "Goblin",
                        MaxHealth = 50,
                        Health = 50,
                        AttackPower = 15
                    };
                    break;

                case (5, 0):
                    currentMonster = new Monster
                    {
                        Name = "Minotaurusz",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (5, 1):
                    currentMonster = new Monster
                    {
                        Name = "Farkas",
                        MaxHealth = 75,
                        Health = 75,
                        AttackPower = 20
                    };
                    break;

                case (5, 5):
                    currentMonster = new Monster
                    {
                        Name = "Bandita",
                        MaxHealth = 75,
                        Health = 75,
                        AttackPower = 20
                    };
                    break;

                case (6, 3):
                    currentMonster = new Monster
                    {
                        Name = "Minotaurusz",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (6, 5):
                    currentMonster = new Monster
                    {
                        Name = "Medve",
                        MaxHealth = 100,
                        Health = 100,
                        AttackPower = 25
                    };
                    break;

                case (6, 0):
                    currentMonster = new Monster
                    {
                        Name = "Háromfejű Sárkány",
                        MaxHealth = 250,
                        Health = 250,
                        AttackPower = 75
                    };
                    break;


                default:
                    currentMonster = new Monster
                    {
                        Name = "Ismeretlen lény",
                        MaxHealth = 50,
                        Health = 50,
                        AttackPower = 12
                    };
                    break;
            }

            inBattle = true;
            UpdateMenuOptionsForBattle();

            TypeTextToTextBox($" Egy {currentMonster.Name} állja utad! Készen állsz a harcra?");
        }



        private async Task AttemptEscape()
        {
            Random rng = new Random();
            bool success = rng.Next(0, 2) == 0;

            if (success)
            {
                await TypeTextToTextBox("Sikerült elmenekülnöd!", append: true);
                inBattle = false;
                UpdateMenuOptionsForMovement();
            }
            else
            {
                await TypeTextToTextBox("Nem sikerült elmenekülnöd!", append: true);
                EnemyAttacks();
            }
        }

        private void AcceptQuest()
        {
            
        }

        private void TalkToVillagers()
        {
            
        }

        private void GetWeaponUpgrade()
        {
            
        }

        private void BuyWeaponUpgrade()
        {
            
        }

        private void BuyPotion()
        {
            
        }

        private async Task UseHealingPotion()
        {
            int healAmount = 20;
            player.Health += healAmount;
            if (player.Health > player.MaxHealth) player.Health = player.MaxHealth;

            await TypeTextToTextBox($"Használtál egy gyógyitalt. Gyógyultál: {healAmount}. Jelenlegi életed: {player.Health}/{player.MaxHealth}", append: true);
            EnemyAttacks();
        }



        private async Task PlayerAttacks()
        {
            currentMonster.Health -= player.AttackPower;
            if (currentMonster.Health < 0) currentMonster.Health = 0;

            await TypeTextToTextBox($"Megtámadod a {currentMonster.Name}-t. Sebzést okoztál: {player.AttackPower}. Ellenfél élete: {currentMonster.Health}/{currentMonster.MaxHealth}", append: true);

            if (!currentMonster.IsAlive)
            {
                inBattle = false;
                await TypeTextToTextBox($"{currentMonster.Name} legyőzve!", append: true);
                UpdateMenuOptionsForMovement();
                return;
            }

            await EnemyAttacks();

        }

        private async Task EnemyAttacks()
        {
            if (currentMonster == null)
            {
                await TypeTextToTextBox("Nincs szörny a látóteredben.", append: true);
                return;
            }
            player.Health -= currentMonster.AttackPower;
            if (player.Health < 0) player.Health = 0;

            await TypeTextToTextBox($"{currentMonster.Name} visszatámad! Sebzést kaptál: {currentMonster.AttackPower}. Hős élete: {player.Health}/{player.MaxHealth}", append: true);

            if (!player.IsAlive)
            {
                TypeTextToTextBox("Meghaltál. Vége a játéknak.", append: true);
                MenuListBox.Items.Clear();
            }


        }

        //----------------------------------------------------------------------------------

        private async Task TypeTextToTextBox(string text, bool append = false)
        {
            if (!append)
            {
                SzovegTextBox.Text = "";
                lastTypedText = "";
            }

            foreach (char c in text)
            {
                SzovegTextBox.Text += c;
                await Task.Delay(15);
            }

            SzovegTextBox.Text += Environment.NewLine;
        }

        private void ClearCombatLog()
        {
            SzovegTextBox.Text = "";
        }

        private void UpdateMenuOptionsForBattle()
        {
            MenuListBox.Items.Clear();
            MenuListBox.Items.Add("Megtámadom");
            MenuListBox.Items.Add("Gyógyitalt használok");
            MenuListBox.Items.Add("Megpróbálok elmenekülni");
        }


        private void UpdateHelyszinDisplay()
        {
            HelyszinPictureBox.Image = map[playerY, playerX].CellImage;
        }

        private void UpdateTerkepDisplay()
        {
            Bitmap mapBitmap = new Bitmap(gridSize * cellSize, gridSize * cellSize);
            using (Graphics g = Graphics.FromImage(mapBitmap))
            {
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (map[i, j].CellImage != null)
                            g.DrawImage(map[i, j].CellImage, j * cellSize, i * cellSize, cellSize, cellSize);
                        else
                            g.FillRectangle(Brushes.Gray, j * cellSize, i * cellSize, cellSize, cellSize);

                        g.DrawRectangle(Pens.Black, j * cellSize, i * cellSize, cellSize, cellSize);
                    }
                }
                g.DrawRectangle(new Pen(Color.Red, 3), playerX * cellSize, playerY * cellSize, cellSize, cellSize);
            }
            TerkepPictureBox.Image = mapBitmap;
        }


        //----------------------------------------------------------------------------------

        private async void CheckCellEvent()
        {
            MapCell cell = map[playerY, playerX];

            if (cell.OnEnter != null)
            {
                await cell.OnEnter();
            }
            UpdateMenuOptions(cell.CellType);
        }


        //----------------------------------------------------------------------------------

        private void MoveUp()
        {
            if (playerY > 0)
            {
                playerY--;
                UpdateHelyszinDisplay();
                UpdateTerkepDisplay();
                CheckCellEvent();
            }
        }

        private void MoveDown()
        {
            if (playerY < gridSize - 1)
            {
                playerY++;
                UpdateHelyszinDisplay();
                UpdateTerkepDisplay();
                CheckCellEvent();
            }
        }

        private void MoveLeft()
        {
            if (playerX > 0)
            {
                playerX--;
                UpdateHelyszinDisplay();
                UpdateTerkepDisplay();
                CheckCellEvent();
            }
        }

        private void MoveRight()
        {
            if (playerX < gridSize - 1)
            {
                playerX++;
                UpdateHelyszinDisplay();
                UpdateTerkepDisplay();
                CheckCellEvent();
            }
        }


        private void UpdateMenuOptionsForMovement()
        {
            MenuListBox.Items.Clear();
            MenuListBox.Items.Add("Észak felé megyek");
            MenuListBox.Items.Add("Dél felé megyek");
            MenuListBox.Items.Add("Nyugat felé megyek");
            MenuListBox.Items.Add("Kelet felé megyek");
        }


        private async void MenuListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MenuListBox.SelectedItem != null)
            {
                string selectedOption = MenuListBox.SelectedItem.ToString();

                if (inBattle)
                {
                    switch (selectedOption)
                    {
                        case "Megtámadom":
                            await PlayerAttacks();
                            break;
                        case "Gyógyitalt használok":
                            await UseHealingPotion();
                            break;
                        case "Megpróbálok elmenekülni":
                            await AttemptEscape();
                            break;
                    }
                }
                else
                {
                    switch (selectedOption)
                    {
                        case "Észak felé megyek":
                            await TypeTextAsync("Észak felé indulsz...");
                            MoveUp();
                            break;

                        case "Dél felé megyek":
                            await TypeTextAsync("Dél felé indulsz...");
                            MoveDown();
                            break;

                        case "Nyugat felé megyek":
                            await TypeTextAsync("Nyugat felé indulsz...");
                            MoveLeft();
                            break;

                        case "Kelet felé megyek":
                            await TypeTextAsync("Kelet felé indulsz...");
                            MoveRight();
                            break;

                        case "Megtámadom":
                            await TypeTextAsync("Megpróbálod megtámadni az ellenséget!");
                            StartBattle();
                            break;

                        case "Gyógyitalt használok":
                            await TypeTextAsync("");
                            UseHealingPotion();
                            break;

                        case "Gyógyitalt veszek":
                            await TypeTextAsync("Gyógyitalt vettél!");
                            BuyPotion();
                            break;

                        case "Fegyver erősítést veszek":
                            await TypeTextAsync("Fegyver erősítést vettél!");
                            BuyWeaponUpgrade();
                            break;

                        case "Ajándék elfogadása":
                            await TypeTextAsync("Ajándékot kaptál!");
                            GetWeaponUpgrade();
                            break;

                        case "Beszélgetek a lakókkal":
                            await TypeTextAsync("Beszélgetsz a lakókkal...");
                            TalkToVillagers();
                            break;

                        case "Küldetést elfogadom":
                            await TypeTextAsync("Elfogadtál egy fontos küldetést!");
                            AcceptQuest();
                            break;

                        case "Megpróbálok elmenekülni":
                            await TypeTextAsync("Megpróbálsz elmenekülni...");
                            AttemptEscape();
                            break;

                        default:
                            await TypeTextAsync("Nem történik semmi különös.");
                            break;
                    }

                }

                e.Handled = true;
                e.SuppressKeyPress = true;

                MenuListBox.ClearSelected();
            }
        }


        private void LoadVersionFromDatabase()
        {
            var loader = new VersionLoader();
            try
            {
                var version = loader.GetVersion();
                if (version != null)
                {
                    VerzioLabel.Text = $"Verzió: {version}";
                }
                else
                {
                    VerzioLabel.Text = "Verzió: Ismeretlen";
                }
            }
            catch (Exception ex)
            {
                VerzioLabel.Text = "Verzió: [Hiba]";
                loader.ShowError(ex);
            }
        }


        private void ResetButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Biztosan újraindítod a játékot? Minden eddigi előrehaladás elvész.", "Játék újraindítása", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestartGame();
                MenuListBox.Focus();
                this.ActiveControl = MenuListBox;
            }
            else
            {
                MenuListBox.Focus();
                this.ActiveControl = MenuListBox;
            }
            
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
