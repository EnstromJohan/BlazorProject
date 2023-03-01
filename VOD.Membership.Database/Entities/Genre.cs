﻿namespace VOD.Membership.Database.Entities
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public virtual FilmGenre? FilmGenre { get; set; }   
    }
}
