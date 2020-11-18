using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMS_Sea_Wars
{
    public partial class Form1 : Form
    {
        public const int mapSize = 11;
        public int cellsSize = 35;
        public string alphabet = "АБВГДЕЖЗИК";
        public int playerCellsCount = 0;
        public int botCellsCount = 0;

        public int[,] map = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];
        public bool StartPlay = false;
        public bool hit = true;

        public Button[,] myButtons = new Button[mapSize, mapSize];   

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Text = "Морской бой";
            StartPlay = false;
            Start();
        }

        public void Start()
        {
            CreateMap();
        }

        public void CreateMap()
        {
            this.Width = cellsSize * 11 + 80;
            this.Height = cellsSize * 15;
            for (int i = 0; i < mapSize; i++)
            {
                if (i == 0)
                {
                    for (int j = 1; j < mapSize; j++)
                    {
                        Label label = new Label();
                        label.Location = new Point(j * cellsSize, i * cellsSize);
                        label.Size = new Size(cellsSize, cellsSize);
                        label.Text = j.ToString();
                        this.Controls.Add(label);
                    }
                }
                else
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (j == 0)
                        {
                            Label label = new Label();
                            label.Location = new Point(j * cellsSize, i * cellsSize);
                            label.Size = new Size(cellsSize, cellsSize);
                            label.Text = alphabet[i - 1].ToString();
                            this.Controls.Add(label);
                        }
                        else
                        {
                            map[i, j] = 0;

                            Button button = new Button();
                            button.Location = new Point(j * cellsSize, i * cellsSize);
                            button.Size = new Size(cellsSize, cellsSize);
                            button.BackColor = Color.AliceBlue;
                            button.Click += new EventHandler(StartConfig);
                            this.Controls.Add(button);
                            myButtons[i, j] = button;
                    }
                    }
                }
            }

            Button startButton = new Button();
            startButton.Text = "Начать игру";
            startButton.Location = new Point(cellsSize*5, mapSize * cellsSize + cellsSize);
            startButton.Click += new EventHandler(StartGame);
            this.Controls.Add(startButton);
        }

        public bool CheckIfMapIsNotEmpty()
        {
            int isEmpty = 0;
            for (int i = 1; i < mapSize; i++)
            {
                for (int j = 1; j < mapSize; j++)
                {
                    if (map[i, j] != 0)
                        isEmpty += 1;
                }
            }
            if (isEmpty < 16)
                return false;
            else return true;
        }

        public void StartGame(object sender, EventArgs e)
        {
            MessageBox.Show("Морской бой. \nВы играете в морской бой против компьютера. При помощи левой кнопки мыши вы можете расставлять корабли на поле. Вы можете расставлять их так, как вам угодно, но сумма все занятых ячеек должна составлять 16. Компьютер же будет придерживаться классической расстановки.");
            StartPlay = true;
            if (!CheckIfMapIsNotEmpty())
            {
                MessageBox.Show("Расставьте корабли!");
                StartPlay = false;
            }
            else
            {
                this.Width = cellsSize * mapSize * 2 + 300;
                BotGeneration();
                this.Controls.Remove((Button)sender);
            }
        }

        public void StartConfig(object sender, EventArgs e)
        {
            Button ship = sender as Button;

            if (!StartPlay)
            {
                if (map[ship.Location.Y / cellsSize, ship.Location.X / cellsSize] == 0)
                {
                    ship.BackColor = Color.BlueViolet;
                    map[ship.Location.Y / cellsSize, ship.Location.X / cellsSize] = 1;
                    playerCellsCount += 1;
                }
                else
                {
                    ship.BackColor = Color.AliceBlue;
                    map[ship.Location.Y / cellsSize, ship.Location.X / cellsSize] = 0;
                    playerCellsCount -= 1;
                }
            }
        }

        public void PlayerShoot(object sender, EventArgs e)
        {
            Button shipshoot = sender as Button;
            bool playerTurn = Shoot(shipshoot);
            if (!playerTurn)
            {
                BotShoot();
            }
        }

        public bool Shoot(Button ship)
        {
            hit = false;
            if (enemyMap[ship.Location.Y / cellsSize, (ship.Location.X - 450) / cellsSize] != 0)
            {
                hit = true;
                if (enemyMap[ship.Location.Y / cellsSize, (ship.Location.X - 450) / cellsSize] == 1)
                {
                    enemyMap[ship.Location.Y / cellsSize, (ship.Location.X - 450) / cellsSize] = 2;
                    ship.BackColor = Color.Coral;
                    ship.Text = "X";
                    botCellsCount -= 1;
                }
            }
            else
            {
                hit = false;
                enemyMap[ship.Location.Y / cellsSize, (ship.Location.X - 450) / cellsSize] = 2;
                ship.BackColor = Color.MediumOrchid;
            }

            if(botCellsCount <=  0)
            {
                MessageBox.Show("Вы выиграли");
                this.Close();
            }

            return hit;
        }

        public bool BotShoot()
        {
            var rand = new Random();
            int x, y;
            var randDir = new Random();
            int direction = randDir.Next(2, 3);
            choose:
            x = rand.Next(1, 11);
            y = rand.Next(1, 11);

            if (map[y, x] == 2)
            {
                goto choose;
            }

            if (map[y, x] == 1)
            {
                map[y, x] = 2;
                hit = true;
                myButtons[y, x].BackColor = Color.Coral;
                myButtons[y, x].Text = "X";
                playerCellsCount -= 1;
                if (playerCellsCount > 1) 
                { 
                    switch (direction)
                    {
                        case (1):
                        up:
                            while (y != 1)
                            {
                                if (map[y - 1, x] == 1)
                                {
                                    y -= 1;
                                    myButtons[y, x].BackColor = Color.Coral;
                                    myButtons[y, x].Text = "X";
                                    map[y, x] = 2;
                                    playerCellsCount -= 1;
                                }
                                else if (map[y - 1, x] == 0)
                                {
                                    y -= 1;
                                    myButtons[y, x].BackColor = Color.MediumOrchid;
                                    map[y, x] = 2;
                                    hit = false;
                                    break;
                                }
                                else
                                {
                                    if (y != 10)
                                    {
                                        if (map[y + 1, x] == 0)
                                        {
                                            goto down;
                                        }
                                    }

                                    if (x != 10)
                                    {
                                        if (map[y, x + 1] == 0)
                                        {
                                            goto right;
                                        }
                                    }

                                    if (x != 1)
                                    {
                                        if (map[y, x - 1] == 0)
                                        {
                                            goto left;
                                        }
                                    }
                                    goto choose;
                                }
                            }
                            if (y == 1)
                            {
                                goto down;
                            }
                            break;


                        case (2):
                        down:
                            while (y != 10)
                            {
                                if (map[y + 1, x] == 1)
                                {
                                    y += 1;
                                    myButtons[y, x].BackColor = Color.Coral;
                                    myButtons[y, x].Text = "X";
                                    map[y, x] = 2;
                                    playerCellsCount -= 1;
                                }
                                else if (map[y + 1, x] == 0)
                                {
                                    y += 1;
                                    myButtons[y, x].BackColor = Color.MediumOrchid;
                                    map[y, x] = 2;
                                    hit = false;
                                    break;
                                }
                                else
                                {
                                    if (y != 1)
                                    {
                                        if (map[y - 1, x] == 0)
                                        {
                                            goto up;
                                        }
                                    }

                                    if (x != 10)
                                    {
                                        if (map[y, x + 1] == 0)
                                        {
                                            goto right;
                                        }
                                    }

                                    if (x != 1)
                                    {
                                        if (map[y, x - 1] == 0)
                                        {
                                            goto left;
                                        }
                                    }

                                    goto choose;
                                }
                            }
                            if (y == 10)
                            {
                                goto up;
                            }
                            break;

                        case (3):
                        right:
                            while (x != 10)
                            {
                                if (map[y, x + 1] == 1)
                                {
                                    x += 1;
                                    myButtons[y, x].BackColor = Color.Coral;
                                    myButtons[y, x].Text = "X";
                                    map[y, x] = 2;
                                    playerCellsCount -= 1;
                                }
                                else if (map[y, x + 1] == 0)
                                {
                                    x += 1;
                                    myButtons[y, x].BackColor = Color.MediumOrchid;
                                    map[y, x] = 2;
                                    hit = false;
                                    break;
                                }
                                else
                                {
                                    if (x != 1)
                                    {
                                        if (map[y, x - 1] == 0)
                                        {
                                            goto left;
                                        }
                                    }

                                    if (y != 1)
                                    {
                                        if (map[y - 1, x] == 0)
                                        {
                                            goto up;
                                        }
                                    }

                                    if (y != 10)
                                    {
                                        if (map[y + 1, x] == 0)
                                        {
                                            goto down;
                                        }
                                    }

                                    goto choose;
                                }
                            }
                            if (x == 10)
                            {
                                goto left;
                            }
                            break;

                        case (4):
                        left:
                            while (x != 1)
                            {
                                if (map[y, x - 1] == 1)
                                {
                                    x -= 1;
                                    myButtons[y, x].BackColor = Color.Coral;
                                    myButtons[y, x].Text = "X";
                                    map[y, x] = 2;
                                    playerCellsCount -= 1;
                                }
                                else if (map[y, x - 1] == 0)
                                {
                                    x -= 1;
                                    myButtons[y, x].BackColor = Color.MediumOrchid;
                                    map[y, x] = 2;
                                    hit = false;
                                    break;
                                }
                                else
                                {
                                    if (x != 10)
                                    {
                                        if (map[y, x + 1] == 0)
                                        {
                                            goto right;
                                        }
                                    }

                                    if (y != 1)
                                    {
                                        if (map[y - 1, x] == 0)
                                        {
                                            goto up;
                                        }
                                    }

                                    if (y != 10)
                                    {
                                        if (map[y + 1, x] == 0)
                                        {
                                            goto down;
                                        }
                                    }

                                    goto choose;
                                }
                            }
                            if (x == 1)
                            {
                                goto right;
                            }
                            break;
                    }
                }
            }
                
            
            if(map[y, x] == 0)
            {
                hit = false;
                myButtons[y, x].BackColor = Color.MediumOrchid;
                map[y, x] = 2;
            }   
            
            if (playerCellsCount <= 0)
            {
                MessageBox.Show("Бот победил");
                this.Close();
            }

            return hit;
        }

        public void BotGeneration()
        {
                var rand = new Random();
                int x, y;
                int direction = 0;
                int countShips = 0;
                int shipLenght = 4;

                for (int i = 1; i < mapSize; i++)
                {
                    for (int j = 1; j < mapSize; j++)
                    {
                        enemyMap[i, j] = 0;
                    }
                }

                while (countShips < 10)
                {
                    x = rand.Next(1, 11);
                    y = rand.Next(1, 11);

                    int tempX = x;
                    int tempY = y;

                    direction = rand.Next(1, 5);

                    bool setShip = true;

                    //проверка
                    for (int i = 0; i < shipLenght; i++)
                    {
                        if (tempX < 1 || tempY < 1 || tempX >= 11 || tempY >= 11)
                        {
                            setShip = false;
                            break;
                        }

                        if (enemyMap[tempY, tempX] == 1)
                        {
                            setShip = false;
                            break;
                        }

                        if (tempY == 1 && tempX != 10)
                        {
                            if (
                                 enemyMap[tempY, tempX + 1] == 1 ||
                                 enemyMap[tempY + 1, tempX] == 1 ||
                                 enemyMap[tempY + 1, tempX + 1] == 1 ||
                                 enemyMap[tempY, tempX - 1] == 1 ||
                                 enemyMap[tempY + 1, tempX - 1] == 1
                                 )
                            {
                                setShip = false;
                                break;
                            }
                        }

                        if (tempY == 10 && tempX != 10)
                        {
                            if (
                                 enemyMap[tempY - 1, tempX] == 1 ||
                                 enemyMap[tempY - 1, tempX - 1] == 1 ||
                                 enemyMap[tempY - 1, tempX + 1] == 1 ||
                                 enemyMap[tempY, tempX - 1] == 1 ||
                                 enemyMap[tempY, tempX + 1] == 1
                                 )
                            {
                                setShip = false;
                                break;
                            }
                        }

                        if ((tempX == 1 || tempX == 10) && tempY != 10)
                        {
                            if (enemyMap[tempY - 1, tempX] == 1)
                            {
                                setShip = false;
                                break;
                            }
                        }

                        if (tempX == 1 && tempY != 10)
                        {
                            if (
                                enemyMap[tempY + 1, tempX] == 1 ||
                                enemyMap[tempY + 1, tempX + 1] == 1 ||
                                enemyMap[tempY, tempX + 1] == 1 ||
                                enemyMap[tempY - 1, tempX + 1] == 1 ||
                                enemyMap[tempY - 1, tempX] == 1
                               )
                            {
                                setShip = false;
                                break;
                            }
                        }

                        if (tempX == 10 && tempY != 10)
                        {
                            if (
                                enemyMap[tempY + 1, tempX] == 1 ||
                                enemyMap[tempY + 1, tempX - 1] == 1 ||
                                enemyMap[tempY, tempX - 1] == 1 ||
                                enemyMap[tempY - 1, tempX - 1] == 1 ||
                                enemyMap[tempY - 1, tempX] == 1
                               )
                            {
                                setShip = false;
                                break;
                            }
                        }

                        if (tempX > 1 && tempY > 1 && tempX < 10 && tempY < 10)
                        {
                            if (
                                 enemyMap[tempY, tempX + 1] == 1 ||
                                 enemyMap[tempY, tempX - 1] == 1 ||
                                 enemyMap[tempY + 1, tempX] == 1 ||
                                 enemyMap[tempY + 1, tempX + 1] == 1 ||
                                 enemyMap[tempY + 1, tempX - 1] == 1 ||
                                 enemyMap[tempY - 1, tempX] == 1 ||
                                 enemyMap[tempY - 1, tempX + 1] == 1 ||
                                 enemyMap[tempY - 1, tempX - 1] == 1
                                 )
                            {
                                setShip = false;
                                break;
                            }
                        }

                        switch (direction)
                        {
                            case (1):
                                tempX += 1;
                                break;
                            case (2):
                                tempX -= 1;
                                break;
                            case (3):
                                tempY += 1;
                                break;
                            case (4):
                                tempY -= 1;
                                break;
                        }

                    }


                    //расстановка
                    if (setShip)
                    {
                        for (int i = 0; i < shipLenght; i++)
                        {
                            switch (direction)
                            {
                                case (1):
                                    enemyMap[y, x] = 1;
                                    x += 1;
                                    break;
                                case (2):
                                    enemyMap[y, x] = 1;
                                    x -= 1;
                                    break;
                                case (3):
                                    enemyMap[y, x] = 1;
                                    y += 1;
                                    break;
                                case (4):
                                    enemyMap[y, x] = 1;
                                    y -= 1;
                                    break;
                            }
                        }
                        botCellsCount += shipLenght;
                        if (shipLenght == 1)
                        {
                            countShips += 1;
                        }
                        else if (shipLenght == 2)
                        {
                            if (countShips == 5)
                            {
                                shipLenght -= 1;
                                countShips += 1;
                            }
                            else
                            {
                                countShips += 1;
                            }
                        }
                        else if (shipLenght == 3)
                        {
                            if (countShips == 2)
                            {
                                shipLenght -= 1;
                                countShips += 1;
                            }
                            else
                            {
                                countShips += 1;
                            }
                        }
                        else if (shipLenght == 4)
                        {
                            countShips += 1;
                            shipLenght -= 1;
                        }
                    }
                }

                for (int i = 0; i < mapSize; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j < mapSize; j++)
                        {
                            Label label = new Label();
                            label.Location = new Point(450 + j * cellsSize, i * cellsSize);
                            label.Size = new Size(cellsSize, cellsSize);
                            label.Text = j.ToString();
                            this.Controls.Add(label);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < mapSize; j++)
                        {
                            if (j == 0)
                            {
                                Label label = new Label();
                                label.Location = new Point(450 + j * cellsSize, i * cellsSize);
                                label.Size = new Size(cellsSize, cellsSize);
                                label.Text = alphabet[i - 1].ToString();
                                this.Controls.Add(label);
                            }
                            else
                            {
                                Button button = new Button();
                                button.Location = new Point(450 + j * cellsSize, i * cellsSize);
                                button.Size = new Size(cellsSize, cellsSize);
                                button.Click += new EventHandler(PlayerShoot);
                                button.BackColor = Color.AliceBlue;
                                this.Controls.Add(button);
                            }
                        }
                    }
                } 
            }
        }
    } 

