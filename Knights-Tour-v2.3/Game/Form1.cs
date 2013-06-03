using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Game
{
    public partial class Form1 : Form
    {
        GameBoard gb;
        Highscore hs;
        Sprite seleccionada;
        Sprite anterior;
        Sprite opcion1, opcion2, opcion3, opcion4, opcion5, opcion6, opcion7, opcion8;
        
        public int cont = 0;
        public int segundero;
        protected List<Sprite> marcados = new List<Sprite>();


        public Form1()
        {
            InitializeComponent();
            seleccionada = null;

            gb = new GameBoard();
            hs = new Highscore();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gb.draw_board(this);
        }

        private void Form1_Move(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {


            //Cual celda se selecciono
            if (seleccionada == null)
            {
                seleccionada = gb.get_selected(e.X, e.Y);



                timer1.Start();

                this.label1.Text = seleccionada.i.ToString();
                this.label4.Text = seleccionada.j.ToString();

                seleccionada.actual = Sprite.Type.knight;



                marcados.Add(new Sprite(seleccionada.i, seleccionada.j, Sprite.Type.marked));
                gb.board.Add(marcados);

                gb.draw_board(this);

                cont++;

            }

            else
            {
                anterior = gb.get_selected(e.X, e.Y);

                gb.valido(seleccionada, anterior);
                opcion1 = gb.get_selected(e.X + 2, e.Y + 1);
                opcion2 = gb.get_selected(e.X + 2, e.Y - 1);
                opcion3 = gb.get_selected(e.X - 2, e.Y + 1);
                opcion4 = gb.get_selected(e.X - 2, e.Y - 1);
                opcion5 = gb.get_selected(e.X + 1, e.Y + 2);
                opcion6 = gb.get_selected(e.X + 1, e.Y - 2);
                opcion7 = gb.get_selected(e.X - 1, e.Y + 2);
                opcion8 = gb.get_selected(e.X - 1, e.Y - 2);

                if (gb.valido(seleccionada, anterior) == true && anterior.actual != Sprite.Type.marked)
                {
                    anterior.actual = Sprite.Type.knight;

                    seleccionada.actual = Sprite.Type.marked;

                    this.label1.Text = seleccionada.i.ToString();
                    this.label4.Text = seleccionada.j.ToString();

                    marcados.Add(new Sprite(seleccionada.i, seleccionada.j, Sprite.Type.marked));
                    gb.board.Add(marcados);

                    gb.draw_board(this);

                    seleccionada = anterior;

                    cont++;

                }

                else if (gb.valido(seleccionada, anterior) == false || anterior.actual == Sprite.Type.marked)
                {
                    this.label1.Text = "Invalido";
                    this.label4.Text = "Intente otro";
                }

                if (cont == 64)
                {   
                    timer1.Stop();
                    this.label1.Text = "Felicidades";
                    this.label4.Text = "Ganaste";
                    hs.highscore(cont,segundero);
                }

                if (gb.valido(seleccionada, anterior) == false &&
                    opcion1.actual == Sprite.Type.marked && opcion2.actual == Sprite.Type.marked &&
                    opcion3.actual == Sprite.Type.marked && opcion4.actual == Sprite.Type.marked &&
                    opcion5.actual == Sprite.Type.marked && opcion6.actual == Sprite.Type.marked &&
                    opcion7.actual == Sprite.Type.marked && opcion8.actual == Sprite.Type.marked)
                {
                    timer1.Stop();
                    this.label1.Text = "Fin del Juego";
                    this.label4.Text = "Intente de Nuevo";
                    
                    hs.highscore(cont, segundero);
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundero++;
            this.Text = segundero.ToString();

        }


    }
}