﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicGame
{
    public partial class NewGame : Form
    {
        public NewGame()
        {
            InitializeComponent();
            GameForm game = new GameForm();
            game.Show();
        }
    }
}
