﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class WallStreetBetsDB
    {
        // METHODS //
        // NOTE: all of our API calls will happen on the backend (not from the front end)
        public static List<JoinResults> GetJoinResults(string username)
        {
            List<JoinResults> results = null;
            using (WallStreetBetsContext context = new WallStreetBetsContext())
            {
                var query = from myFavs in context.Favorites
                            join myNotes in context.Notes on myFavs.id equals myNotes.favorite_id
                            where myFavs.username == username
                            select new JoinResults()
                            {
                                id = myFavs.id,
                                username = myFavs.username,
                                ticker = myFavs.ticker,
                                favorite_id = myNotes.favorite_id,
                                description = myNotes.description,
                            };
                results = query.ToList();
            }
            return results;
        }
    }

    public class User
    {
        // PROPERTIES //
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
    }

    public class Favorite
    {
        // PROPERTIES //
        public int id { get; set; }
        public string ticker { get; set; }
        public string username { get; set; }
        //public int user_id { get; set; }            // foreign key
        //public List<Note> noteList { get; set; }    // like a foreign key
    }

    public class Note
    {
        public int id { get; set; }      
        public string description { get; set; } // EXAMPLE: GME looks like a great buy!
        public int favorite_id { get; set; }
        //public DateTime lastEdit { get; set; }      //  TODO: optional
        //public List<Favorite> favoriteList { get; set; }
    }

    public class JoinResults
    {
        // PROPERTIES //
        // User
        public string username { get; set; }
        //public int user_id { get; set; }
        // Favorite
        public string ticker { get; set; }
        public int favorite_id { get; set; }
        // Note
        public string description { get; set; }
    }

    public class WallStreetBetsContext : DbContext
    {        
        // PROPERTIES //
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Note> Notes { get; set; }

        // METHODS //
        public WallStreetBetsContext(DbContextOptions<WallStreetBetsContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;");
        }
    }

}
