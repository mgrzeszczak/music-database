using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{ 
    public class Song : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual Album Album { get; set; }
        public virtual TimeSpan Length { get; set; }
        public virtual int Number { get; set; }
        public virtual string YoutubeUrl { get; set; }
        public virtual string Lyrics { get; set; }

        public Song()
        {

        }

        public Song(long id, string title, Album album, TimeSpan length, int number, string youtubeUrl, string lyrics)
        {
            this.Id = id;
            this.Title = title;
            this.Album = album;
            this.Length = length;
            this.Number = number;
            this.YoutubeUrl = youtubeUrl;
            this.Lyrics = lyrics;
        }

        public Song Clone()
        {
            return new Song(Id, Title, Album, Length, Number, YoutubeUrl, Lyrics);
        }
    }
}
