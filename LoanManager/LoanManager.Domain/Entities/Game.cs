﻿using LoanManager.Domain.ValueObjects.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public Platform Platform { get; set; }
    }
}
