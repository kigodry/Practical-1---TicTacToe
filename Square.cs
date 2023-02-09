﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public enum Symbol
    {
        O = -10,
        X = 10,
        Blank = 0
    }
    internal class Square
    {
        public string Name { get; set; }
        public Button Button { get; set; }
        public Symbol Symbol { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Square(Button btn)
        {
            Button = btn;
            Name = btn.Name;
            Symbol = Symbol.Blank;

        }

        public static Square? GetByName(List<Square> squares, string name)
        {
            try
            {
                foreach (Square s in squares)
                {
                    if (s.Name == name)
                    {
                        return s;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
