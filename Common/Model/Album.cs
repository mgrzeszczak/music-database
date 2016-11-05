using System.Collections.Generic;

namespace Common.Model
{
    public class Album : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual IList<Song> Songs { get; set; }
        public virtual int Number { get; set; }
        public virtual string CoverUrl { get; set; }
        public virtual Artist Artist { get; set; }

        public Album()
        {

        }

        public Album(long id, string name, IList<Song> songs, int number, string coverUrl, Artist artist)
        {
            this.Id = id;
            this.Name = name;
            this.Songs = new List<Song>();
            foreach (var s in songs) this.Songs.Add(s.Clone());
            this.Number = number;
            this.CoverUrl = coverUrl;
            this.Artist = artist.Clone();
        }

        public Album Clone()
        {
            return new Album(Id,Name,Songs,Number,CoverUrl,Artist);
        }
    }
}
