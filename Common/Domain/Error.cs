﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public enum Error
    {
        UNKNOWN_ERROR,
        ARTIST_NAME_TAKEN,
        ALBUM_NAME_TAKEN,
        SONG_TITLE_TAKEN,
        SONG_NUMBER_TAKEN,
        ALBUM_NUMBER_TAKEN
    }
}
