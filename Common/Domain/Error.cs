using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Json;
using Newtonsoft.Json;

namespace Common.Domain
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum Error
    {
        UNKNOWN_ERROR,
        ARTIST_NAME_TAKEN,
        ALBUM_NAME_TAKEN,
        SONG_TITLE_TAKEN,
        SONG_NUMBER_TAKEN,
        ALBUM_NUMBER_TAKEN,

        INVALID_CREDENTIALS,
        NOT_FOUND,
        INVALID_VERSION,

        LOGIN_TAKEN,
        VALIDATION_FAILED,
        MULTIPLE_RATINGS,
        UNAUTHORIZED

    }
}
