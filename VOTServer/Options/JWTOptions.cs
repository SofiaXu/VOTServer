﻿namespace VOTServer.Options
{
    public class JWTOptions
    {
        public string SigningKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
