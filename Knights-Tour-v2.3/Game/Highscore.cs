using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



public class Highscore
  {

    class Data
    {
        public string casillas;
        public string segundos;
       

        public Data(string c, string s)
        {
            casillas = c;
            segundos = s;
           
        }

    }
    
    public void highscore(int c, int s)
        {

            string segundos = s.ToString();
            string casillas = c.ToString();

            List<Data> puntuacion = new List<Data>();
            
            puntuacion.Add( new Data("# de Casillas","Tiempo"));

            BinaryWriter bw = new BinaryWriter(new FileStream(@"G:\Knights-Tour-v2.3\Game\puntuacion.txt", FileMode.OpenOrCreate, FileAccess.Write));
            foreach (Data p in puntuacion)
            {
                bw.Write("Casillas"+casillas + "|");
                bw.Write("Tiempo"+ segundos);
            }

            bw.Close();


            StreamReader sr = new StreamReader(
                new FileStream(@"G:\Knights-Tour-v2.3\Game\puntuacion.txt", FileMode.OpenOrCreate, FileAccess.Read));

            while(sr.Peek() != -1 )
            {
                string row = sr.ReadLine();
                string[] columnas = row.Split('|');
                puntuacion.Add(new Data(columnas[0], columnas[1]));
 
            }
        
    }
    
}  

