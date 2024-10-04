using Aiv.Fast2D;
using OpenTK;
using RescueSpacey.Entities;
using System;

namespace MyFast2DGame
{
    public class Game
    {
        public static Window window;
        private Player player;
        private Enemy enemy1;
        private Enemy enemy2;
        private PowerUp powerUp1;
        private PowerUp powerUp2;

        public Game()
        {
            // Crea una finestra 720x720
            window = new Window(720, 720, "Rescue Spacey");

            // Crea il player
            player = new Player("C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\Player.png", new Vector2(360, 360), 100); // Posizione iniziale al centro

            // Crea il nemico 1 (con 50 punti vita e tipo 1)
            enemy1 = new Enemy(1, "C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\Enemy1.png", new Vector2(100, 100), 50);

            // Crea il nemico 2 (con 70 punti vita e tipo 2)
            enemy2 = new Enemy(2, "C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\Enemy2.png", new Vector2(500, 100), 70);

            // Crea il power-up 1 (tipo 1)
            powerUp1 = new PowerUp(1, "C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\PowerUp1.png", new Vector2(200, 200));

            // Crea il power-up 2 (tipo 2)
            powerUp2 = new PowerUp(2, "C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\PowerUp2.png", new Vector2(400, 400));
        }

        public void Run()
        {
            while (window.IsOpened)
            {
                Update();
                Draw();
                window.Update(); // Aggiorna la finestra e gli input
            }
        }

        private void Update()
        {
            // Aggiorna tutte le entità nella scena
            player.Update();
            enemy1.Update();
            enemy2.Update();
            powerUp1.Update();
            powerUp2.Update();

            // Esempio di danno al nemico 1 (per testare la distruzione)
            if (window.GetKey(KeyCode.Space)) // Premendo spazio, danneggiamo il nemico 1
            {
                enemy1.TakeDamage(10); // Infligge 10 punti di danno al nemico 1
            }

            // Esempio di danno al nemico 2
            if (window.GetKey(KeyCode.B)) // Premendo B, danneggiamo il nemico 2
            {
                enemy2.TakeDamage(15); // Infligge 15 punti di danno al nemico 2
            }
        }

        private void Draw()
        {
            // Disegna tutte le entità
            player.Draw();
            enemy1.Draw();
            enemy2.Draw();
            powerUp1.Draw();
            powerUp2.Draw();
        }

        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}
