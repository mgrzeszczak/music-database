﻿using Backend.Service;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class SongController : ApiController
    {
        private SongService songService;

        public SongController(SongService songService)
        {
            this.songService = songService;
        }

        // GET api/values
        public bool Get()
        {
            return songService == null;
        }

    }
}
