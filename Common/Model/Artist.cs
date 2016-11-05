
using Common.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Artist : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual IList<Album> Albums { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual IList<Genre> Genres { get; set; }
        public virtual string Description { get; set; }

        public Artist()
        {

        }

        public Artist(long id, string name, IList<Album> albums, string imageUrl, IList<Genre> genres)
        {
            this.Id = id;
            this.Name = name;
            this.Albums = new List<Album>();
            foreach (var a in albums) this.Albums.Add(a.Clone());
            this.ImageUrl = imageUrl;
            this.Genres = new List<Genre>();
            foreach (var g in genres) this.Genres.Add(g);
        }

        public Artist Clone()
        {
            return new Artist(Id,Name, Albums, ImageUrl, Genres);
        }
    }
}
