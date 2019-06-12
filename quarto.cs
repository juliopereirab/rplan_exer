using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public class Game{

        public class Chip
        {
            public bool isSquare; 
            public bool isHollow;
            public bool isSmall;
            public bool isWhite;

            public Chip(){}

            public Chip(bool square, bool hollow, bool small, bool white){
                this.isSquare = square; 
                this.isHollow = hollow;
                this.isSmall = small;
                this.isWhite = white;
            }

            public string describeChip(){
                string shape, fill, size, color;

                if(isSquare){shape = "sq"; } else {shape="ro";} 
                if(isHollow){fill = "ho"; } else {fill="fi";} 
                if(isSmall){size = "sm"; } else {size="ta";} 
                if(isWhite){color = "wh"; } else {color="br";} 

                return $"{shape}-{fill}-{size}-{color}";

            }
        }

        public static Dictionary<string, Chip> generateSet(){
            // var listOfChips = new List<Chip>();

            Dictionary<string, Chip> chipDictionary = new Dictionary<string, Chip>();

            bool[] shape = {true, false};
            bool[] fill = {true, false};
            bool[] size = {true, false};
            bool[] color = {true, false};

            int i = 1;

            foreach(bool b1 in shape){
                foreach(bool b2 in fill){
                    foreach(bool b3 in size){
                        foreach(bool b4 in color){
                            Chip c = new Chip(b1, b2, b3, b4);
                            chipDictionary.Add(i.ToString(), c);
                            i++;
                        }  
                    }
                }            
            }

            return chipDictionary;
        }


        public class Board
        {
            public Chip[] brd = new Chip[16];

            public void displayBoard(bool possibleMoves = false)
            {

                List<string> moves = new List<string>();
                int i = 0;

                foreach(Chip c in brd){
                    if(c == null){
                        if(possibleMoves == true){
                            moves.Add(i.ToString());
                        } else {
                            moves.Add(" ");
                        }
                    } else {
                        moves.Add(c.describeChip());
                    }
                    i++;
                }

                string t1 = " {0} || {1} || {2} || {3} \n";
                string t2 = " {4} || {5} || {6} || {7} \n";
                string t3 = " {8} || {9} || {10} || {11} \n";
                string t4 = " {12} || {13} || {14} || {15} \n";
                string r = "==================\n";
                string table = t1+r+t2+r+t3+r+t4;
                Console.WriteLine(table, moves[0], moves[1], moves[2], moves[3], moves[4], moves[5], moves[6], moves[7], moves[8], moves[9], moves[10], moves[11], moves[12], moves[13], moves[14], moves[15]);

            }
        }


        static public void play(Dictionary<string, Chip> diction, Board b){
            do
            {
                bool win = checkWin(b.brd);
                if(win == true){
                    break;
                }
                b.displayBoard();

                foreach(KeyValuePair<string, Chip> pair in diction){
                    Console.WriteLine($"{pair.Key} - {pair.Value.describeChip()}");
                }
                Console.Write("Choose opponent's Chip by typing the corresponding number: ");
                var read = Console.ReadLine();

                Chip selectedChip = diction[read];
                diction.Remove(read);

                b.displayBoard(possibleMoves : true);
                Console.Write("Choose where to place the Chip by typing the number of the square: ");            
                var read2 = Console.ReadLine(); 

                b.brd[int.Parse(read2)] = selectedChip;

            } while(diction.Count() > 0);
        }


        static public bool checkWin(Chip[] status){

            bool Win = false;

            var combinations = new int[][]{
                new int[]{0, 1, 2, 3},
                new int[]{4, 5, 6, 7},
                new int[]{8, 9, 10, 11},
                new int[]{12, 13, 14, 15},
                new int[]{0, 4, 8, 12},
                new int[]{1, 5, 9, 13},
                new int[]{2, 6, 10, 14},
                new int[]{3, 7, 11, 15},
                new int[]{0, 5, 10, 15},
                new int[]{3, 6, 9, 12}
            };

            foreach(int[] comb in combinations){
                List<bool> checkShape = new List<bool>();
                List<bool> checkFill = new List<bool>();
                List<bool> checkSize = new List<bool>();
                List<bool> checkColor = new List<bool>();
                foreach(int index in comb){
                    Chip c = status[index];
                    if(c != null){
                        checkShape.Add(c.isSquare);
                        checkFill.Add(c.isHollow);
                        checkSize.Add(c.isSmall);
                        checkColor.Add(c.isWhite);
                    }
                }
                if(checkShape.Count() == 4){
                    var dimensions = new List<List<bool>>(){checkShape, checkFill, checkSize, checkColor};

                    foreach(List<bool> dim in dimensions){
                        if(Win != true){
                            bool op1, op2;
                            op1 = dim.All(b => b == true );                    
                            op2 = dim.All(b => b == false );
                            if(op1==true || op2== true){
                                Win = true;
                            }
                        }
                    }
                }

            }

            return Win;
        }

        //si el método va a ser utilizado cuando se haya instanciado la clase
        //entonces no se debe utilizar static (porque diria que no es accesible)
        public void Init(){

            // Chip c = new Chip();
            // Chip[] brd = new Chip[16];
            // Console.WriteLine(brd[0]==null);

            var d = generateSet();

            Board b = new Board();

            play(d, b);
            b.displayBoard();

            if(checkWin(b.brd)){
                Console.WriteLine("Quarto!");            
                Console.Write("the last player that played a Chip may type his/her name: ");
                string winner = Console.ReadLine();
                Console.WriteLine("{0} WINS!!!", winner);
            } else {
                Console.WriteLine("no winner, sorry");            
            }
        }

    }



    static void Main(string[] args){

        Game game = new Game();

        game.Init();

    }

}





//NOTAS

//modificar endless loop para que pregunte y descarte fichas por turno
    //incluso.. no tiene que ser endless realmente
//modificar board para que tenga dos displays: uno de seleccion de chip y otro de selección de cuadro

//completar función de play, y luego ver si se puede wrap up



//se genera el tablero, que es una lista de 2 dimensiones que tenga como tipo Move
//se genera una función que retorna todas las fichas posibles
    //todavia tengo que decidir cuál va a ser el formato o tipo de las fichas posibles... un diccionario con el tipo string y Move?

//se genera un loop de juego, que se mantiene si tras entrar un input todavía no se gana el juego
    //el loop alberga la ejecución de una función verificación de victoria (que contempla todas las posibles combinaciones en vertical, horizontal o diagonal)
    //vamos a omitir la escogencia de ficha... pero vamos a dejarla para luego, si hay chance
        //se inicia el juego cuando jugador 1 elige la primera ficha con la que jugador 2 tendrá que jugar... luego el loop va: posicion/ficha




        // List<object> shapeList = new List<object>();
        // List<object> fillList = new List<object>();
        // List<object> sizeList = new List<object>();
        // List<object> colorList = new List<object>();

        // foreach(object o in status){
        //     if(o == null){
        //         shapeList.Add(o);
        //         fillList.Add(o);
        //         sizeList.Add(o);
        //         colorList.Add(o);
        //     } else {
        //         Chip c = (Chip)o;
        //         shapeList.Add(c.isSquare);
        //         fillList.Add(c.isHollow);
        //         sizeList.Add(c.isSmall);
        //         colorList.Add(c.isWhite);
        //     }
        // }





    // public class binaryvalue
    // {
    //     bool boolval;
    //     virtual string s1 = "string1"; 
    //     virtual string s2 = "string2"; 

    //     public binaryvalue(bool v){
    //         this.boolval = v;
    //         if(v){
    //             this.indicator = s1; 
    //         } else {
    //             this.indicator = s2;                 
    //         }
    //     }
    // }

    // public class squareoround : binaryvalue
    // {
    //     override string s1 = "square";
    //     override string s2 = "round";
    // }




        // int[,] d2 = new int[2,2]{
        //     {1, 2},
        //     {3, 4}
        // };
        // foreach(int v in d2){
        //     Console.WriteLine(v);
        // }
        // Console.WriteLine($"{d2[0,0]}, {d2[0,1]}, {d2[1,0]}, {d2[1,1]}");





        // b.brd[0] = new Chip(true, true, false, false);
        // b.brd[1] = new Chip(false, false, true, true);
        // b.brd[2] = new Chip(true, true, false, false);
        // b.brd[3] = new Chip(true, true, false, false);
        // b.brd[5] = new Chip(true, true, false, false);
        // b.brd[10] = new Chip(true, true, false, false);
        // b.brd[15] = new Chip(false, false, true, false);

        // b.displayBoard();

        // bool[] listbool = {false, false, false, false, true, false, false, false, true, false, false, false, true};
        // Console.WriteLine(checkWin(listbool)); 

        // Console.WriteLine(checkWin(b.brd)); 





        // var sl = new bool[]{status[0], status[4], status[8], status[12]};
        // Win = sl.All(b => b == true );


        // var l1 = new bool[]{status[0], status[4], status[8], status[12]};
        // IEnumerable<bool> l2 = l1.Where(b => b == false);
        // if(l2.Count() == 4 || l2.Count() == 0){
        //     Win = true;
        // }








