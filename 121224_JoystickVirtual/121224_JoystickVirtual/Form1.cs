using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace _121224_JoystickVirtual
{
    public partial class Form1 : Form
    {
        private Panel joystick;
        private Point center;
        private int maxDistance = 50;
        private bool usarWASD = true; // Variable para alternar entre modos (WASD/Flechas)
        private Timer moveTimer; // Temporizador para controlar la frecuencia de movimiento
        private DateTime lastMoveTime; // Marca de tiempo para el último movimiento
        private bool isMoving = false; // Para verificar si el joystick está siendo movido

        public Form1()
        {
            InitializeComponent();
            InicializarUI();

            // Inicializar el temporizador
            moveTimer = new Timer();
            moveTimer.Interval = 10; // Comprueba cada N ms la posición
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start(); // Empezamos el temporizador
            lastMoveTime = DateTime.Now;
        }
        /*
         * Funciona chungo xDDD
         * 
        // Sobreescribir el evento Paint para dibujar el fondo personalizado
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Crear un objeto Graphics para dibujar en el formulario
            Graphics g = e.Graphics;

            // Crear un pincel para el fondo azul claro
            using (Brush blueBrush = new SolidBrush(Color.LightBlue))
            {
                // Dibujar el fondo azul claro para todo el formulario
                g.FillRectangle(blueBrush, this.ClientRectangle);
            }

            // Crear un pincel para el círculo negro en el centro
            using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                // Dibujar un círculo negro con radio 50 px en el centro
                int centerX = this.ClientSize.Width / 2;
                int centerY = this.ClientSize.Height / 2;
                int radius = 50;
                g.FillEllipse(blackBrush, centerX - radius, centerY - radius, radius * 2, radius * 2);
            }
        }
        */
        private void InicializarUI()
        {
            // Tamaño y configuración del formulario
            this.Text = "Joystick Virtual";
            this.Size = new Size(400, 400);
           this.BackColor = Color.LightBlue; // Fondo azul claro

            // Panel del joystick
            joystick = new Panel
            {
                Size = new Size(50, 50), // Tamaño del mango del joystick
                BackColor = Color.Gray, // Color gris medio
                Location = new Point(this.ClientSize.Width / 2 - 25, this.ClientSize.Height / 2 - 25),
                Cursor = Cursors.Hand // Cambia el cursor cuando se mueve el joystick
            };

            // Crear la región circular (círculo) para el joystick
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, 50, 50); // Tamaño del mango del joystick
            Region region = new Region(path);
            joystick.Region = region; // Aplicar la región circular al panel

            this.Controls.Add(joystick);

            center = joystick.Location;

            // Botón para alternar entre modos (WASD/Flechas)
            Button toggleModeButton = new Button
            {
                Text = "Alternar Modo (WASD/Flechas)",
                Location = new Point(100, 300),
                Size = new Size(200, 30)
            };

            toggleModeButton.Click += (s, e) =>
            {
                usarWASD = !usarWASD;
                string modo = usarWASD ? "WASD" : "Flechas";
                MessageBox.Show($"Modo cambiado a: {modo}", "Modo Actual");
            };

            this.Controls.Add(toggleModeButton);

            // Configurar el evento MouseMove del formulario para detectar el movimiento del ratón
            this.MouseMove += Form1_MouseMove;

            // Configurar el evento MouseDown para detectar clic derecho
            this.MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Si se hace clic derecho, el joystick vuelve a la posición inicial
            if (e.Button == MouseButtons.Right)
            {
                joystick.Location = new Point(this.ClientSize.Width / 2 - 25, this.ClientSize.Height / 2 - 25);
                SendKeys.Send("{ESC}"); // Enviar una tecla de escape al hacer clic derecho
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Solo procesar el movimiento si ha pasado suficiente tiempo desde el último movimiento
            if ((DateTime.Now - lastMoveTime).TotalMilliseconds > 10)
            {
                lastMoveTime = DateTime.Now;

                // Calcular la nueva posición del joystick
                Point newPos = new Point(e.X - joystick.Width / 2, e.Y - joystick.Height / 2);
                int deltaX = newPos.X - center.X;
                int deltaY = newPos.Y - center.Y;

                // Limitar el movimiento para que no se desplace fuera del radio máximo
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                if (distance > maxDistance)
                {
                    double angle = Math.Atan2(deltaY, deltaX);
                    newPos.X = center.X + (int)(maxDistance * Math.Cos(angle));
                    newPos.Y = center.Y + (int)(maxDistance * Math.Sin(angle));
                }

                // Asegurarse de que el joystick se mantenga dentro de los límites de la ventana
                newPos.X = Math.Max(0, Math.Min(this.ClientSize.Width - joystick.Width, newPos.X));
                newPos.Y = Math.Max(0, Math.Min(this.ClientSize.Height - joystick.Height, newPos.Y));

                joystick.Location = newPos;

                // Detectar la dirección y enviar teclas
                DetectDirection(deltaX, deltaY);
            }
        }

        private void DetectDirection(int deltaX, int deltaY)
        {
            // Usar las teclas correspondientes según el movimiento del joystick
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                if (deltaX > 0)
                {
                    SendKey(usarWASD ? "d" : "{RIGHT}");
                }
                else if (deltaX < 0)
                {
                    SendKey(usarWASD ? "a" : "{LEFT}");
                }
            }
            else
            {
                if (deltaY > 0)
                {
                    SendKey(usarWASD ? "s" : "{DOWN}");
                }
                else if (deltaY < 0)
                {
                    SendKey(usarWASD ? "w" : "{UP}");
                }
            }
        }

        private void SendKey(string key)
        {
            SendKeys.Send(key); // Enviar la tecla simulada
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Cualquier código adicional en el inicio
        }
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            // Lógica adicional si es necesario (se puede usar si se quiere controlar la velocidad de la tecla presionada)
        }
    }
}
