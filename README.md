# BattleShip
Изработиле: Филип Голабоски, Љупчо Томев

1. Опис на играта
==================
  BattleShip e игра на погодување за двајца играчи. Играчите се во улога на капетани на морнарица и нивна цел е да ги потонат сите бродови на противникот. На почетокот од играта играчите ги поставуваат своите бродови на нивните позиции. Играта почнува и играчите погодуваат по случаен избор. Ако играчот погоди некој брод од противникот играчот забележува погоден брод на таа позиција и ги бара другите делов од бродот во четири насоки. Ако играчот погоди брод во тој момент противникот кажува дека неговиот брод е потонат. Играта продолжува се додека сите бродови на еден од играчите не се погодени.  
> **Историја на играта**  
>Играта е добро позната како игра со молив и хартија од Прва светска војна. Се мисли дека потекнува од француската игра _L'Ataque_ иако >исто така е слчна со играта _Baslinda_ и игра која наводно ја играле војници на руската армија во Прва светстка војна. Првата >комерцијална верзија на играта е _Salvo_ публицирана во 1931 година во Америка. Подоцна се појавуваат и други верзии на играта како >_Combat: The Battleship Game_ , _Broadsides: A Game of Naval Strategy_ и _Warfare Naval Combat_. Се појавуваат и безброј компјутерски >игри како и научно фантастичниот филм _Battleship_ од 2012 година.

2. Опис на апликацијата
===================
Апликацијата е развиена да е битка помеѓу еден физички противник и компјутерот. Играта е поделена во двa дела. Во првиот дел играчите ги поставуваат своите бродови и со притискање на копче започнува играта. Секој од играчите има 5 бродови кои треба да ги постави пред да почне играта. Откако играчот ќе ги постави своите бродови играта може да почне. Играчот погодува, ако промаши на ред е компјутерот. Алгоритмот за погодување на почеток погодува по случаен избор. Ако погоди брод ги зема соседните координати како потенцијални мети за погодување и погодува се додека не погоди брод или нема повеќе потези за погодување. Ако промаши на ред е играчот.
  2.1 Почеток на апликација
----------------------
Во почетокот апликацијата има еден Panel на кој се претставени копчиња New Game , How to play и Exit Game. Преку овој панел со кликање на копчето New Game ја започнуваме играта и истиот е поставен при притискање на копчето Esc да се појавува секој пат подоцна со цел играчот да може да почне нова игра.

  2.2 Поставување на бродови
-----------------------
Откако ќе се притисне на копчето New Game панелот се крие, а во прозорецот се прикажува setup режимот на играта. Тука играчот ги поставува своите бродови во левото табла. Десната табла е наменета за противникот и е исклучена од употреба. Играчот треба да ги постави своите 5 брода и тоа 3 Destroyer брода, 1 Battleship и 1 Aircraft carrier, исто така тој има опција за да ја менува нивната насока, односно дали бродот ќе биде поставен хоризонтално или вертикално.

  - Destroyer (претставува три полиња од мапата)
  - Battleship (претставува четири полиња од мапата)
  - Aircraft carrier (претставува пет полиња од мапата)
  
Со притискање на копчето **Start the battle** апликацијата се менува во режимот на борба.

  2.3 Борба
-------------------------
Играчите во режимот на борба погодуваат по случаен избор.Ако бродот на противникот е погоден тогаш играчот може да погодува по втор пат. Борбата трае се додека некој од играчите не ги погоди бродовите на другите играчи.

3 Решение на апликацијата
========================
## 3.1 Податочни структури

###3.1.1 Tiles
---- 
 Секое поле се чува во класа наречена **Tile** која наследува од **PictureBox** класата на Windows Forms.
 ``` C#
 public class Tile : PictureBox
    { 
        public int i, j;
        public bool boatHere = false;
        public bool isHighLighted = false;
        public bool clicked = false;
        public Image img = null;
      
        public Tile( int i, int j)
        {
            Height = 40;
            Width = 40;
            this.i = i;
            this.j = j;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public void setImage(Bitmap img)
        {
            this.Image = img;
            this.img = img;
        }
        public virtual void click() {}
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        public void setOpBoat(Image img)
        {
            boatHere = true;
            this.img = img;
        }
        public void setBoat()
        {
            boatHere = true;
            this.Image = null;
            Enabled = false;
            isHighLighted = false;   
        }
        public void highLight()
        {
            if (!boatHere)
            {
                isHighLighted = true;
                this.Image = null;
                BackColor = Color.Yellow;
            }
        }
        public void unhighLight()
        {
            if (!boatHere)
            {
                isHighLighted = false;
                this.Image = img;
            }

        }
    }
 ```
 Во оваа класа чуваме податоци за тоа дали полето е обележано, неговите координати во матрицата,која слика ја содржи како позадина и дали има брод на тоа место. Чуваме и друга слика која при поминување со глушецот ја памти претходната слика за да може да се врати на неа при излез со глушецот. Од оваа класа наследуваме три класи **ShipTile**, **WaterTile** и **OpponentShipTile** кои се однесуваат различно во режимот на борба (пример OpponentShipTile ја крие својата слика на брод, при кликање исцртуваме **X** врз полињата и слично). При поставување на бродовите ја користиме основната Tile класа, а во режимот на борба бидејки ја прецртуваме целата матрица соодветно, поствавуме објект кој го претставува тоа поле.
 
###3.1.2 SetUp
--------
 
 **PlayerSetupBoard** е класа во кои се чуваат матриците за поставување на бродовите. Наследуваат од Panel класата. PlayerSetupBoard има методи за "Highlighting" кои во зависност од насоката, бројот на полиња што треба да се обележат, дали веќе постои поле на тоа место и дали излегува надвор од матрицата обележува одреден број на полиња. Овие методи се повикуваат секогаш кога со глушецот ќе влеземе врз поле и излеземе од него. Врз секое поле додадаваме настан на клик во кој се поставуваат бродови на обележаните позиции. Исто така оваа класа содржи листа од хаш сетови во кои ги чуваме позициите на бродовите.
```C#
   private void Tiles_Enter(object sender, EventArgs e)
        {   //pri vleguvanje so glushecot se menuva bojata
            Tile temp = (Tile)sender;
            if (shipRotation==0)//spoder rotacijata na koja strana da se vrti
            {
                    if (temp.i < MAXI - numBoats) // ova e za da ne izleguva OutOfBounds indeksot
                    {// dolu se objasnati ovie funkcii
                        HighlighEnoughVerticalBoats(temp);
                    }
                    else
                    {
                        HighlightLessVerticalBoats(temp);
                    }
            }
            else
            {
                if (temp.j < MAXJ - numBoats)
                {
                    HighlighEnoughHorizontalBoats(temp);
                }
                else {
                    HighlightLessHorizontalBoats(temp);
                }
            }
        }
 private void HighlighEnoughVerticalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i + k, temp.j].highLight();//k delovi nadolu ako ima dovolno mesto pravi highligh
                k++;
            }

        }
 private void UnHighlighEnoughVerticalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i + k, temp.j].unhighLight(); //isto kako predhodno
                k++;
            }

        }
 private void HighlightLessVerticalBoats(Tile temp)
        {   //TODO ova treba da se modificira bidejki iako praktichno ne izgleda dobro
                 int k = 0;
            int i = MAXI-numBoats;
                while (k < numBoats)
                {
                    Tiles[i + k, temp.j].highLight();//slichna e logikata samo shto namesto nadolu da odi odi nagore 
                    k++;
                }
           
        }
  private void UnHighlightLessVerticalBoats(Tile temp)
        {
            int k = 0;
            int i = MAXI - numBoats;
            while (k < numBoats)
                {
                    Tiles[i + k, temp.j].unhighLight();
                    k++;
                }
           
        }
  private void HighlighEnoughHorizontalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i, temp.j+k].highLight();
                k++;
            }
        }
  private void UnHighlighEnoughHorizontalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i, temp.j+k].unhighLight();
                k++;
            }
        }
  private void HighlightLessHorizontalBoats(Tile temp)
        {
            int k = 0;
            int j = MAXJ - numBoats;
            while (k < numBoats)
            {
                Tiles[temp.i, j+k].highLight();//slichna e logikata samo shto namesto nadolu da odi odi nagore 
                k++;
            }
        }
  private void UnHighlightLessHorizontalBoats(Tile temp)
        {
            
            int k = 0;
            int j = MAXJ - numBoats;
            while (k < numBoats)
            {
                Tiles[temp.i, j+k].unhighLight();
                k++;
            }
        }
````
 **OpponentSetupBoard** е класа во која се вршат пресметки за тоа каде се позициите на бродовите на противникот. Со фунцијата _setBoat(int leng)_ имаме циклус во кој се пробува да се постави брод од должина leng и случајна насока. Циклусот работи се додека не се постави бродот. Исто така, класата содржи листа на бродови.
 ````C#
  public void setBoat(int leng) {
            bool boatSet = false;
            boatLength = leng;
            
            int k = boatLength;
            int dir = direction.Next(2);
            while (!boatSet)
            {
                int i = tileChooseI.Next(10);
                int j = tileChooseJ.Next(10);
                if (dir == 0)
                {
                    if (MAXI - i > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            HashSet<Index> boat = new HashSet<Index>();
                            while (k > 0)
                            {
                                if (leng == 3)
                                Tiles[i + k, j].setOpBoat(triImagesVer[k-1]);
                                if (leng == 4)
                                    Tiles[i + k, j].setOpBoat(fourImagesVer[k-1]);
                                if (leng == 5)
                                    Tiles[i + k, j].setOpBoat(fiveImagesVer[k-1]);
                                boat.Add(new Index(i + k, j));
                                k--;
                            }
                            listOfBoats.Add(boat);
                            boatSet = true;
                        }
                        else
                        {
                            boatSet = false;
                        }
                    }
                    if (MAXI - i < boatLength)
                    {
                        int pos = MAXI - boatLength - 1;
                        if (!Tiles[pos, j].boatHere)
                        {
                            if (checkTrue(dir, Tiles[pos, j]))
                            {
                                HashSet<Index> boat = new HashSet<Index>();
                                while (k > 0)
                                {
                                    if(leng==3)
                                    Tiles[pos + k, j].setOpBoat(triImagesVer[k-1]);
                                    if (leng == 4)
                                        Tiles[pos + k, j].setOpBoat(fourImagesVer[k-1]);
                                    if (leng == 5)
                                        Tiles[pos + k, j].setOpBoat(fiveImagesVer[k-1]);
                                    boat.Add(new Index(pos + k, j));
                                    k--;
                                }
                                listOfBoats.Add(boat);
                                boatSet = true;
                            }
                            else
                            {
                                boatSet = false;
                            }
                        }
                    }

                }
                else
                {
                    if (MAXJ - j > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            HashSet<Index> boat = new HashSet<Index>();
                            while (k > 0)
                            {
                                if(leng==3)
                                Tiles[i, j + k].setOpBoat(triImages[k-1]);
                                if (leng == 4)
                                    Tiles[i, j + k].setOpBoat(fourImages[k-1]);
                                if (leng == 5)
                                    Tiles[i, j + k].setOpBoat(fiveImages[k-1]);
                                boat.Add(new Index(i, j+k));
                                k--;
                            }
                            listOfBoats.Add(boat);
                            boatSet = true;
                        }
                        else
                        {
                            boatSet = false;
                        }
                    }
                    if (MAXI - j < boatLength)
                    {
                        int pos = MAXJ - boatLength-1;

                        if (!Tiles[i, pos].boatHere)
                        {
                            if (checkTrue(dir, Tiles[i, pos]))
                            {
                                HashSet<Index> boat = new HashSet<Index>();
                                while (k > 0)
                                {
                                    if(leng==3)
                                    Tiles[i, pos + k].setOpBoat(triImages[k-1]);
                                    if (leng == 4)
                                        Tiles[i, pos + k].setOpBoat(fourImages[k-1]);
                                    if (leng == 5)
                                        Tiles[i, pos + k].setOpBoat(fiveImages[k-1]);
                                    boat.Add(new Index(i, pos + k));
                                    k--;
                                }
                                boatSet = true;
                                listOfBoats.Add(boat);
                            }
                            else
                            {
                                boatSet = false;
                            }
                        }
                    }
                }

            }
        }

 ```
 Класата **setUpTwoPlayerBoard** наследува од Panel класата и содржи копчиња за променување на насоката, типот на брод и копче за почнување на битката и горенаведените објекти.
 
###3.1.3 Battle
--------
 
 **BattleBoard** класата чува два објекти од класи кои наследуваат од **Panel** наречени **PlayerWarBoard** и **OpponentWarBoard**. Во овие класи префрлуваме податоци за полињата на бродовите и листите со бродови. Во BattleBoard поставуваме настани при кликнување на поле од противникот. Ако има брод, тука поставуваме настан кој што испишува дека сме погодиле и повторно пукаме. Ако е погоден брод се испишува соодветна порака на екран. Ако се промаши се појавува соодветна порака и на ред е противникот.
 
 ```C#
  private void Tile_Click(object sender, EventArgs e)
        {
          
            string s1 = "That was a miss";
            string s2 = "We hit nothing but water, captain";
            string s3 = "No enemy ships on that position";
            int n = r.Next(0, 3);
            switch (n)
            {
                case 0:
                    l.Text = s1;
                    break;
                case 1:
                    l.Text = s2;
                    break;
                case 2:
                    l.Text = s3;
                    break;
            }
            huntIsOn = true;
            opponentBoard.Enabled = false;
        }
        private void ShipTile_Click(object sender, EventArgs e)
        {

            sTilesHit++;
            if (sTilesHit < 18)
            {
                l.Text = "We hit an enemy ship! Fire again, captain!";
                Tile t = (Tile)sender;
                foreach (HashSet<Index> boat in opponentListBoats)
                {
                    Index ind = new Index(t.i, t.j);
                    boat.Remove(ind);
                    if (boat.Count == 0)
                    {
                        l.Text = "Great job, you destroyed an enemy battleship! Fire again!";
                        opponentListBoats.Remove(boat);
                        break;
                    }

                }
            }
            else
            {
                t.Stop();
                l.Text = "You W I N";
                DialogResult res = MessageBox.Show("You are victorious, captain! How about another round?", "V I C T O R Y",
                    MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    victoryCondition = true;
                    newGame.PerformClick();
                }
                else
                    Application.Exit();
            }

        }
 ```
###3.1.3 Главна форма
------------------------
 
 Во главната форма додаваме настани врз копчињата на setUp и battleBoard за почеток на битката и почнување на нова битка кои повикуваат функции наречени startSetup() и startGame(). Овие функции креираат нов објект и го поставуаат во формата во зависност од тоа дали сакаме да почнеме нова игра или да почнеме со битка.
 ````C#
   public void startGame()
        {

            battleBoard = new BattleBoard(setUp.playerBoard, setUp.opponentBoard, setUp.playerBoard.getBoatList(), setUp.opponentBoard.getListOfBoats());
            battleBoard.Location = new System.Drawing.Point(0, 0);
            battleBoard.Height = this.Height;
            battleBoard.Width = this.Width;
            battleBoard.newGame.Click += newGameB_click;
            battleBoard.BackColor = Color.Transparent;
            this.Controls.Add(battleBoard);
            if (setUp != null)
            {
                setUp.Dispose();
            }

        }
    public void startSetup()
        { 
            setUp = new setUpTwoPlayerBoard();
            setUp.Location = new System.Drawing.Point(0, 0);
            setUp.Height = this.Height;
            setUp.Width = this.Width;
            setUp.startGame.Click += startGame_click;
            setUp.BackColor = Color.Transparent;
            this.Controls.Add(setUp);
            if (battleBoard != null)
            {
                battleBoard.Dispose();
            }
        }
 ````
 
### 3.2 Програмирање на противникот
 -----------------------------
 Противникот поради претставување на динамика на играњето пробува да погоди на секои четири секунди ако му е ред да погодува 
 ````C#
 private void tick(object sender, EventArgs e)
        {
            if (huntIsOn)
            {
                if (c == 10)
                {
                    c = 0;
                    AIclick();
                }
                else
                {

                    if (c > 3)
                    {
                        if (c % 2 == 0)
                        {
                            l.Text = "Enemy is thinking.";
                        }
                        else {

                            l.Text = "Enemy is thinking..";
                        }
                    }
                    
                    c++;
                }
            }
        }
 ```
 Ако се повика функцијата AIclick() противникот погодува по случаен избор се додека не погоди.Ако погоди ги зема четирите негови соседи и ги поставува на stack.Почнува да погодува некој од нив.Ако е успешен оди во линијата со која почнал се додека не погоди брод или се потроши stack-от. Ако ги погоди сите бродови се појавува MessageBox со порака дека противникот победил.
 ````C#
  public  void AIclick()
        {
            if (possibleClicks.Count == 0)
            {
                firstTime = true;
                int i = calcI();
                int j = calcJ(i);
                while (playerBoard.WarTile[i, j].clicked)
                {
                    i = calcI();
                    j = calcJ(i);
                }

                playerBoard.WarTile[i, j].click();
                lasti = i;
                lastj = j;
                if (playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    shipHit++;
                    l.Text = "Enemy hit our ship!";
                    addToPossible();
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            possibleClicks.Clear();
                            l.Text = "Enemy destroyed one of out battleships!";
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }
                    }
                }
                else
                {
                    l.Text = "Enemy missed! Fire when ready, captain";
                    opponentBoard.Enabled = true;
                    huntIsOn = false;
                    return;
                }
            }
            else
            {

                Tile t = possibleClicks.Pop();
                while(t.clicked && possibleClicks.Count > 0)
                {
                    t = possibleClicks.Pop();
                }
                playerBoard.WarTile[t.i, t.j].click();
                if (playerBoard.WarTile[t.i, t.j].boatHere)
                {
                    if (firstTime)
                    {
                        possibleClicks.Clear();
                        if (lastj - 1 == t.j)
                            if (lastj + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj + 1]);
                        if (lasti + 1 == t.i)
                            if (lasti - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                        if (lastj + 1 == t.j)
                            if (lastj - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj - 1]);
                        if (lasti - 1 == t.i)
                            if (lasti + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                        
                    }
                    changePossible(t);
                    firstTime = false;      
                    shipHit++;
                    l.Text = "Enemy hit our ship!";
                    lasti = t.i;
                    lastj = t.j;
                    
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            l.Text = "Enemy destroyed one of our battleships";
                            possibleClicks.Clear();
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }
                    }
                    
                }
                else
                {
                   
                    l.Text = "Enemy missed! Fire when ready, captain";
                    opponentBoard.Enabled = true;
                    huntIsOn = false;
                    return;
                }

            }    
            if (shipHit == 18)
            {
                l.Text = "Enemy is victorious";
                DialogResult res = MessageBox.Show("Enemy wins! Do you want a rematch?", "D E F E A T",
                    MessageBoxButtons.YesNo
                    );
                t.Stop();
                if (res == DialogResult.Yes)
                {
                    victoryCondition = true;
                    newGame.PerformClick();
                } else
                {
                    Application.Exit();
                }
            }
          
            
        }
 ```
 


