using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Drawing.Text;


namespace Slutprojekt;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D pixel;
    Players p = new Players(385,420,30,20);
    Bullets b = new Bullets(800);
    Retry r = new Retry(265);
    List<Enemies> Enemies = new List<Enemies>();
    DifficultyBox Blue = new DifficultyBox(285);
    DifficultyBox Green = new DifficultyBox(250);
    DifficultyBox Red = new DifficultyBox(320);
    Rectangle Difficulty = new Rectangle(700, 280, 50, 35);
    SoundEffect pewSFX;
    SoundEffect scoreSFX;
    List<Lives> Life = new List<Lives>();
    int score = 0;
    int highScore = 0;
    int difficulty = 2;
    SpriteFont scoreFont;
    SpriteFont gameOverFont;
    SpriteFont HscoreFont;
    SpriteFont retry;
    Random rnd = new Random();
    double elapsedTime;
    double nextInterval;
    bool gameOver = false;

    private double GetRandomInterval(){
        return rnd.NextDouble()*(1.8-0.4)+0.4;
    }
    public Game1()
    {
	    _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        nextInterval = GetRandomInterval();
        Life.Add(new Lives(770));
        Life.Add(new Lives(750));
        Life.Add(new Lives(730));
        Enemies.Add(new Enemies(480));
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        pixel = new Texture2D(GraphicsDevice,1,1);
        pixel.SetData(new Color[]{Color.White});
        pewSFX = Content.Load<SoundEffect>("pew1");
        scoreFont = Content.Load<SpriteFont>("Score");
        gameOverFont = Content.Load<SpriteFont>("GameOver");
        HscoreFont = Content.Load<SpriteFont>("Highscore");
        retry = Content.Load<SpriteFont>("Retry");
    }
    KeyboardState kState1, kState2 = Keyboard.GetState();
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if(Life.Count==0)
            gameOver = true;
        kState1 = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if(gameOver == false){
            if(kState1.IsKeyDown(Keys.Left) && p.Player.X > 0)
                p.MoveLeft();
            if(kState1.IsKeyDown(Keys.Right) && p.Player.X < 770)
                p.MoveRight();
            if(kState1.IsKeyDown(Keys.Space) && b.Bullet.Y < -10){
                b.Shoot(p.Player.X);
                pewSFX.Play();
            }

            kState2 = kState1;
            
            b.MoveUp();

            if(difficulty == 1){
                foreach(Enemies e in Enemies){
                    e.MoveGreen();
                }
            }
            if(difficulty == 2){
                foreach(Enemies e in Enemies){
                    e.MoveBlue();
                }
            }
            if(difficulty == 3){
                foreach(Enemies e in Enemies){
                    e.MoveRed();
                }
            }

            for( int i = Enemies.Count; i >= 1; i--){
                if(Enemies[i-1].Enemy.Intersects(b.Bullet)){
                    Enemies.RemoveAt(i-1);
                    score++;
                }
            }

            for(int e = Enemies.Count-1; e >= 1; e--){
                if(Enemies[e].Enemy.X > 800){
                    Life.RemoveAt(Life.Count-1);
                    Enemies.RemoveAt(e);
                }
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsedTime >= nextInterval){
                Enemies.Add(new Enemies(rnd.Next(90, 180)));
                elapsedTime = 0;
                nextInterval = GetRandomInterval();
            }
        }


        if(gameOver == true){
            Enemies.Clear();
            if(mouse.X > 705 && mouse.X < 740 && mouse.Y > 250 && mouse.Y < 275){
                if(mouse.LeftButton == ButtonState.Pressed){
                    difficulty = 1;
                    Difficulty.Y = 245;
                }
            }
            if(mouse.X > 705 && mouse.X < 740 && mouse.Y > 285 && mouse.Y < 310){
                if(mouse.LeftButton == ButtonState.Pressed){
                    difficulty = 2;
                    Difficulty.Y = 280;
                }
            }
            if(mouse.X > 705 && mouse.X < 740 && mouse.Y > 320 && mouse.Y < 345){
                if(mouse.LeftButton == ButtonState.Pressed){
                    difficulty = 3;
                    Difficulty.Y = 315;
                }
            }
            if(mouse.X > 265 && mouse.X < 535 && mouse.Y > 300 && mouse.Y < 450){
                if(mouse.LeftButton == ButtonState.Pressed){
                    Life.Add(new Lives(770));
                    Life.Add(new Lives(750));
                    Life.Add(new Lives(730));
                    Enemies.Add(new Enemies(480));
                    score = 0;
                    p.Reset();
                    gameOver = false;
                }
            }
        }

        if(score > highScore)
                highScore = score;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        if(gameOver == false){
            _spriteBatch.Draw(pixel, p.Player, Color.White);
            _spriteBatch.Draw(pixel, b.Bullet, Color.White);
            _spriteBatch.DrawString(scoreFont, "Score:" + score ,new Vector2(10,10), Color.White);
            _spriteBatch.DrawString(HscoreFont, "Highscore:" + highScore ,new Vector2(10,40), Color.White);
            foreach(Enemies element in Enemies)
                _spriteBatch.Draw(pixel, element.Enemy, Color.OrangeRed);
            foreach(Lives element in Life)
                _spriteBatch.Draw(pixel, element.Life, Color.Red);
        }
        if(gameOver == true){
            _spriteBatch.DrawString(gameOverFont,"GAME OVER", new Vector2(10,10),Color.OrangeRed);
            _spriteBatch.DrawString(scoreFont,"Score:" + score, new Vector2(350,200),Color.White);
            _spriteBatch.DrawString(HscoreFont,"Highscore:" + highScore, new Vector2(330,230),Color.White);
            _spriteBatch.DrawString(scoreFont,"Difficulty:", new Vector2(600,282),Color.White);
            _spriteBatch.Draw(pixel, r.Border, Color.White);
            _spriteBatch.Draw(pixel, r.Middle, Color.Black);
            _spriteBatch.DrawString(retry, "Retry", new Vector2(295, 320), Color.White);
            _spriteBatch.Draw(pixel, Difficulty, Color.White);
            _spriteBatch.Draw(pixel, Green.Box, Color.Green);
            _spriteBatch.Draw(pixel, Blue.Box, Color.Blue);
            _spriteBatch.Draw(pixel, Red.Box, Color.Red);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}