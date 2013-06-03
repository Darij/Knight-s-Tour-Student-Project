using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Game
{
    public class GameBoard
    {
        public List<List<Sprite>> board;
        public enum State { puting, updating, gameover }
        public GameBoard.State current_state;

        private Sprite actual;

        public GameBoard()
        {
            board = new List<List<Sprite>>();
            initialize_board();
            current_state = State.puting;
        }

        public void initialize_board()
        {

            int contador = 2;

            try
            {

                for (int i = 0; i < 8; i++)
                {
                    List<Sprite> sublist = new List<Sprite>();

                    contador++;

                    for (int j = 0; j < 8; j++)
                    {
                        if (contador % 2 == 0)
                        {
                            sublist.Add(new Sprite(i, j, Sprite.Type.white));
                        }

                        if (contador % 2 != 0)
                        {
                            sublist.Add(new Sprite(i, j, Sprite.Type.black));
                        }
                        contador++;

                    }

                    board.Add(sublist);
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        public void draw_board(Form f)
        {
            foreach (var sublist in board)
            {

                foreach (Sprite value in sublist)
                {
                    value.Draw(f);
                }

            }


            foreach (var marcados in board)
            {
                foreach (Sprite value in marcados)
                {
                    value.Draw(f);
                }

            }

        }

        public Sprite get_selected(int x, int y)
        {
            foreach (var sublist in board)
            {
                foreach (Sprite value in sublist)
                {
                    if (value.overlaps(x, y))
                    {
                        this.actual = (Sprite)value;
                        return value;
                    }
                }

            }

            return null;
        }


        public bool valido(Sprite a, Sprite p)//actual y previa
        {
          

            if (p.i - 1 == a.i && p.j - 2 == a.j) { return true; }

            if (p.i - 1 == a.i && p.j + 2 == a.j) { return true; }

            if (p.i + 1 == a.i && p.j - 2 == a.j) { return true; }

            if (p.i + 1 == a.i && p.j + 2 == a.j) { return true; }

            if (p.i - 2 == a.i && p.j - 1 == a.j) { return true; }

            if (p.i - 2 == a.i && p.j + 1 == a.j) { return true; }

            if (p.i + 2 == a.i && p.j - 1 == a.j) { return true; }

            if (p.i + 2 == a.i && p.j + 1 == a.j) { return true; }

            else 
                return false; 

        }
     
    }

}

public abstract class GameObject
{
    //   public abstract void Update( );
    public abstract void Draw(Form F);
}

public class Sprite : GameObject
{
    public enum Type { knight, black, white, marked, opcion }
    public int size = 64;
    public int i, j;
    public int x;
    public int y;
   
    public Sprite.Type actual;
    public List<Bitmap> _frames = new List<Bitmap>();

    public Sprite(int i, int j, Sprite.Type actual)
    {
        
        this.actual = actual;
        this.i = i;
        this.j = j;
        x = i * size;
        y = j * size;
        _frames.Add(new Bitmap(@"G:\Knights-Tour-v2.3\Game\images\horse.png"));
        _frames.Add(new Bitmap(@"G:\Knights-Tour-v2.3\Game\images\marbleb.png"));
        _frames.Add(new Bitmap(@"G:\Knights-Tour-v2.3\Game\images\marblew.png"));
        _frames.Add(new Bitmap(@"G:\Knights-Tour-v2.3\Game\images\marked.png"));
        _frames.Add(new Bitmap(@"G:\Knights-Tour-v2.3\Game\images\opcion.png"));
    }

   

    public override void Draw(Form f)
    {
        Graphics graphics = f.CreateGraphics();
        try
        {
            graphics.DrawImage(_frames[(int)this.actual], size * i, size * j, size, size);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.Source);
        }
    }

    public bool overlaps(int x, int y)
    {
        return this.x < x && this.x + size > x && this.y < y && this.y + size > y;

    }

}


