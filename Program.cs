namespace Tarea3
{
    class Game
    {
        private static List<Enemy> enemies;

        public static void RunGame(int enemyCount)
        {
            enemies = new List<Enemy>();
            for (int i = 0; i < enemyCount; i++)
            {
                enemies.Add(new MeleeEnemy(50, 10, 1));
                enemies.Add(new RangeEnemy(50, 20, 3, 2));
            }

            Player player = Player.CreatePlayer();

            while (true)
            {
                if (player.GetHP() <= 0)
                {
                    break;
                }
                
                    if (enemies.Count > 0)
                    {

                        Console.WriteLine("Player Turn");
                        Game.PlayerAttack(enemies, player.GetDmg());
                        Console.WriteLine("Enemy Turn");
                        Game.EnemyAttack(enemies, player);
                    }

                    else
                    {
                        Console.WriteLine("You won. Good job");
                        break;
                    }
                             
            }
        }

        private static void PlayerAttack(List<Enemy> enemies, int damage)
        {
            int enemyid;

            Console.WriteLine("Select enemy ID to attack:");
            enemyid = int.Parse(Console.ReadLine());

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].id == enemyid)
                {
                    enemies[i].EnemyTakeDamage(damage);
                    Console.WriteLine("Enemy " + enemies[i].id + " has taken " + damage);
                    if (!enemies[i].IsAlive())
                    {
                        Console.WriteLine("Enemy " + enemies[i].id + " defeated");
                        enemies.RemoveAt(i);
                    }
                    break;
                }
            }
        }

        private static void EnemyAttack(List<Enemy> enemies, Player player)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine("Enemy " + enemies[i].id + " attacks the player");
                player.TakeDamage(enemies[i].EnemyGetDmg());
                Console.WriteLine("Player recieves " + enemies[i].EnemyGetDmg() + "damage");

                if (player.GetHP() <= 0)
                {
                    Console.WriteLine("You lost, try again");
                    break;
                }
            }
        }

        public static void Main(string[] args)
        {
            RunGame(1);
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    class Player
    {
        private int hp;
        private int dmg;

        public Player(int hp, int dmg)
        {
            this.hp = hp;
            this.dmg = dmg;
        }

        public static Player CreatePlayer()
        {
            int hp = 0;
            int dmg = 0;

            do
            {
                Console.WriteLine("Insert player HP value (must not excede 100):");
                hp = int.Parse(Console.ReadLine());

                if (hp > 100)
                {
                    Console.WriteLine("Player HP may not excede 100");
                    continue;
                }

                Console.WriteLine("Insert player Damage value (must not excede 100):");
                dmg = int.Parse(Console.ReadLine());

                if (dmg > 100)
                {
                    Console.WriteLine("Player Damage may not excede 100");
                    continue;
                }

            } while (hp > 100 && dmg > 100);

            return new Player(hp, dmg);
        }

        public int GetHP()
        {
            return hp;
        }

        public int GetDmg()
        {
            return dmg;
        }

        public void TakeDamage(int dmgtaken)
        {
            hp -= dmgtaken;
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    class Enemy
    {
        public int enemyHP;
        public int dmg;
        public int id;

        public Enemy(int startHP, int dmg, int id)
        {
            this.enemyHP = startHP;
            this.dmg = dmg;
            this.id = id;
        }

        public void EnemyTakeDamage(int damage)
        {
            enemyHP -= damage;
            if (enemyHP < 0)
            {
                enemyHP = 0;
            }
        }

        public virtual int EnemyGetDmg()
        {
            return dmg;
        }

        public bool IsAlive()
        {
            return enemyHP > 0;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------

    class MeleeEnemy : Enemy
    {
        public MeleeEnemy(int enemyHP, int dmg, int id) : base(enemyHP, dmg, id)
        {

        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------

    class RangeEnemy : Enemy
    {
        private int ammo;

        public RangeEnemy(int enemyHP, int rangedmg, int ammo, int id) : base(enemyHP, rangedmg, id)
        {
            this.ammo = ammo;
        }

        public override int EnemyGetDmg()
        {
            if (ammo > 0)
            {
                ammo--;
                return dmg;
            }
            else
            {
                Console.WriteLine("Ranged enemy skips turn");
                return 0;
            }
        }
    }
}
