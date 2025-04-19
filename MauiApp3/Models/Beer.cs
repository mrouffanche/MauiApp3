﻿namespace MauiApp3.Models;

    public class Rating
    {
        public double Average { get; set; }
        public int Reviews { get; set; }
    }

    public class Beer
    {
        public string Price { get; set; }
        public string Name { get; set; }
        public Rating Rating { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
    }
