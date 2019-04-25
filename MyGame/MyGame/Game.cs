using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace MyGame
{
    class GameObjectException : Exception
    {
        public GameObjectException(string message)
            : base(message)
        {
        }
    }

    static class Game
    {

        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static void Load()
        {
            try
            {
                int x = 0;
                int y = 400;
                _objs = new BaseObject[30];
                _bullet = new Bullet(new Point(x, y), new Point(5, 0), new Size(8, 3));
                _asteroids = new Asteroid[30];

                if (x <0) throw new GameObjectException("Отрицательная позиция по х");
                if (y > 500) throw new GameObjectException("Неверная позиция по y");
            }

            catch (GameObjectException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }


            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }

        public static BaseObject[] _objs;

        //public static void Load()
        //{
        //    _objs = new BaseObject[60];
        //    //for (int i = 0; i < _objs.Length / 2; i++)
        //    //    _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(8, 8));
        //    for (int i = _objs.Length / 2; i < _objs.Length; i++)
        //        _objs[i] = new Star(new Point(800, (i - 30) * 20), new Point(-i, 0), new Size(4, 4));
        //    for (int i = 0; i < _objs.Length / 2; i++)
        //        _objs[i] = new Star2(new Point(800, i * 20), new Point(-i, 0), new Size(3, 3));

        //}

        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 150 };
            timer.Start();
            timer.Tick += Timer_Tick;

            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;

            if (Width>1000 || Height>1000 || Width<0 || Height<0)
            {
               throw new ArgumentOutOfRangeException("Размер экрана вне диапазона", "Размер экрана должен быть больше нуля и меньше 1000");
            }

            }

            catch (ArgumentOutOfRangeException outOfRange)
            {
                //Console.WriteLine("ArgumentOutOfRangeException!");
                Console.WriteLine("Error: {0}", outOfRange.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();                    
                    a.DrawCollision();
                    _bullet.DrawCollision();
                }
            }
            _bullet.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

    }
}
